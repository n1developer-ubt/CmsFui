using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CmsFui.Views.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingContentView : ContentView
    {
        public SettingContentView()
        {
            InitializeComponent();
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Successful());
            await Task.Delay(2000);
            await Navigation.PopModalAsync(true);
        }
    }
}