using System;
using CmsFui.Models;
using CmsFui.Models.Data;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjY5NjgyQDMxMzgyZTMxMmUzMGpKSktZRmI0dUNBdVdXdFFTcDFCSTJqQXQrZ2QzWk54NnduSklaYmZkRU09");
            InitializeComponent();

            Global.CurrentStudent = new Student()
            {
                Email = "usamat37@gmail.com",
                Id = 1,
                Name = "Usama Bin Tariq",
                Program = "BCSE",
                RollNo = "053",
                Year = 2017,
            };

            MainPage = new NavigationPage(new MainView());
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
