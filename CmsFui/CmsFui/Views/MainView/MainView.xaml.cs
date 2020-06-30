using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MainContentView.PropertyChanged += MainContentViewOnPropertyChanged;
            _selectCourse = new SelectCourseContentView();
            _selectCourse.CourseSelected += SelectCourseOnCourseSelected;

            var s = new RegisterCoursesContentView();
            MainContentView.Content = s;

            Task.Run(async () => await s.LoadCourses()).Wait();


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
                    Task.Run(async () => await _selectCourse.LoadCoursesAsync()).Wait();
                    break;
                case 1:
                    SelectedPage = PageType.RegisterCourses;
                    break;
                case 2:
                    SelectedPage = PageType.Attendance;
                    MainContentView.Content = _selectCourse;
                    await _selectCourse.LoadCoursesAsync();
                    break;
                case 3:
                    SelectedPage = PageType.Result;
                    MainContentView.Content = _selectCourse;
                    await _selectCourse.LoadCoursesAsync();
                    break;
                case 4:
                    SelectedPage = PageType.FeeChallan;
                    break;
                case 5:
                    SelectedPage = PageType.Setting;
                    break;
            }
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