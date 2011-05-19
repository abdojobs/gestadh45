using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageIhm : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le type de notification
		/// </summary>
		public string TypeNotificationIhm { get; set; }
		
		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotification">Notification à afficher sur l'ihm</param>
		public NotificationMessageIhm(string pType, string pNotification)
			: base(pNotification) {
				this.TypeNotificationIhm = pType;
		}
	}
}
