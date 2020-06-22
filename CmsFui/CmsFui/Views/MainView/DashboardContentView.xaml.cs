using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardContentView : ContentView
    {
        public ObservableCollection<Model> Data { get; set; }
        public DashboardContentView()
        {
            InitializeComponent();
            Data = new ObservableCollection<Model>()
            {
                new Model("Jan", 50),
                new Model("Feb", 70),
                new Model("Mar", 65),
                new Model("Apr", 57),
                new Model("May", 48),
            };
            var marker = new ChartDataMarker(){LabelStyle =  new DataMarkerLabelStyle(){LabelPosition = DataMarkerLabelPosition.Inner}};
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "Hello", Width = 100, EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloWorld", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", Label = "HelloS", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker });
            MyChart.Series.Add(new ColumnSeries(){ItemsSource = Data, XBindingPath = "Month", YBindingPath = "Target", EnableAnimation = true, AnimationDuration = 1, DataMarker = marker});
        }

        public class Model
        {
            public string Month { get; set; }

            public double Target { get; set; }

            public Model(string xValue, double yValue)
            {
                Month = xValue;
                Target = yValue;
            }
        }
    }
}