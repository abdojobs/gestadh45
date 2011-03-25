using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour App.xaml
	/// </summary>
	public partial class App : Application
	{
		static App() {
			DispatcherHelper.Initialize();
		}
	}
}
