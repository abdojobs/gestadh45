using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.MarquesVM
{
	public class FormulaireMarqueVM : VMFormulaireBase
	{
		#region CurrentMarque
		private Marque _currentMarque;

		/// <summary>
		/// Gets or sets the current marque.
		/// </summary>
		/// <value>
		/// The current marque.
		/// </value>
		public Marque CurrentMarque {
			get {
				return this._currentMarque;
			}

			set {
				if (this._currentMarque != value) {
					this._currentMarque = value;
					this.RaisePropertyChanged(() => this.CurrentMarque);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<Marque> _repoMarque;
		#endregion

		#region Constructeur
		public FormulaireMarqueVM() {
			this._repoMarque = new Repository<Marque>(this._context);
			this.CurrentMarque = new Marque();
			this.UCParentCode = CodesUC.ConsultationMarques;
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentMarque.ID = Guid.NewGuid();
				this._repoMarque.Add(this.CurrentMarque);
				this._repoMarque.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentMarque.Libelle)) {
				errors.Add(ResMarques.ErrLibelleObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResMarques.ErrMarqueExiste);
			}

			return errors.Count == 0;
		}

		protected override bool CurrentElementExists() {
			return this._repoMarque.GetAll().Where(
					(m) => m.Libelle.Equals(this.CurrentMarque.Libelle, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentMarque.Libelle = (this.CurrentMarque.Libelle == null) ? null : this.CurrentMarque.Libelle.ToUpperInvariant();
		}
	}
}
