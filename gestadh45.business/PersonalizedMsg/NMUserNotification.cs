using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMUserNotification : NotificationMessage
	{
		public string Text { get; set; }

		public NMUserNotification(string notification) : base(NMType.NMUserNotification) {
			this.Text = notification;
		}
	}
}
