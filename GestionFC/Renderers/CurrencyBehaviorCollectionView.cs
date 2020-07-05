using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace GestionFC.Renderers
{
    public class CurrencyBehaviorCollectionView: Behavior<Entry>
    {
		private bool _hasFormattedOnce = false;
		private bool _firstTime = true;
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			entry.Focused += EntryOnFocused;
			entry.Unfocused += EntryOnUnfocused;
			base.OnAttachedTo(entry);
		}

		private void EntryOnUnfocused(object sender, FocusEventArgs e)
		{
			var entry = sender as Entry;
			if (entry.Text.Length <= 0)
			{
				entry.Text = "$0";
			}
		}

		private void EntryOnFocused(object sender, FocusEventArgs e)
		{
			//var entry = sender as Entry;
			//if (entry?.Text == "$0")
			//{
			//	entry.Text = "";
			//}
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			entry.Focused -= EntryOnFocused;
			entry.Unfocused -= EntryOnUnfocused;
			base.OnDetachingFrom(entry);
		}

		private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			if (string.IsNullOrEmpty(args.NewTextValue))
				return;

			string saldo = string.Empty;
			if (args.NewTextValue.Contains("$"))
			{
				_hasFormattedOnce = false;
			}
			saldo = Regex.Replace(args.NewTextValue.Trim().ToString(), @"\D", "");

			if (!_hasFormattedOnce && saldo != string.Empty)
			{
				((Entry)sender).Text = Decimal.Parse(saldo).ToString("C0");
				_hasFormattedOnce = true;
			}
		}
	}
}
