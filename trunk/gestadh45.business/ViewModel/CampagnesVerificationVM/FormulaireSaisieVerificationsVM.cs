using System;
using System.Linq;
using gestadh45.dal;
using System.Collections.Generic;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireSaisieVerificationsVM : VMFormulaireBase
	{
		#region CurrentCampagneVerification
		private CampagneVerification _currentCampagneVerification;

		/// <summary>
		/// Obtient/Définit la campagne de vérification courante
		/// </summary>
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

		#region StatutsVerification
		private IOrderedEnumerable<StatutVerification> _statutsVerifications;

		/// <summary>
		/// Obtient/Définit la liste des statuts de vérification
		/// </summary>
		public IOrderedEnumerable<StatutVerification> StatutsVerification {
			get { return this._statutsVerifications; }
			set {
				if (this._statutsVerifications != value) {
					this._statutsVerifications = value;
					this.RaisePropertyChanged(() => this.StatutsVerification);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<CampagneVerification> _repoCampagneVerification;
		private Repository<StatutVerification> _repoStatutsVerification;
		#endregion

		#region Constructeur
		public FormulaireSaisieVerificationsVM(Guid idCampagne) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagne);
			this.PopulateCombos();
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
			this._repoStatutsVerification = new Repository<StatutVerification>(this._context);
		}

		private void PopulateCombos() {			
			this.StatutsVerification = this._repoStatutsVerification.GetAll().OrderBy(s => s.EstDefaut).ThenBy(s => s.Libelle);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			foreach (Verification verif in this.CurrentCampagneVerification.Verifications) {
				if (verif.StatutVerification.EstCommentaireObligatoire && string.IsNullOrWhiteSpace(verif.Commentaire)) {
					errors.Add(string.Format(ResCampagnesVerification.ErrCommentaireObligatoire, verif.Equipement.Libelle));
				}
			}

			return errors.Count == 0;
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
				this._repoCampagneVerification.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
