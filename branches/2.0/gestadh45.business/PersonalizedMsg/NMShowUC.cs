using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMShowUC : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à afficher
		/// </summary>
		public string CodeUC { get; set; }

		public NMShowUC(string codeUC) : base(NMType.NMShowUC) {
			this.CodeUC = codeUC;
		}
	}

	public class NMShowUC<TContent> : NotificationMessage<TContent>
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à afficher
		/// </summary>
		public string CodeUC { get; set; }

		public NMShowUC(string codeUC, TContent content)
			: base(content, NMType.NMShowUC) {
				this.CodeUC = codeUC;
		}
	}
}
