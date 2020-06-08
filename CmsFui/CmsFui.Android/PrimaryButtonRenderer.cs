using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using CmsFui.Droid;
using Xamarin.Forms;
using CmsFui.Controls;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(PrimaryButton), typeof(PrimaryButtonRenderer))]
namespace CmsFui.Droid
{
    class PrimaryButtonRenderer:ButtonRenderer
    {
        public PrimaryButtonRenderer(Context c) : base(c)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            SetBackgroundResource(Resource.Drawable.hideRipple);
        }
    }
}