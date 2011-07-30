using System.Windows.Controls;
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
		}

		public FormulaireAdherentUC(Adherent pAdherent, MsgAfficherUC.TypeOuverture pMode) {
			InitializeComponent();
			FormulaireAdherentUCViewModel lVm = base.DataContext as FormulaireAdherentUCViewModel;
			lVm.EstEdition = (pMode == MsgAfficherUC.TypeOuverture.Edition);
			lVm.SetAdherent(pAdherent);
		}
	}
}
