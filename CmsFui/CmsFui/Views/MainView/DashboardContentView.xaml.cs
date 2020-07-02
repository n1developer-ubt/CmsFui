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
        public Student CurrentStudent => Global.CurrentStudent;

        public async Task LoadEverything()
        {
            var result = await _studentController.GetStudentSemesters(CurrentStudent.Id);

            LblGpa.Text = result.Average(x => x.Gpa).ToString();

            var courses = await _studentController.GetCourses(CurrentStudent.Id);

            var atts = new Dictionary<string, List<Attendance>>();

            foreach (var course in courses)
            {
                var attendance = await _studentController.GetAttendance(CurrentStudent.Id, course.Course.Code);

                foreach (var att in attendance)
                {
                    if (!atts.ContainsKey(att.Date.Month.ToString()))
                    {
                        atts[att.Date.Month.ToString()] = new List<Attendance>();
                    }

                    atts[att.Date.Month.ToString()].Add(att);
                }

                var data = new ObservableCollection<DashboardContentView.AttendanceModel>();

                foreach (var re in atts.Keys)
                {
                    var da = new AttendanceModel(re, atts[re].Count(x => x.Present));
                    data.Add(da);
                }

                data = new ObservableCollection<DashboardContentView.AttendanceModel>(
                    data.OrderBy(i => Convert.ToInt16(i.Month)));

                
                var marker = new ChartDataMarker()
                    { LabelStyle = new DataMarkerLabelStyle() { LabelPosition = DataMarkerLabelPosition.Inner } };

                ChartAttendance.Series.Add(new ColumnSeries()
                {
                    ItemsSource = data,
                    DataMarker = marker,
                    XBindingPath = "Month",
                    YBindingPath = "Target",
                    Label = course.Course.Name,
                    EnableAnimation = true,
                    AnimationDuration = 1
                });
            }

            var results = await _studentController.GetStudentSemesters(CurrentStudent.Id);

            foreach (var semester in results)
            {
                var resultsData = new ObservableCollection<AttendanceModel>();
                resultsData.Add(new AttendanceModel(semester.Title.Split('#')[1],semester.Gpa));
                var markerResult = new ChartDataMarker()
                    { LabelStyle = new DataMarkerLabelStyle() { LabelPosition = DataMarkerLabelPosition.Inner } };


                ChartResults.Series.Add(new ColumnSeries()
                {
                    ItemsSource = resultsData,
                    DataMarker = markerResult,
                    XBindingPath = "Month",
                    YBindingPath = "Target",
                    Label = semester.Title,
                    EnableAnimation = true,
                    AnimationDuration = 1,
                    Spacing = 0.1
                });
            }

        }

        public DashboardContentView()
        {
            InitializeComponent();
            BindingContext = this;
            _studentController = new StudentController();
            return;
            Data = new ObservableCollection<AttendanceModel>()
            {
                new AttendanceModel("Jan", 50),
                new AttendanceModel("Feb", 70),
                new AttendanceModel("Mar", 65),
                new AttendanceModel("Apr", 57),
                new AttendanceModel("May", 48),
            };
            var marker = new ChartDataMarker(){LabelStyle =  new DataMarkerLabelStyle(){LabelPosition = DataMarkerLabelPosition.Inner}};
            ChartAttendance.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "Hello", Width = 100, EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            ChartAttendance.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloWorld", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            ChartAttendance.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloS", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            ChartAttendance.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker});
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