using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMCloseApplication : NotificationMessage
	{
		public NMCloseApplication() : base(NMType.NMCloseApplication) { }
	}
}
