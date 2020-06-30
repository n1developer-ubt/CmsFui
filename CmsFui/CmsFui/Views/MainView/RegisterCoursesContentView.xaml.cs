using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using CmsFui.Models.Data;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPullToRefresh.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCoursesContentView : ContentView, BackableContentView
    {
        private readonly StudentController _studentController;
        public ObservableCollection<SemesterCourse> Courses { get; }

        public delegate void ActionPerformed(StudentController.UpdateResult result);

        public delegate void Error(string s);

        public event Error ErrorOccured;

        public event ActionPerformed CourseRegisterUpdate;

        public RegisterCoursesContentView()
        {
            InitializeComponent();
            this.BindingContext = this;
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
            if (ListViewCourses.SelectedItems.Count == 0)
            {
                ErrorOccured?.Invoke("Please Selected Atleast One Subject");
                return;
            }

            var coursesToRegister = ListViewCourses.SelectedItems.Select(x => ((SemesterCourse) x).Id).ToList();

            var result = await _studentController.RegisterCourses(Global.CurrentStudent.Id, coursesToRegister);

            CourseRegisterUpdate?.Invoke(result);
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