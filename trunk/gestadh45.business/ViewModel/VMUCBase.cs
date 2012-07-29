
using System;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
namespace gestadh45.business.ViewModel
{
	public abstract class VMUCBase : VMApplicationBase
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC parent
		/// </summary>
		public string UCParentCode { get; set; }

		public Guid UCGuid { get; internal set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'UC s'affiche dans une fenêtre (True) ou dans l'écran principal
		/// </summary>
		public bool IsWindowMode { get; set; }

		protected gestadh45Entities _context;

		/// <summary>
		/// Constructeur définissant l'UC par défaut à afficher (Consultation infos club)
		/// </summary>
		public VMUCBase() {
			this._context = new gestadh45Entities();
			this.UCParentCode = CodesUC.ConsultationInfosClub;
			this.UCGuid = Guid.NewGuid();

			// envoi du message d'affichage du datasource
			Messenger.Default.Send(new NMShowInfosDataSource(this._context.Database.Connection.ConnectionString));
		}
	}
}
