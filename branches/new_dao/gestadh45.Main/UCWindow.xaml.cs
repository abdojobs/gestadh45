using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour UCWindow.xaml
	/// </summary>
	public partial class UCWindow : Window
	{
		public UCWindow(UserControl pUc) {
			InitializeComponent();
			this.contenu.Child = pUc;

			// Abonnement au message de fermeture de la fenetre
			Messenger.Default.Register<NotificationMessageFermetureFenetre>(
				this,
				(msg) => this.Close()
			);
		}
	}
}
