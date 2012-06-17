using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMShowInfosDataSource : NotificationMessage<string>
	{
		public NMShowInfosDataSource(string infosDataSource) : base(infosDataSource, NMType.NMShowInfosDataSource) { }
	}
}
