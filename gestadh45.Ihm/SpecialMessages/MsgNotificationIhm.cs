using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.ObjetsIhm;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgNotificationIhm : NotificationMessage
	{
		public enum ModeAffichage
		{
			Ajout,			// la notification sera ajoutée à la suite des autres
			Remplacement	// la liste sera effacée avant d'ajouter la notification
		}
		
		/// <summary>
		/// Obtient/Définit la notification
		/// </summary>
		public NotificationIhm Contenu { get; set; }

		/// <summary>
		/// Obtient/Définit le mode d'affichage de la notification
		/// </summary>
		public ModeAffichage Mode { get; set; }
		
		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotification">Notification à afficher sur l'ihm</param>
		public MsgNotificationIhm(NotificationIhm pNotification)
			: base(TypesNotification.NotificationIhm) {
			this.Contenu = pNotification;
			this.Mode = ModeAffichage.Ajout;
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pNotification">Notification à afficher sur l'ihm</param>
		/// <param name="pMode">Mode d'affichage</param>
		public MsgNotificationIhm(NotificationIhm pNotification, ModeAffichage pMode)
			: base(TypesNotification.NotificationIhm) {
			this.Contenu = pNotification;
			this.Mode = pMode;
		}
	}
}
