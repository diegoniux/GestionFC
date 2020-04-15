using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using GestionFC.iOS.Renderers;
using GestionFC.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ProductividadPage), typeof(ProductividadPageRenderer))]
namespace GestionFC.iOS.Renderers
{
    public class ProductividadPageRenderer : PageRenderer
    {
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation"));
        }
    }
}