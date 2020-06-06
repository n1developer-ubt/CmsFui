using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CmsFui
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnPressed(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                b.BackgroundColor = Color.White;
            }
        }

        private void Button_OnReleased(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                b.BackgroundColor = Color.Black;
            }
        }
    }
}
