using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Formulaire;
using gestadh45.model;

namespace gestadh45.Main.UserControls.Formulaire
{
	/// <summary>
	/// Logique d'interaction pour FormulaireAdherentUC.xaml
	/// </summary>
	public partial class FormulaireAdherentUC : UserControl
	{
		public FormulaireAdherentUC() {
			InitializeComponent();
			this.AbonnementMessages();
		}

		public FormulaireAdherentUC(Adherent pAdherent, MsgAfficherUC.TypeOuverture pMode) {
			InitializeComponent();
			FormulaireAdherentUCViewModel lVm = base.DataContext as FormulaireAdherentUCViewModel;
			lVm.EstEdition = (pMode == MsgAfficherUC.TypeOuverture.Edition);
			lVm.SetAdherent(pAdherent);

			this.SelectionnerSexe(pAdherent.Sexe.Id);
			this.SelectionnerVille(pAdherent.Adresse.Ville.Id);

			this.AbonnementMessages();
		}

		private void AbonnementMessages() {
			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this,
				(msg) => this.SelectionnerVille(msg.Content.Id)
			);
		}

		private void SelectionnerSexe(int pSexeId) {
			this.cmbSexes.SelectedValue = pSexeId;
		}

		private void SelectionnerVille(int pIdVille) {
			this.cmbVilles.SelectedValue = pIdVille;
		}
	}
}
