using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.VillesVM
{
	public class FormulaireVilleVM : VMFormulaireBase
	{
		#region CurrentVille
		private Ville _currentVille;

		/// <summary>
		/// Obtient/Définit la ville du formulaire
		/// </summary>
		public Ville CurrentVille {
			get { return this._currentVille; }
			set {
				if (this._currentVille != value) {
					this._currentVille = value;
					this.RaisePropertyChanged(() => this.CurrentVille);
				}
			}
		}
		#endregion

		#region repository
		private Repository<Ville> repoVille;
		#endregion

		public FormulaireVilleVM() {
			this.repoVille = new Repository<Ville>(this._context);
			this.CurrentVille = new Ville();
			this.UCParentCode = CodesUC.ConsultationVilles;
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentVille.ID = Guid.NewGuid();
				this.repoVille.Add(this.CurrentVille);
				this.repoVille.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}			
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentVille.Libelle)) {
				errors.Add(ResVilles.ErrLibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.CurrentVille.CodePostal)) {
				errors.Add(ResVilles.ErrCodePostalObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResVilles.ErrVilleExiste);
			}

			return errors.Count == 0;
		}

		protected override bool CurrentElementExists() {
			return this.repoVille.GetAll().Where(
				(v) => v.Libelle.Equals(this.CurrentVille.Libelle, StringComparison.OrdinalIgnoreCase) 
					&& v.CodePostal.Equals(this.CurrentVille.CodePostal, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentVille.Libelle = (this.CurrentVille.Libelle == null) ? null : this.CurrentVille.Libelle.ToUpperInvariant();
			this.CurrentVille.CodePostal = (this.CurrentVille.CodePostal == null) ? null : this.CurrentVille.CodePostal.ToUpperInvariant();
		}
	}
}
