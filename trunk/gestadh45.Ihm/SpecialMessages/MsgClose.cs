using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgClose : NotificationMessage
	{
		public MsgClose() : base(TypesNotification.NotificationClose) {}
	}
}
