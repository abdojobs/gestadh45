﻿
namespace gestadh45.business.ViewModel
{
	public abstract class VMUCBase : VMApplicationBase
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC parent
		/// </summary>
		public string UCParentCode { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'UC s'affiche dans une fenêtre (True) ou dans l'écran principal
		/// </summary>
		public bool IsWindowMode { get; set; }

		/// <summary>
		/// Constructeur définissant l'UC par défaut à afficher (Consultation infos club)
		/// </summary>
		public VMUCBase() {
			this.UCParentCode = CodesUC.ConsultationInfosClub;
		}
	}
}
