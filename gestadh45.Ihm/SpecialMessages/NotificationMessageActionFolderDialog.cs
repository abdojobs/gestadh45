using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageActionFolderDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		public NotificationMessageActionFolderDialog(Action<TCallbackParameter> callback)
			: base(TypesNotification.FolderBrowserDialog, callback) {
		}
	}
}
