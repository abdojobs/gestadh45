using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireCampagneVerificationVM : VMFormulaireBase
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

		#region EquipementsInclus
		private ObservableCollection<Equipement> _equipementsInclus;

		/// <summary>
		/// Gets or sets the equipements inclus.
		/// </summary>
		/// <value>
		/// The equipements inclus.
		/// </value>
		public ObservableCollection<Equipement> EquipementsInclus {
			get { return this._equipementsInclus; }
			set {
				if (this._equipementsInclus != value) {
					this._equipementsInclus = value;
					this.RaisePropertyChanged(() => this.EquipementsInclus);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<CampagneVerification> _repoCampagneVerification;
		private Repository<Equipement> _repoEquipement;
		private Repository<StatutVerification> _repoStatutsVerifications;
		private Repository<Verification> _repoVerifications;
		#endregion

		#region constructeurs
		public FormulaireCampagneVerificationVM() {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = false;

			this.CreateRepositories();

			this.CurrentCampagneVerification = new CampagneVerification()
			{
				Date = DateTime.Now
			};

			this.EquipementsInclus = new ObservableCollection<Equipement>(
				this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).ToList()
			);
		}

		/// <summary>
		/// Constructeur pour le mode édition
		/// </summary>
		/// <param name="idDureeDeVie">ID de l'objet à éditer</param>
		public FormulaireCampagneVerificationVM(Guid idCampagneVerification) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this.CreateRepositories();

			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagneVerification);

			this.EquipementsInclus = new ObservableCollection<Equipement>();
			foreach (var verif in this.CurrentCampagneVerification.Verifications) {
				this.EquipementsInclus.Add(verif.Equipement);
			}
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
			this._repoEquipement = new Repository<Equipement>(this._context);
			this._repoStatutsVerifications = new Repository<StatutVerification>(this._context);
			this._repoVerifications = new Repository<Verification>(this._context);
		}

		protected override bool CheckFormValidity(List<string> errors) {			
			if (string.IsNullOrWhiteSpace(this.CurrentCampagneVerification.Libelle)) {
				errors.Add(ResCampagnesVerification.ErrLibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.CurrentCampagneVerification.Responsable)) {
				errors.Add(ResCampagnesVerification.ErrResponsableObligatoire);
			}

			if (!this.IsEditMode && this.EquipementsInclus.Count == 0) {
				errors.Add(ResCampagnesVerification.ErrAucunEquipement);
			}

			return errors.Count == 0;
		}

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
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				if (this.IsEditMode) {
					// enregistrement des modifs de la campagne
					this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
					this._repoCampagneVerification.Save();
				}
				else {
					// enregistrement de la campagne
					this.CurrentCampagneVerification.ID = Guid.NewGuid();
					this._repoCampagneVerification.Add(this.CurrentCampagneVerification);
					this._repoCampagneVerification.Save();

					// création des vérifications
					var statutDefaut = this._repoStatutsVerifications.GetAll().Where(s => s.EstDefaut).FirstOrDefault();

					foreach (Equipement equip in this.EquipementsInclus) {
						var verif = new Verification()
						{
							ID = Guid.NewGuid(),
							CampagneVerification = this.CurrentCampagneVerification,
							Equipement = equip,
							StatutVerification = statutDefaut
						};

						this._repoVerifications.Add(verif);
					}

					this._repoVerifications.Save();
				}	

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
