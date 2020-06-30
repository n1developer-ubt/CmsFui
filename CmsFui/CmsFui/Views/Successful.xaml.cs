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
    public partial class Successful : ContentPage
    {
        
        public Successful(string message)
        {
            InitializeComponent();
            LabelText.Text = message;
        }

        private bool Poped = false;

        public async Task Wait()
        {
            await Task.Delay(1000);
            if(!Poped)
                await Navigation.PopModalAsync(true);
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Poped = true;
            await Navigation.PopModalAsync(true);
        }
    }
}