using System;
using System.Windows.Data;
using System.Windows.Media;

namespace gestadh45.wpf.Tools
{
	public class BooleanToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(Brush)) {
				throw new InvalidOperationException("The target must be a Brush");
			}

			if ((bool)value) {
				return Brushes.Red;
			}
			else {
				return Brushes.Black;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
