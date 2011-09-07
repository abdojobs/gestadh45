using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageOuvertureFenetre : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à afficher dans la fenêtre
		/// </summary>
		public string CodeUC { get; set; }
		
		public NotificationMessageOuvertureFenetre(string pCodeUC) 
			: base(TypesNotification.OuvertureFenetre) {
				this.CodeUC = pCodeUC;
		}
	}
}
