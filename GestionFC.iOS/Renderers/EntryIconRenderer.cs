
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using GestionFC.iOS.Renderers;
using GestionFC.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryIcon), typeof(EntryIconRenderer))]
namespace GestionFC.iOS.Renderers
{
    public class EntryIconRenderer: EntryRenderer
    {
        public EntryIcon ElementV2 => Element as EntryIcon;
        public UITextFieldPadding ControlV2 => Control as UITextFieldPadding;

        protected override UITextField CreateNativeControl()
        {
            var control = new UITextFieldPadding(RectangleF.Empty)
            {
                Padding = ElementV2.Padding,
                BorderStyle = UITextBorderStyle.RoundedRect,
                ClipsToBounds = true
            };

            UpdateBackground(control);

            return control;
        }

        protected void UpdateBackground(UITextField control)
        {
            if (control == null) return;
            control.Layer.CornerRadius = ElementV2.CornerRadius;
            control.Layer.BorderWidth = ElementV2.BorderThickness;
            control.Layer.BorderColor = ElementV2.BorderColor.ToCGColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EntryIcon.PaddingProperty.PropertyName)
            {
                UpdatePadding();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdatePadding()
        {
            if (Control == null)
                return;

            ControlV2.Padding = ElementV2.Padding;
        }
    }

    public class UITextFieldPadding : UITextField
    {
        private Thickness _padding = new Thickness(5);

        public Thickness Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding = value;
                    //InvalidateIntrinsicContentSize();
                }
            }
        }

        public UITextFieldPadding()
        {
        }
        public UITextFieldPadding(NSCoder coder) : base(coder)
        {
        }

        public UITextFieldPadding(CGRect rect) : base(rect)
        {
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            var insets = new UIEdgeInsets((float)Padding.Top, (float)Padding.Left, (float)Padding.Bottom, (float)Padding.Right);
            return insets.InsetRect(forBounds);
        }
    }
}