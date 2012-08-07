using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireCreationCampagneVerificationVM : VMFormulaireBase
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

		#region Repositories
		private Repository<CampagneVerification> _repoCampagnesVerification;
		private Repository<Verification> _repoVerification;
		private Repository<Equipement> _repoEquipement;
		private Repository<StatutVerification> _repoStatutVerification;
		#endregion

		#region Constructeur
		public FormulaireCreationCampagneVerificationVM() {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.CurrentCampagneVerification = new CampagneVerification();
			this.CurrentCampagneVerification.Date = DateTime.Now;
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagnesVerification = new Repository<CampagneVerification>(this._context);
			this._repoEquipement = new Repository<Equipement>(this._context);
			this._repoVerification = new Repository<Verification>(this._context);
			this._repoStatutVerification = new Repository<StatutVerification>(this._context);
		}

		/// <summary>
		/// Créé une vérification pour chaque équipement qui n'est pas au rebut
		/// </summary>
		private void CreateVerifications() {
			var defautStatut = this._repoStatutVerification.GetAll().Where(s => s.EstDefaut).FirstOrDefault();

			foreach (var equip in this._repoEquipement.GetAll().Where(e => !e.EstAuRebut)) {
				var verif = new Verification()
				{
					ID = Guid.NewGuid(),
					CampagneVerification = this.CurrentCampagneVerification,
					Equipement = equip,
					StatutVerification = defautStatut
				};

				this._repoVerification.Add(verif);
			}

			this._repoVerification.Save();
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentCampagneVerification.Responsable)) {
				errors.Add(ResCampagnesVerification.ErrResponsableObligatoire);
			}

			return errors.Count == 0;
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentCampagneVerification.ID = Guid.NewGuid();
				this._repoCampagnesVerification.Add(this.CurrentCampagneVerification);
				this._repoCampagnesVerification.Save();

				this.CreateVerifications();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
