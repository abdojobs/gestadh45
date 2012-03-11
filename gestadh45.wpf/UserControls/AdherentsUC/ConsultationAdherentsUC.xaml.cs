using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.wpf.UserControls.AdherentsUC
{
	/// <summary>
	/// Logique d'interaction pour ConsultationAdherentsUC.xaml
	/// </summary>
	public partial class ConsultationAdherentsUC : UserControl
	{
		public ConsultationAdherentsUC() {
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
		}

		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
	}
}
