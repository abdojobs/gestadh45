using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMUserNotification : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le texte de la notification
		/// </summary>
		public string Text { get; set; }

		public NMUserNotification(string notification) : base(NMType.NMUserNotification) {
			this.Text = notification;
		}
	}
}
