using System.Windows.Controls;
using gestadh45.dal;
using gestadh45.Ihm.ViewModel.Adherents;

namespace gestadh45.Main.UserControls.Adherents
{
	/// <summary>
	/// Logique d'interaction pour FormulaireAdherentUC.xaml
	/// </summary>
	public partial class FormulaireAdherentUC : UserControl
	{
		public FormulaireAdherentUC() {
			InitializeComponent();
		}

		public FormulaireAdherentUC(Adherent pAdherent) {
			InitializeComponent();
			FormulaireAdherentUCViewModel lVm = base.DataContext as FormulaireAdherentUCViewModel;
			lVm.Adherent = pAdherent;
			lVm.EstEdition = true;
		}
	}
}
