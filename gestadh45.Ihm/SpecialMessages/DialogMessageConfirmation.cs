using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class DialogMessageConfirmation : DialogMessage
	{
		public DialogMessageConfirmation(string content, Action<MessageBoxResult> callback)
			: base(content, callback) {
			base.Button = MessageBoxButton.OKCancel;
			base.Caption = ResMessages.TitreConfirmation;
		}
	}
}
