using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageFermetureFenetre : NotificationMessage
	{
		public NotificationMessageFermetureFenetre() 
			: base(TypesNotification.FermetureFenetre) {
		}
	}
}
