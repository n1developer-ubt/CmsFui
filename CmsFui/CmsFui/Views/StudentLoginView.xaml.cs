using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmsFui.Controller;
using CmsFui.Models.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentLoginView : ContentPage
    {
        private readonly StudentController _studentController;
        public StudentLoginView()
        {
            InitializeComponent();
            _studentController= new StudentController();
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();

            this.entryPassword.HorizontalTextAlignment = TextAlignment.Center;
            this.EntryRollNo.HorizontalTextAlignment = TextAlignment.Center;
        }

        private async void BtnLogin_OnClicked(object sender, EventArgs e)
        {

            string username = CBYear.SelectedItem.ToString() + CBProgram.SelectedItem.ToString() +
                              EntryRollNo.Text.Trim();

            BtnLogin.IsEnabled = false;

            var student = await _studentController
                .Authenticate(
                    new AuthenticationModel()
                        {
                            Password = entryPassword.Text,Username = username
                        }
                    );


            if (student == null)
            {
                await DisplayAlert("Unable to login!", "Authentication Error", "Ok");
                BtnLogin.IsEnabled = true;
                return;
            }
            Application.Current.MainPage = new NavigationPage(new MainView.MainView());
        }
    }
}