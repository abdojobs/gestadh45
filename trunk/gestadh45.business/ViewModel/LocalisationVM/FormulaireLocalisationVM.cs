using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.LocalisationVM
{
	public partial class FormulaireLocalisationVM : VMFormulaireBase
	{
		#region CurrentLocalisation
		private Localisation _currentLocalisation;

		/// <summary>
		/// Obtient/Définit la localisation courante
		/// </summary>
		public Localisation CurrentLocalisation {
			get { return this._currentLocalisation; }
			set {
				if (this._currentLocalisation != value) {
					this._currentLocalisation = value;
					this.RaisePropertyChanged(() => this.CurrentLocalisation);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Localisation> _repoLocalisation;
		#endregion

		#region Constructeurs
		public FormulaireLocalisationVM() {
			this._repoLocalisation = new Repository<Localisation>(this._context);
			this.CurrentLocalisation = new Localisation();
			this.UCParentCode = CodesUC.ConsultationLocalisations;
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentLocalisation.ID = Guid.NewGuid();
				this._repoLocalisation.Add(this.CurrentLocalisation);
				this._repoLocalisation.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentLocalisation.Libelle)) {
				errors.Add(ResLocalisation.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResLocalisation.ErrLocalisationExiste);
			}

			return errors.Count == 0;
		}

		protected override bool CurrentElementExists() {
			return this._repoLocalisation.GetAll().Where(
					l => l.Libelle.Equals(this.CurrentLocalisation.Libelle, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentLocalisation.Libelle = (this.CurrentLocalisation.Libelle == null) ? null : this.CurrentLocalisation.Libelle.ToUpperInvariant();
		}
	}
}
