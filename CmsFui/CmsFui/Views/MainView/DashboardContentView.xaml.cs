using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models;
using CmsFui.Models.Data;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardContentView : ContentView
    {
        public ObservableCollection<AttendanceModel> Data { get; set; }
        private StudentController _studentController;
        private Student CurrentStudent => Global.CurrentStudent;
        public async Task LoadEverything()
        {
            LblName.Text = CurrentStudent.Name;
            LblProgram.Text = CurrentStudent.Program;
            LblRolNo.Text = CurrentStudent.Season+ CurrentStudent.Year+ CurrentStudent.Program+ CurrentStudent.RollNo;

            var result = await _studentController.GetStudentSemesters(CurrentStudent.Id);

            LblGpa.Text = result.Average(x => x.Gpa).ToString();

            var courses = await _studentController.GetCourses(CurrentStudent.Id);

            foreach (var course in courses)
            {
                var attendance = await _studentController.GetAttendance(CurrentStudent.Id, course.Course.Code);

                var attGroup = attendance.GroupBy(at => at.Date.Month);

                var data = new ObservableCollection<AttendanceModel>();

                foreach (var att in attGroup)
                {
                    //data.Add( new AttendanceModel());
                }

            }

        }

        public DashboardContentView()
        {
            InitializeComponent();
            _studentController = new StudentController();

            Data = new ObservableCollection<AttendanceModel>()
            {
                new AttendanceModel("Jan", 50),
                new AttendanceModel("Feb", 70),
                new AttendanceModel("Mar", 65),
                new AttendanceModel("Apr", 57),
                new AttendanceModel("May", 48),
            };
            var marker = new ChartDataMarker(){LabelStyle =  new DataMarkerLabelStyle(){LabelPosition = DataMarkerLabelPosition.Inner}};
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "Hello", Width = 100, EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloWorld", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloS", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker});
        }

        public class AttendanceModel
        {
            public string Month { get; set; }

            public double Target { get; set; }

            public AttendanceModel(string xValue, double yValue)
            {
                Month = xValue;
                Target = yValue;
            }
        }
    }
}