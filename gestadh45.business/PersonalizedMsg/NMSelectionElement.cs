using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMSelectionElement<TElement> : NotificationMessage<TElement>
	{
		public NMSelectionElement(TElement element) : base(element, NMType.NMSelectionElement){}
	}
}
