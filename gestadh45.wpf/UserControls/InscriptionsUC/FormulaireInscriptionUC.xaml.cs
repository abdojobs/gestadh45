using System;
using System.Windows.Controls;
using gestadh45.business.ViewModel.InscriptionsVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.InscriptionsUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireInscription.xaml
	/// </summary>
	public partial class FormulaireInscriptionUC : UserControl
	{
		public FormulaireInscriptionUC() {
			InitializeComponent();
		}

		public FormulaireInscriptionUC(Inscription inscription) {
			InitializeComponent();
			this.DataContext = new FormulaireInscriptionVM((Guid)inscription.ID);
			
		}

		public FormulaireInscriptionUC(Adherent adherent) {
			InitializeComponent();
			var vm = this.DataContext as FormulaireInscriptionVM;
			vm.SetAdherent((Guid)adherent.ID);
		}
	}
}
