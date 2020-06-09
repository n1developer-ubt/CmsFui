using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems { get { return menuItems; } }
        public MainView()
        {
            InitializeComponent();
            //hamburgerButton.Image = (FileImageSource)ImageSource.FromFile("hamburger_icon.png");
            MainContentView.Content = new DashboardContentView();
        }

        void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Your codes here
            navigationDrawer.ToggleDrawer();
        }

        public class MenuItem
        {
            public string ItemName { get; set; }
        }
    }
}