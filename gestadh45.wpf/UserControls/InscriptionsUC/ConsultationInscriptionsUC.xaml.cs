using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.wpf.UserControls.InscriptionsUC
{
	/// <summary>
	/// Logique d'interaction pour ConsultationInscriptionsUC.xaml
	/// </summary>
	public partial class ConsultationInscriptionsUC : UserControl
	{
		public ConsultationInscriptionsUC() {
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
		}

		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
	}
}
