using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgSelectionElement<T> : NotificationMessage<T>
	{
		public MsgSelectionElement(T elem) 
			: base (elem, TypesNotification.SelectionElement) {}
	}
}
