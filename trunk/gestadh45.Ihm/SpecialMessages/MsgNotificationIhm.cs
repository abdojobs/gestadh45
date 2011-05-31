using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.ObjetsIhm;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgNotificationIhm : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit la liste des notifications
		/// </summary>
		public List<NotificationIhm> Contenu { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotification">Notification à afficher sur l'ihm</param>
		public MsgNotificationIhm(NotificationIhm pNotification)
			: base(TypesNotification.NotificationIhm) {
			
			this.Contenu = new List<NotificationIhm>();
			this.Contenu.Add(pNotification);
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotifications">Liste de Notifications à afficher sur l'ihm</param>
		public MsgNotificationIhm(List<NotificationIhm> pNotifications)
			: base(TypesNotification.NotificationIhm) {
				this.Contenu = pNotifications;
		}
	}
}
