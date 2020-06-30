using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class SelectCourseContentView : ContentView, BackableContentView
    {
        private StudentController _studentController = new StudentController();

        public delegate void Action(string code, string courseName);

        public event Action CourseSelected;

        public ObservableCollection<SemesterCourse> Courses = new ObservableCollection<SemesterCourse>();

        private string _courseCode = "";

        public SelectCourseContentView()
        {
            InitializeComponent();
            ListViewCourses.ItemsSource = Courses;

            Courses.Add(new SemesterCourse() { Course = new Course() { Code = "123", Name = "SVV" }, Teacher = new Teacher() { Name = "Haris Javed" } });
            Courses.Add(new SemesterCourse() { Course = new Course() { Code = "123" } });
            Courses.Add(new SemesterCourse() { Course = new Course() { Code = "123" } });
        }

        public async Task LoadCoursesAsync()
        {
            Courses.Clear();

            var result = await _studentController.GetCourses(Global.CurrentStudent.Id);

            result.ForEach(sc => Courses.Add(sc));
        }

        public void LoadCourses()
        {
            Courses.Clear();

            var result = _studentController.GetCourses(Global.CurrentStudent.Id);
            result.Wait();

            int x = 0;

            result.Result.ForEach(sc => Courses.Add(sc));
        }

        private void ListViewCourses_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListViewCourses.SelectedItem = null;

            if (e.SelectedItem is SemesterCourse sc)
            {
                CourseSelected?.Invoke(sc.Course.Code, sc.Course.Name);
            }
        }

        private async void ListViewCourses_OnRefreshing(object sender, EventArgs e)
        {
            await LoadCoursesAsync();
            ListViewCourses.IsRefreshing = false;
        }

        public event GoBack.ActionPerformed BackToDashboard;

        private void BackToDashboard_Clicked(object sender, EventArgs e)
        {
            BackToDashboard?.Invoke();
        }
    }
}