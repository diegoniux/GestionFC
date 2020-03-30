using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GestionFC.Renderers
{
    public class EntryIcon : Entry
    {
        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(EntryIcon), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(EntryIcon), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(EntryIcon), new Thickness(5));

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryIcon), Color.Transparent);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

    }
}
