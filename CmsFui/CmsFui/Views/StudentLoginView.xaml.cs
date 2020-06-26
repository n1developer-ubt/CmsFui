using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentLoginView : ContentPage
    {
        public StudentLoginView()
        {
            InitializeComponent();
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();

            this.entryPassword.HorizontalTextAlignment = TextAlignment.Center;
            this.EntryRollNo.HorizontalTextAlignment = TextAlignment.Center;
        }

        private void BtnLogin_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}