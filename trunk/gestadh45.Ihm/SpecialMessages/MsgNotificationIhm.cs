using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.ObjetsIhm;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgNotificationIhm : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit la notification
		/// </summary>
		public NotificationIhm Contenu { get; set; }
		
		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotification">Notification à afficher sur l'ihm</param>
		public MsgNotificationIhm(NotificationIhm pNotification)
			: base(TypesNotification.NotificationIhm) {
				this.Contenu = pNotification;
		}
	}
}
