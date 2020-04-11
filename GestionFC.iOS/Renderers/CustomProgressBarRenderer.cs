using CoreGraphics;
using GestionFC.iOS;
using GestionFC.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace GestionFC.iOS
{

    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(
         ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            Control.ProgressTintColor = Color.FromHex("#45d2b9").ToUIColor();
            Control.TrackTintColor = Color.FromRgb(188, 203, 219).ToUIColor();
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var X = 1.0f;
            var Y = 4.0f;

            CGAffineTransform transform = CGAffineTransform.MakeScale(X, Y);
            this.Transform = transform;



            this.ClipsToBounds = true;
            this.Layer.MasksToBounds = true;
            this.Layer.CornerRadius = 5; // This is for rounded corners.
        }
    }
}