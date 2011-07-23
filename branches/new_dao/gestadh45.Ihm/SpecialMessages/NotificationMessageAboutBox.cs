using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageAboutBox : NotificationMessage
	{
		public NotificationMessageAboutBox()
			: base(TypesNotification.AboutBox) {
		}
	}
}
