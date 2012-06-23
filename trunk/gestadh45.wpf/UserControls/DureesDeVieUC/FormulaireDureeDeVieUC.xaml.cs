using System.Windows.Controls;
using gestadh45.business.ViewModel.DureesDeVieVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.DureesDeVieUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireDureeDeVieUC.xaml
	/// </summary>
	public partial class FormulaireDureeDeVieUC : UserControl
	{
		public FormulaireDureeDeVieUC() {
			InitializeComponent();
		}

		public FormulaireDureeDeVieUC(DureeDeVie dureeDeVie) {
			InitializeComponent();
			this.DataContext = new FormulaireDureeDeVieVM(dureeDeVie.ID);
		}
	}
}
