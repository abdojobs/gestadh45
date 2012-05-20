using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CategoriesVM
{
	public class FormulaireCategorieVM : VMFormulaireBase
	{
		#region CurrentCategorie
		private Categorie _currentCategorie;

		/// <summary>
		/// Gets or sets the current categorie.
		/// </summary>
		/// <value>
		/// The current categorie.
		/// </value>
		public Categorie CurrentCategorie {
			get {
				return this._currentCategorie;
			}

			set {
				if (this._currentCategorie != value) {
					this._currentCategorie = value;
					this.RaisePropertyChanged(() => this.CurrentCategorie);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<Categorie> _repoCategorie;
		#endregion

		#region Constructeur
		public FormulaireCategorieVM() {
			this._repoCategorie = new Repository<Categorie>(this._context);
			this.CurrentCategorie = new Categorie();
			this.UCParentCode = CodesUC.ConsultationCategories;
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentCategorie.ID = Guid.NewGuid();
				this._repoCategorie.Add(this.CurrentCategorie);
				this._repoCategorie.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentCategorie.Libelle)) {
				errors.Add(ResCategories.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResCategories.ErrCategorieExiste);
			}

			return errors.Count == 0;
		}

		protected override bool CurrentElementExists() {
			return this._repoCategorie.GetAll().Where(
					(c) => c.Libelle.Equals(this.CurrentCategorie.Libelle, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentCategorie.Libelle = (this.CurrentCategorie.Libelle == null) ? null : this.CurrentCategorie.Libelle.ToUpperInvariant();
		}
	}
}
