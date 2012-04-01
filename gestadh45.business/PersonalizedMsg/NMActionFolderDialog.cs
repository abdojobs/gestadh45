using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMActionFolderDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		public NMActionFolderDialog(Action<TCallbackParameter> callback)
			: base(NMType.NMActionFolderDialog, callback) {
		}
	}
}
