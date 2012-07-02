using System.Windows.Controls;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Logique d'interaction pour SaisieVerificationsUC.xaml
	/// </summary>
	public partial class SaisieVerificationsUC : UserControl
	{
		public SaisieVerificationsUC(CampagneVerification campagneVerification) {
			InitializeComponent();
			this.DataContext = new SaisieVerificationsVM(campagneVerification.ID);
		}
	}
}
