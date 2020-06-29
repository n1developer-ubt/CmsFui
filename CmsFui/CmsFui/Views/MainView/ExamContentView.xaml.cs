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
    public partial class ExamContentView : ContentView
    {
        private StudentController _studentController = new StudentController();
        public ObservableCollection<StudentExam> StudentExams = new ObservableCollection<StudentExam>();
        public ExamContentView()
        {
            InitializeComponent();
            ListViewExams.ItemsSource = StudentExams;
        }

        public async Task LoadExams(int studentId, string courseId)
        {
            StudentExams.Clear();
            var exams = await _studentController.GetCourseExams(studentId, courseId);
            exams.ForEach(e=> StudentExams.Add(e));
        }

    }
}