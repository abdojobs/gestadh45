using System.Windows.Controls;
using gestadh45.Ihm.ViewModel.Inscriptions;
using gestadh45.model;

namespace gestadh45.Main.UserControls.Inscriptions
{
	/// <summary>
	/// Logique d'interaction pour FormulaireInscriptionUC.xaml
	/// </summary>
	public partial class FormulaireInscriptionUC : UserControl
	{
		public FormulaireInscriptionUC() {
			InitializeComponent();
		}

		public FormulaireInscriptionUC(Inscription pInscription) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel lVm = base.DataContext as FormulaireInscriptionUCViewModel;
			lVm.EstEdition = true;
			lVm.SetInscription(pInscription);

			this.SelectionnerAdherent(pInscription.Adherent.Id);
			this.SelectionnerGroupe(pInscription.Groupe.Id);
			this.SelectionnerStatutInscription(pInscription.StatutInscription.Id);
		}

		public FormulaireInscriptionUC(Adherent pAdherent) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel lVm = base.DataContext as FormulaireInscriptionUCViewModel;
			lVm.EstEdition = false;
			lVm.SetAdherent(pAdherent);

			this.SelectionnerAdherent(pAdherent.Id);
		}

		private void SelectionnerAdherent(int pIdAdherent) {
			this.cmbAdherents.SelectedValue = pIdAdherent;
		}

		private void SelectionnerGroupe(int pIdGroupe) {
			this.cmbGroupes.SelectedValue = pIdGroupe;
		}

		private void SelectionnerStatutInscription(int pIdStatutInscription) {
			this.cmbStatutsInscription.SelectedValue = pIdStatutInscription;
		}
	}
}
