using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Main.UserControls.Adherents
{
	/// <summary>
	/// Logique d'interaction pour ConsultationAdherentsUC.xaml
	/// </summary>
	public partial class ConsultationAdherentsUC : UserControl
	{
		public ConsultationAdherentsUC() {
			InitializeComponent();
			
			Messenger.Default.Register<NotificationMessage>(
				this, 
				(msg) => this.EffacerFiltre()
			);
		}

		public ConsultationAdherentsUC(Adherent pAdherent) {
			InitializeComponent();

			Messenger.Default.Register<NotificationMessage>(
				this,
				(msg) => this.EffacerFiltre()
			);

			// Envoi du message déclenchant la sélection de l'adhérent
			Messenger.Default.Send(new MsgSelectionElement<Adherent>(pAdherent));
		}

		private void EffacerFiltre() {
			this.tbxFiltre.Clear();
		}
	}
}
