﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using CmsFui.Models.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public Student CurrentStudent => Global.CurrentStudent;
        private readonly SelectCourseContentView _selectCourse;
        private DashboardContentView _dashboard;
        public MainView()
        {
            InitializeComponent();

            _dashboard = new DashboardContentView();

            Task.Run(async () => { await _dashboard.LoadEverything(); });

            MainContentView.PropertyChanged += MainContentViewOnPropertyChanged;
            _selectCourse = new SelectCourseContentView();
            _selectCourse.CourseSelected += SelectCourseOnCourseSelected;

            var s = new ResultContentView();
            MainContentView.Content = _dashboard;

            if (Global.CurrentStudent == null)
                return;

            LblStudentName.Text = CurrentStudent.Name;
        }

        private void MainContentViewOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ContentView c)
            {
                if (e.PropertyName.ToLower().Equals("content"))
                {
                    if (c.Content is BackableContentView bc)
                    {
                        bc.BackToDashboard -= BcOnBackToDashboard;
                        bc.BackToDashboard += BcOnBackToDashboard;
                    }
                }
            }
        }

        private void BcOnBackToDashboard()
        {
            MainContentView.Content = _dashboard;
        }

        private async void SelectCourseOnCourseSelected(string code, string courseName)
        {
            switch (SelectedPage)
            {
                case PageType.CoursePortal:
                    var exams = new ExamContentView(courseName);
                    MainContentView.Content = exams;
                    await exams.LoadExams(code);
                    break;
                case PageType.Attendance:
                    var att = new AttendanceContentView(courseName);
                    MainContentView.Content = att;
                    await att.Load(code);
                    break;
            }
        }

        void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void Logout_OnCLick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new StudentLoginView();
        }

        private PageType SelectedPage;

        private async void MenuView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MenuView.SelectedItem == null)
                return;

            navigationDrawer.ToggleDrawer();

            MenuView.SelectedItem = null;

            switch (e.SelectedItemIndex)
            {
                case 0:
                    SelectedPage = PageType.CoursePortal;
                    MainContentView.Content = _selectCourse;
                    await _selectCourse.LoadCoursesAsync();
                    break;
                case 1:
                    SelectedPage = PageType.RegisterCourses;
                    var r = new RegisterCoursesContentView();
                    r.CourseRegisterUpdate += ROnCourseRegisterUpdate;
                    r.ErrorOccured += ROnErrorOccured;
                    MainContentView.Content = r;
                    await r.LoadCourses();
                    break;
                case 2:
                    SelectedPage = PageType.Attendance;
                    MainContentView.Content = _selectCourse;
                    await _selectCourse.LoadCoursesAsync();
                    break;
                case 3:
                    SelectedPage = PageType.Result;
                    var rv = new ResultContentView();
                    MainContentView.Content = rv;
                    await rv.Load();
                    break;
                case 4:
                    SelectedPage = PageType.FeeChallan;
                    MainContentView.Content = new FeeChallanContentView();
                    break;
                case 5:
                    SelectedPage = PageType.Setting;
                    var s = new SettingContentView();
                    MainContentView.Content = s;
                    s.Updated += ROnCourseRegisterUpdate;
                    await s.Load();
                    break;
            }
        }

        private void ROnErrorOccured(string s)
        {
            DisplayAlert("Error", s, "Ok");
        }

        private async void ROnCourseRegisterUpdate(StudentController.UpdateResult result, string e)
        {
            if (result == StudentController.UpdateResult.Updated)
            {
                var suc = new Successful(e);
                await Navigation.PushModalAsync(suc);
                await suc.Wait();
                return;
            }

            await DisplayAlert("Error", e, "Ok");
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }

    public enum PageType
    {
        CoursePortal,
        RegisterCourses,
        Attendance,
        Result,
        FeeChallan,
        Setting
    }
}