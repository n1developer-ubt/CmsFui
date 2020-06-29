using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private SelectCourseContentView SelectCourse;
        public MainView()
        {
            InitializeComponent();
            SelectCourse = new SelectCourseContentView();
            SelectCourse.CourseSelected += SelectCourseOnCourseSelected;
            var ex = new ExamContentView();
            MainContentView.Content = ex;


            if (Global.CurrentStudent == null)
                return;

            LblStudentName.Text = CurrentStudent.Name;
        }

        private void SelectCourseOnCourseSelected(string code)
        {
            
        }

        void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void Logout_OnCLick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new StudentLoginView();
        }

        private int SelectedItem = -1;

        private async void MenuView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MenuView.SelectedItem == null)
                return;

            navigationDrawer.ToggleDrawer();

            MenuView.SelectedItem = null;

            SelectedItem = e.SelectedItemIndex;

            switch (e.SelectedItemIndex)
            {
                case 0:
                    MainContentView.Content = SelectCourse;
                    await SelectCourse.LoadCoursesAsync();
                    break;
                case 1:
                    break;
                case 2:
                    MainContentView.Content = SelectCourse;
                    await SelectCourse.LoadCoursesAsync();
                    break;
                case 3:
                    MainContentView.Content = SelectCourse;
                    await SelectCourse.LoadCoursesAsync();
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }

    public enum PageType
    {
        CoursePortal
    }
}