using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageActionFileDialog<TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		public NotificationMessageActionFileDialog(string notification, string pExtensionFichier, string pNomFichier, Action<TCallbackParameter> callback)
			: base(notification, callback) {
			this.ExtensionFichier = pExtensionFichier;
			this.NomFichier = pNomFichier;
		}

		public NotificationMessageActionFileDialog(object sender, string notification, string pExtensionFichier, string pNomFichier, Action<TCallbackParameter> callback)
			: base(sender, notification, callback) {
			this.ExtensionFichier = pExtensionFichier;
			this.NomFichier = pNomFichier;
		}

		public NotificationMessageActionFileDialog(object sender, object target, string notification, string pExtensionFichier, string pNomFichier, Action<TCallbackParameter> callback)
			: base(sender, target, notification, callback) {
			this.ExtensionFichier = pExtensionFichier;
			this.NomFichier = pNomFichier;
		}

		/// <summary>
		/// Obtient/Définit l'extension du fichier
		/// </summary>
		public string ExtensionFichier { get; set; }

		/// <summary>
		/// Obtient/Définit le nom du fichier
		/// </summary>
		public string NomFichier { get; set; }
	}
}
