using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageException : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant si il faut quitter l'application suite à cette exception
		/// </summary>
		public bool QuitterApplication { get; set; }

		/// <summary>
		/// Obtient/Définit l'exception
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pException">Exception</param>
		/// <param name="pQuitterApplication">Booléen indiquant si l'exception nécessite de quitter l'application</param>
		public NotificationMessageException(Exception pException, bool pQuitterApplication) 
			: base(TypesNotification.Exception) {

			this.Exception = pException;
			this.QuitterApplication = pQuitterApplication;

		}
	}
}
