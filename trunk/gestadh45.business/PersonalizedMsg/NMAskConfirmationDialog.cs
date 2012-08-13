using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMAskConfirmationDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		public string Text { get; set; }
		
		public NMAskConfirmationDialog(Action<TCallbackParameter> callback, string text)
			: base(NMType.NMAskConfirmationDialog, callback) {

				this.Text = text;
		}
	}
}
