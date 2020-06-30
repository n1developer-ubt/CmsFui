using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultContentView : ContentView, BackableContentView
    {
        private readonly StudentController _studentController;

        public ObservableCollection<StudentSemester> Semesters;

        public ResultContentView()
        {
            InitializeComponent();

            _studentController = new StudentController();
            Semesters = new ObservableCollection<StudentSemester>();
            ListViewResult.ItemsSource = Semesters;


        }

        public async Task Load()
        {
            Semesters.Clear();

            var results = await _studentController.GetStudentSemesters(Global.CurrentStudent.Id);

            results.ForEach(r=>Semesters.Add(r));

            int x = 0;
        }

        public event GoBack.ActionPerformed BackToDashboard;

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            BackToDashboard?.Invoke();
        }
    }
}