using System.Windows.Controls;
using gestadh45.Ihm.ViewModel.Formulaire;
using gestadh45.Model;

namespace gestadh45.Main.UserControls.Formulaire
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
			FormulaireInscriptionUCViewModel formulaireInscriptionUCViewModel = base.DataContext as FormulaireInscriptionUCViewModel;
			formulaireInscriptionUCViewModel.Inscription = pInscription;
			formulaireInscriptionUCViewModel.EstEdition = true;
		}

		public FormulaireInscriptionUC(Adherent pAdherent) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel formulaireInscriptionUCViewModel = base.DataContext as FormulaireInscriptionUCViewModel;
			Inscription inscription = new Inscription();
			inscription.Adherent = pAdherent;
			formulaireInscriptionUCViewModel.Inscription = inscription;
		}
	}
}
