using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;

namespace gestadh45.wpf.Tools
{
	public class HexaColorStringToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(Brush)) {
				throw new InvalidOperationException("The target must be a Brush");
			}

			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Brush));
			Brush b = (Brush)converter.ConvertFrom(value);

			return b;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
