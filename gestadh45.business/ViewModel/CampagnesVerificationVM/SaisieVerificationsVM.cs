using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class SaisieVerificationsVM : VMFormulaireBase
	{
		#region CurrentCampagneVerification
		private CampagneVerification _currentCampagneVerification;

		/// <summary>
		/// Gets or sets the current campagne verification.
		/// </summary>
		/// <value>
		/// The current campagne verification.
		/// </value>
		public CampagneVerification CurrentCampagneVerification {
			get { return this._currentCampagneVerification; }
			set {
				if (this._currentCampagneVerification != value) {
					this._currentCampagneVerification = value;
					this.RaisePropertyChanged(() => this.CurrentCampagneVerification);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<CampagneVerification> _repoCampagneVerification;
		#endregion

		#region Constructeur
		public SaisieVerificationsVM(Guid idCampagneVerification) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);

			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagneVerification);
		}
		#endregion

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet courant avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (this.IsEditMode) {
				this._repoCampagneVerification.Reload(this.CurrentCampagneVerification);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
			this._repoCampagneVerification.Save();
			
			base.ExecuteSaveCommand();
		}
		#endregion
	}
}
