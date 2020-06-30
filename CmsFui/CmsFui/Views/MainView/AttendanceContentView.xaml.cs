using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class AttendanceContentView : ContentView, BackableContentView
    {
        private readonly string _courseName;
        private readonly StudentController _studentController;

        public AttendanceContentView(string courseName)
        {
            _courseName = courseName;
            LabelCourseName.Text = courseName;
            InitializeComponent();
            _studentController = new StudentController();
        }

        public async Task Load(string courseCode)
        {
            var result = await _studentController.GetAttendance(Global.CurrentStudent.Id, courseCode);
            Dictionary<string, List<Attendance>> res = new Dictionary<string, List<Attendance>>();

            foreach (var attendance in result)
            {
                if (!res.ContainsKey(attendance.Date.Month.ToString()))
                {
                    res[attendance.Date.Month.ToString()] = new List<Attendance>();
                }

                res[attendance.Date.Month.ToString()].Add(attendance);
            }

            var l = new ObservableCollection<DashboardContentView.AttendanceModel>();

            foreach (var re in res.Keys)
            {
                var da = new DashboardContentView.AttendanceModel(re, res[re].Count(x => x.Present));
                l.Add(da);
            }

            l = new ObservableCollection<DashboardContentView.AttendanceModel>(l.OrderByDescending(i => Convert.ToInt16(i.Month)));

            var marker = new ChartDataMarker()
            { LabelStyle = new DataMarkerLabelStyle() { LabelPosition = DataMarkerLabelPosition.Inner } };
            ChartVisual.Series.Add(new ColumnSeries()
            {
                ItemsSource = l,
                DataMarker = marker,
                XBindingPath = "Month",
                YBindingPath = "Target",
                Label = "Hello",
                Width = 100,
                EnableAnimation = true,
                AnimationDuration = 1,
                Spacing = 0.5,
            });
            CpbPresent.Progress = (result.Count(x => x.Present) * 100) / result.Count;
        }

        public event GoBack.ActionPerformed BackToDashboard;

        public void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            BackToDashboard?.Invoke();
        }
    }
}