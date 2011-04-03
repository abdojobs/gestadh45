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

	public class NotificationMessageChangementUC<T> : NotificationMessageChangementUC
	{
		/// <summary>
		/// Obtient / Définit l'objet à envoyer à l'UC
		/// </summary>
		public T Element { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pCodeUC">Code de l'UC</param>
		/// <param name="pElement">Objet à passer en paramètre de l'UC</param>
		public NotificationMessageChangementUC(string pCodeUC, T pElement) 
			: base(pCodeUC) {
				this.Element = pElement;
		}
	}
}
