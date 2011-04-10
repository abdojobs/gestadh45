using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageConsultationExtractions : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le contenu de l'extraction à afficher
		/// </summary>
		public string ResultatExtraction { get; set; }

		public NotificationMessageConsultationExtractions(string pResultatExtraction) 
			: base(TypesNotification.AfficherExtraction) {
				this.ResultatExtraction = pResultatExtraction;
		}
	}
}
