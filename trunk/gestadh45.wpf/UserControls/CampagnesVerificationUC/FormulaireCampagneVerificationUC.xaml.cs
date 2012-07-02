using System.Windows.Controls;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireCampagneVerificationUC.xaml
	/// </summary>
	public partial class FormulaireCampagneVerificationUC : UserControl
	{
		public FormulaireCampagneVerificationUC() {
			InitializeComponent();
		}

		public FormulaireCampagneVerificationUC(CampagneVerification campagne) {
			InitializeComponent();
			this.DataContext = new FormulaireCampagneVerificationVM(campagne.ID);
		}
	}
}
