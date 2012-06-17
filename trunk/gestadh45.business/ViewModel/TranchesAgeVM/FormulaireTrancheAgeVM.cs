using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.TranchesAgeVM
{
	public class FormulaireTrancheAgeVM : VMFormulaireBase
	{
		#region CurrentTrancheAge
		private TrancheAge _currentTrancheAge;

		/// <summary>
		/// Obtient/Définit la tranche d'âge courante du formulaire
		/// </summary>
		public TrancheAge CurrentTrancheAge {
			get { return this._currentTrancheAge; }
			set {
				if (this._currentTrancheAge != value) {
					this._currentTrancheAge = value;
					this.RaisePropertyChanged(() => this.CurrentTrancheAge);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<TrancheAge> _repoTrancheAge;
		#endregion

		#region Constructeurs
		public FormulaireTrancheAgeVM() {
			this._repoTrancheAge = new Repository<TrancheAge>(this._context);

			this.UCParentCode = CodesUC.ConsultationTranchesAge;
			this.IsEditMode = false;

			this.CurrentTrancheAge = new TrancheAge();
		}

		/// <summary>
		/// Constructeur pour le mode édition. On doit passer par l'id pour récupérer la tranche d'âge car on a changé de contexte.
		/// </summary>
		/// <param name="idAdherent">ID de la tranche d'âge à éditer</param>
		public FormulaireTrancheAgeVM(Guid idTrancheAge) {
			this._repoTrancheAge = new Repository<TrancheAge>(this._context);
			
			this.UCParentCode = CodesUC.ConsultationTranchesAge;
			this.IsEditMode = true;

			this.CurrentTrancheAge = this._repoTrancheAge.GetByKey(idTrancheAge);
		}
		#endregion

		/// <summary>
		/// Vérifie que cette tranche d'âge n'en chevauche pas une déjà existante
		/// </summary>
		/// <returns>Booléen indiquant si la tranche d'âge existe déjà ou non</returns>
		protected override bool CurrentElementExists() {
			return this._repoTrancheAge.GetAll().Count(t =>
						(this.CurrentTrancheAge.AgeInf >= t.AgeInf && this.CurrentTrancheAge.AgeInf <= t.AgeSup)
						||
						(this.CurrentTrancheAge.AgeSup >= t.AgeInf && this.CurrentTrancheAge.AgeSup <= t.AgeSup)
					) != 0;
		}

		protected override bool CheckFormValidity(List<string> errors) {
			// on vérifie que l'age de début soit positif
			if (this.CurrentTrancheAge.AgeInf < 0) {
				errors.Add(ResTranchesAge.TrancheAge_AgeInfPositif);
			}

			// on vérifie que l'âge de fin soit positif
			if (this.CurrentTrancheAge.AgeSup < 0) {
				errors.Add(ResTranchesAge.TrancheAge_AgeSupPositif);
			}

			// on vérifie que l'âge de fin soit supérieur à l'âge de début
			if (errors.Count == 0 && this.CurrentTrancheAge.AgeSup < this.CurrentTrancheAge.AgeInf) {
				errors.Add(ResTranchesAge.TrancheAge_OrdreAges);
			}

			// on vérifie que la tranche n'existe pas déjà
			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResTranchesAge.TrancheAge_Existe);
			}

			return errors.Count == 0;
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet CurrentTrancheAge avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (this.IsEditMode) {
				this._repoTrancheAge.Reload(this.CurrentTrancheAge);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {

				if (this.IsEditMode) {
					this._repoTrancheAge.Edit(this.CurrentTrancheAge);
				}
				else {
					this.CurrentTrancheAge.ID = Guid.NewGuid();
					this._repoTrancheAge.Add(this.CurrentTrancheAge);
				}

				this._repoTrancheAge.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
