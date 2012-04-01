using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{	
	public class NMActionFileDialog <TCallbackParameter> : NotificationMessageAction<TCallbackParameter>
	{
		/// <summary>
		/// Obtient/Définit l'extension du fichier
		/// </summary>
		public string ExtensionFichier {
			get;
			set;
		}

		/// <summary>
		/// Obtient/Définit le nom du fichier
		/// </summary>
		public string NomFichier {
			get;
			set;
		}
		
		public NMActionFileDialog(string extensionFichier, string nomFichier, Action<TCallbackParameter> callback) 
			: base(NMType.NMActionFileDialog, callback) {
				this.ExtensionFichier = extensionFichier;
				this.NomFichier = nomFichier;
		}
	}
}
