using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CmsFui.Controls
{
    class PrimaryButton:Button
    {
        public Color OnPressBackgroundColor { get; set; }
        public Color OnReleaseBackgroundColor { get; set; }
        public Color OnReleaseTextColor { get; set; }
        public Color OnPressTextColor { get; set; }
        
        public PrimaryButton() : base()
        {
            this.Pressed += OnPressed;
            this.Released += OnReleased;
            OnReleaseBackgroundColor = BackgroundColor;
            OnReleaseTextColor = TextColor;
            OnPressTextColor = TextColor;
            OnPressBackgroundColor = BackgroundColor;
        }

        private void OnReleased(object sender, EventArgs e)
        {
            BackgroundColor = OnReleaseBackgroundColor;
        }

        private void OnPressed(object sender, EventArgs e)
        {
            BackgroundColor = OnPressBackgroundColor;
        }
    }
}
