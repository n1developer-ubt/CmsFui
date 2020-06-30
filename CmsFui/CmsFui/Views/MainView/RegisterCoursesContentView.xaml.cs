using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using CmsFui.Models.Data;
using Syncfusion.SfPullToRefresh.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCoursesContentView : ContentView, BackableContentView
    {
        private readonly StudentController _studentController;
        public ObservableCollection<SemesterCourse> Courses;
        public RegisterCoursesContentView()
        {
            InitializeComponent();
            _studentController = new StudentController();
            Courses = new ObservableCollection<SemesterCourse>();
            ListViewCourses.ItemsSource = Courses;
        }

        private void BackToDashboard_Clicked(object sender, EventArgs e)
        {
            BackToDashboard?.Invoke();
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {

        }

        public async Task LoadCourses()
        {
            Courses.Clear();

            var course = await _studentController.GetCourses(Global.CurrentStudent.Id, false);

            course.ForEach(c => Courses.Add(c));
        }

        public event GoBack.ActionPerformed BackToDashboard;

        private async void PullToRefresh_OnRefreshing(object sender, EventArgs e)
        {
            await LoadCourses();
            if (sender is SfPullToRefresh sf)
                sf.IsRefreshing = false;
        }
    }
}