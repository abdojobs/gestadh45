using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageFormulaire<T, U> : NotificationMessage<T>
	{
		public NotificationMessageFormulaire(T pContent, string pNotification, U pElement)
			: base(pContent, pNotification) {
			this.Element = pElement;
		}

		public NotificationMessageFormulaire(T pContent, object pSender, string pNotification, U pElement)
			: base(pSender, pContent, pNotification) {
			this.Element = pElement;
		}

		public NotificationMessageFormulaire(T pContent, object pSender, object pTarget, string pNotification, U pElement)
			: base(pSender, pTarget, pContent, pNotification) {
			this.Element = pElement;
		}

		public U Element { get; set; }
	}
}
