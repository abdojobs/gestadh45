using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Main.UserControls.Consultation
{
	/// <summary>
	/// Logique d'interaction pour ConsultationAdherentsUC.xaml
	/// </summary>
	public partial class ConsultationAdherentsUC : UserControl
	{
		public ConsultationAdherentsUC() {
			InitializeComponent();

			Messenger.Default.Register<NotificationMessage>(this, (msg) => this.EffacerFiltre());
		}

		private void EffacerFiltre() {
			this.tbxFiltre.Text = string.Empty;
		}
	}
}
