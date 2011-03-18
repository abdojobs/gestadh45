using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageErreur : NotificationMessage
	{
		public NotificationMessageErreur(string notification, string pMessage)
			: base(notification) {
			this.Message = pMessage;
		}

		public NotificationMessageErreur(object sender, string notification, string pMessage)
			: base(sender, notification) {
			this.Message = pMessage;
		}

		public NotificationMessageErreur(object sender, object target, string notification, string pMessage)
			: base(sender, target, notification) {
			this.Message = pMessage;
		}

		public string Message { get; set; }
	}
}
