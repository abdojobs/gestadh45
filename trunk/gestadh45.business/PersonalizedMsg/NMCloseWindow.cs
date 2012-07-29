using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMCloseWindow : NotificationMessage
	{
		/// <summary>
		/// Gets the UC GUID.
		/// </summary>
		public Guid UCGuid { get; internal set; }
		
		public NMCloseWindow(Guid ucGuid) : base(NMType.NMCloseWindow) {
			this.UCGuid = ucGuid;
		}
	}
}
