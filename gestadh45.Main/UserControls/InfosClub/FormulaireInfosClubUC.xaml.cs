using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Main.UserControls.InfosClub
{
	/// <summary>
	/// Logique d'interaction pour FormulaireInfosClubUC.xaml
	/// </summary>
	public partial class FormulaireInfosClubUC : UserControl
	{
		public FormulaireInfosClubUC() {
			InitializeComponent();
			this.AbonnementMessages();
		}

		private void AbonnementMessages() {
			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this,
				(msg) => this.SelectionnerVille(msg.Content.Id)
			);
		}

		private void SelectionnerVille(int pIdVille) {
			this.cmbVilles.SelectedValue = pIdVille;
		}
	}
}
