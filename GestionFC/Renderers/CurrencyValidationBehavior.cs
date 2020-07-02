using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace GestionFC.Renderers
{
    public class CurrencyValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers

                ((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
            //if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            //{
            //    string value = args.NewTextValue.Replace("$", "").Replace(",", "");
            //    decimal val = 0;

            //    bool isValid = value.ToCharArray().All(x => char.IsDigit(x)) && decimal.TryParse(value, out val); //Make sure all characters are numbers

            //    ((Entry)sender).Text = isValid ? val.ToString("C0") : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            //}
        }


    }
}
