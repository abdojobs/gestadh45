using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageSelectionElement<T> : NotificationMessage<T>
	{
		public NotificationMessageSelectionElement(T elem) 
			: base (elem, TypesNotification.SelectionElement) {}
	}
}
