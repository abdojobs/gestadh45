using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace gestadh45.Ihm.Tools
{
	public class ListToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(string)) {
				throw new InvalidOperationException("The target must be a string");
			}

			if (value == null) {
				return string.Empty;
			}
			else {
				StringBuilder lSb = new StringBuilder();
				foreach (string lChaine in value as List<string>) {
					lSb.Append(lChaine + "\r\n");
				}

				return lSb.ToString();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
