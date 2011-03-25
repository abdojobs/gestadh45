using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageChangementUC : NotificationMessage
	{
		/// <summary>
		/// Code de l'UC à afficher
		/// </summary>
		public string CodeUC { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pCodeUC">Code de l'UC à afficher</param>
		public NotificationMessageChangementUC(string pCodeUC)
			: base(TypesNotification.ChangementUC) {
				this.CodeUC = pCodeUC;
		}
	}
}
