using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.wpf
{
	/// <summary>
	/// Logique d'interaction pour UCWindow.xaml
	/// </summary>
	public partial class UCWindow : Window
	{
		public UCWindow(UserControl uc) {
			InitializeComponent();
			this.contenu.Children.Add(uc);

			Messenger.Default.Register<NMCloseWindow>(this, m => this.Close());
		}
	}
}
