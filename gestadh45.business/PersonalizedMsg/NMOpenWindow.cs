using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMOpenWindow : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à ouvrir en mode fenêtre
		/// </summary>
		public string CodeUC { get; set; }
		
		public NMOpenWindow(string codeUC) : base(NMType.NMOpenWindow) {
			this.CodeUC = codeUC;
		}
	}
}
