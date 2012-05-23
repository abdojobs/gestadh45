using System;
using System.Windows.Controls;
using gestadh45.business.ViewModel.EquipementsVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.EquipementsUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireEquipementUC.xaml
	/// </summary>
	public partial class FormulaireEquipementUC : UserControl
	{
		public FormulaireEquipementUC() {
			InitializeComponent();
		}

		public FormulaireEquipementUC(Equipement equipement) {
			InitializeComponent();
			this.DataContext = new FormulaireEquipementVM(equipement.ID);
		}
	}
}
