using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageUtilisateur : NotificationMessage
	{
		/// <summary>
		/// Message à afficher à l'utilisateur
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pType">Type de notification utilisateur</param>
		/// <param name="pMessage">Message à afficher</param>
		public NotificationMessageUtilisateur(string pType, string pMessage)
			: base(pType) {
			this.Message = pMessage;
		}
	}
}
