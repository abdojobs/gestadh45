using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	/// <summary>
	/// Message de notification particulier permettant d'appeler le formulaire associé à l'écran de consultation
	/// </summary>
	/// <typeparam name="T">Code du formulaire</typeparam>
	/// <typeparam name="U">Objet de contenu du formulaire</typeparam>
	public class NotificationMessageFormulaire<T, U> : NotificationMessage<T>
	{
		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pContent">Type de notification</param>
		/// <param name="pNotification">Code UC du formulaire</param>
		/// <param name="pElement">Objet de contenu</param>
		public NotificationMessageFormulaire(T pContent, string pNotification, U pElement)
			: base(pContent, pNotification) {
			this.Element = pElement;
		}

		/// <summary>
		/// Objet de contenu du formulaire
		/// </summary>
		public U Element { get; set; }
	}
}
