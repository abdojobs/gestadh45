using System.Windows.Controls;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireSaisieVerificationsUC.xaml
	/// </summary>
	public partial class FormulaireSaisieVerificationsUC : UserControl
	{
		public FormulaireSaisieVerificationsUC(CampagneVerification campagne) {
			InitializeComponent();

			this.DataContext = new FormulaireSaisieVerificationsVM(campagne.ID);
		}
	}
}
