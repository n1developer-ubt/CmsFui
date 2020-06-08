using System;
using CmsFui.Views;
using CmsFui.Views.MainView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CmsFui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
