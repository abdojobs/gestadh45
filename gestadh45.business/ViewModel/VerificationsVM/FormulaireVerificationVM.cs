using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.VerificationsVM
{
	public class FormulaireVerificationVM : VMFormulaireBase
	{
		#region Equipements
		private IOrderedEnumerable<Equipement> _equipements;

		/// <summary>
		/// Gets or sets the equipements.
		/// </summary>
		/// <value>
		/// The equipements.
		/// </value>
		public IOrderedEnumerable<Equipement> Equipements {
			get { return this._equipements; }
			set {
				if (this._equipements != value) {
					this._equipements = value;
					this.RaisePropertyChanged(() => this.Equipements);
				}
			}
		}
		#endregion

		#region CurrentVerification
		private Verification _currentVerification;

		/// <summary>
		/// Gets or sets the current verification.
		/// </summary>
		/// <value>
		/// The current verification.
		/// </value>
		public Verification CurrentVerification {
			get { return this._currentVerification; }
			set {
				if (this._currentVerification != value) {
					this._currentVerification = value;
					this.RaisePropertyChanged(() => this.CurrentVerification);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<Equipement> _repoEquipement;
		private Repository<Verification> _repoVerification;
		private Repository<Saison> _repoSaison;
		#endregion

		#region Constructeur
		public FormulaireVerificationVM() {
			this._repoEquipement = new Repository<Equipement>(this._context);
			this._repoVerification = new Repository<Verification>(this._context);
			this._repoSaison = new Repository<Saison>(this._context);

			this.UCParentCode = CodesUC.ConsultationVerifications;

			this.PopulateCombos();

			this.CurrentVerification = new Verification();
			this.CurrentVerification.Saison = this._repoSaison.GetAll().Where(s => s.EstSaisonCourante).FirstOrDefault();
			this.CurrentVerification.EtatOk = true;
			this.CurrentVerification.DateVerification = DateTime.Now;
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentVerification.ID = Guid.NewGuid();
				this._repoVerification.Add(this.CurrentVerification);
				this._repoVerification.Save();

				// si EtatOk == false => on met au rebut automatiquement
				if (!this.CurrentVerification.EtatOk) {
					this.CurrentVerification.Equipement.DateMiseAuRebut = DateTime.Now;
					this._repoEquipement.Edit(this.CurrentVerification.Equipement);
					this._repoEquipement.Save();
				}

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (this.CurrentVerification.Equipement == null) {
				errors.Add(ResVerifications.ErrEquipementObligatoire);
			}

			// le commentaire est obligatoire en cas de mise au rebut
			if (!this.CurrentVerification.EtatOk 
				&& string.IsNullOrWhiteSpace(this.CurrentVerification.Commentaire)) {
					errors.Add(ResVerifications.ErrCommentaireRebutObligatoire);
			}

			return errors.Count == 0;
		}

		private void PopulateCombos() {
			// on ne proposera que les équipements qui ne sont pas au rebut
			this.Equipements = this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).OrderBy(e => e.ToString());
		}
	}
}
