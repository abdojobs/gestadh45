using System;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMOpenWindow : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC à ouvrir en mode fenêtre
		/// </summary>
		public string CodeUC { get; set; }

		/// <summary>
		/// GUID de l'UC qui a demandé l'ouverture de la fenêtre
		/// </summary>
		public Guid ParentGuid { get; set; }
		
		public NMOpenWindow(Tuple<string, Guid> parameters) : base(NMType.NMOpenWindow) {
			this.CodeUC = parameters.Item1;
			this.ParentGuid = parameters.Item2;
		}
	}
}
