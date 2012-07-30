using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel;

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

			Messenger.Default.Register<NMCloseWindow>(this, m => this.CloseWindow(m.UCGuid));
		}

		private void CloseWindow(Guid ucGuid) {
			var uc = this.contenu.Children[0] as UserControl;

			if (uc != null) {
				var dc = uc.DataContext as VMUCBase;

				if (dc != null && dc.UCGuid == ucGuid) {
					this.Close();
				}
			}
		}
	}
}
