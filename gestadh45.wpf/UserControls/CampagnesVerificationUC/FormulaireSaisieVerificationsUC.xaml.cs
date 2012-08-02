using System;
using System.Windows.Controls;
using gestadh45.business.ViewModel.CampagnesVerificationVM;

namespace gestadh45.wpf.UserControls.CampagnesVerificationUC
{
	/// <summary>
	/// Logique d'interaction pour FormulaireSaisieVerificationsUC.xaml
	/// </summary>
	public partial class FormulaireSaisieVerificationsUC : UserControl
	{
		public FormulaireSaisieVerificationsUC(Guid idCampagneVerification) {
			InitializeComponent();

			this.DataContext = new FormulaireSaisieVerificationsVM(idCampagneVerification);
		}
	}
}
