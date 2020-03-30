using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using GestionFC.Droid.Renderers;
using GestionFC.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryIcon), typeof(EntryIconRenderer))]
namespace GestionFC.Droid.Renderers
{
    public class EntryIconRenderer : EntryRenderer
    {
        public EntryIconRenderer(Context context) : base(context)
        {
        }

        public EntryIcon ElementV2 => Element as EntryIcon;
        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EntryIcon.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryIcon.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryIcon.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}