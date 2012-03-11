using System.Windows.Controls;
using gestadh45.business.ViewModel.AdherentsVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.AdherentsUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireAdherentUC.xaml
	/// </summary>
	public partial class FormulaireAdherentUC : UserControl
	{
		public FormulaireAdherentUC() {
			InitializeComponent();
		}

		public FormulaireAdherentUC(Adherent adherent) {
			InitializeComponent();
			this.DataContext = new FormulaireAdherentVM((int)adherent.ID);
		}
	}
}
