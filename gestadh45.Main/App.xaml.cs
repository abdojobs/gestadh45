using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Threading;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour App.xaml
	/// </summary>
	public partial class App : Application
	{
		static App() {
			// Ensure the current culture passed into bindings 
			// is the OS culture. By default, WPF uses en-US 
			// as the culture, regardless of the system settings.
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement),
				new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag))
			);

			DispatcherHelper.Initialize();
		}
	}
}
