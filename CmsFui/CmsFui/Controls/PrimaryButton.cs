using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace CmsFui.Controls
{
    public class PrimaryButton:Button
    {
        public static readonly BindableProperty OnPressBackgroundColorProperty = BindableProperty.Create("OnPressBackgroundColor", typeof(Color), typeof(VisualElement), Color.Black);
        public static readonly BindableProperty OnReleaseBackgroundColorProperty = BindableProperty.Create("OnReleaseBackgroundColor", typeof(Color), typeof(VisualElement), Color.Black);
        public static readonly BindableProperty OnReleaseTextColorProperty = BindableProperty.Create("OnReleaseTextColor", typeof(Color), typeof(VisualElement), Color.Black);
        public static readonly BindableProperty OnPressTextColorProperty = BindableProperty.Create("OnPressTextColor", typeof(Color), typeof(VisualElement), Color.Black);

        public Color OnPressBackgroundColor
        {
            get=>(Color)GetValue(OnPressBackgroundColorProperty); 
            set=>SetValue(OnPressBackgroundColorProperty, value);
        }

        public Color OnReleaseBackgroundColor
        {
            get => (Color)GetValue(OnReleaseBackgroundColorProperty);
            set => SetValue(OnReleaseBackgroundColorProperty, value);
        }

        public Color OnReleaseTextColor
        {
            get => (Color)GetValue(OnReleaseTextColorProperty);
            set => SetValue(OnReleaseTextColorProperty, value);
        }

        public Color OnPressTextColor
        {
            get => (Color)GetValue(OnPressTextColorProperty);
            set => SetValue(OnPressTextColorProperty, value);
        }

        public PrimaryButton() : base()
        {
            this.Pressed += OnPressed;
            this.Released += OnReleased;
        }

        

        private void OnReleased(object sender, EventArgs e)
        {
            BackgroundColor = OnReleaseBackgroundColor;
            TextColor = OnReleaseTextColor;
        }

        private void OnPressed(object sender, EventArgs e)
        {
            BackgroundColor = OnPressBackgroundColor;
            TextColor = OnPressTextColor;
        }
    }
}
