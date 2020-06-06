using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CmsFui.Controls
{
    class PrimaryButton:Button
    {
        public Color OnPressColor { get; set; }
        public Color OnReleaseColor { get; set; }
        public Color OnReleaseTextColor { get; set; }
        public Color OnPressTextColor { get; set; }
        
        public PrimaryButton() : base()
        {
            this.Pressed += OnPressed;
            this.Released += OnReleased;
            OnReleaseColor = BackgroundColor;
            OnPressColor = BackgroundColor;
        }

        private void OnReleased(object sender, EventArgs e)
        {
            BackgroundColor = OnReleaseColor;
        }

        private void OnPressed(object sender, EventArgs e)
        {
            BackgroundColor = OnPressColor;
        }
    }
}
