using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class MsgAfficherUC : NotificationMessage
	{
		public enum TypeAffichage
		{
			Interne,	// à l'intérieur de l'écran principal
			Externe		// dans une fenêtre indépendante
		}

		/// <summary>
		/// Type d'ouverture de l'UC
		/// </summary>
		public enum TypeOuverture
		{
			Creation,	// l'UC sera ouvert en mode création
			Edition		// l'UC sera ouvert en mode édition (par défaut)
		}

		/// <summary>
		/// Obtient/Définit le type d'affichage de l'UC
		/// </summary>
		public TypeAffichage ModeAffichage { get; set; }

		/// <summary>
		/// Obtient/Définit le type d'ouverture
		/// </summary>
		public TypeOuverture ModeOuverture { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pCodeUC">Code de l'UC à afficher</param>
		/// <param name="pTYpe">Type d'affichage de l'UC</param>
		public MsgAfficherUC(string pCodeUC, TypeAffichage pTYpe) : base(pCodeUC) {
			this.ModeAffichage = pTYpe;
		}
	}

	public class MsgAfficherUC<T> : MsgAfficherUC
	{
		/// <summary>
		/// Obtient / Définit l'objet à envoyer à l'UC
		/// </summary>
		public T Element { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pCodeUC">Code de l'UC</param>
		/// <param name="pType">Type d'affichage de l'UC</param>
		/// <param name="pElement">Objet à passer en paramètre de l'UC</param>
		public MsgAfficherUC(string pCodeUC, TypeAffichage pType, T pElement)
			: base(pCodeUC, pType) {
			this.Element = pElement;
			this.ModeOuverture = TypeOuverture.Edition;
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pCodeUC">Code de l'UC</param>
		/// <param name="pType">Type d'affichage de l'UC</param>
		/// <param name="pElement">Objet à passer en paramètre de l'UC</param>
		/// <param name="pMode">Mode d'ouverture</param>
		public MsgAfficherUC(string pCodeUC, TypeAffichage pType, T pElement, TypeOuverture pMode)
			: base(pCodeUC, pType) {
			this.Element = pElement;
			this.ModeOuverture = pMode;
		}
	}
}
