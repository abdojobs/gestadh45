using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.GroupesVM
{
	public class FormulaireGroupeVM : VMFormulaireBase
	{
		#region JoursSemaine
		private IOrderedEnumerable<JourSemaine> _joursSemaine;

		/// <summary>
		/// Obtient/Définit la liste des jours de la semaine
		/// </summary>
		public IOrderedEnumerable<JourSemaine> JoursSemaine {
			get { return this._joursSemaine; }
			set {
				if (this._joursSemaine != value) {
					this._joursSemaine = value;
						this.RaisePropertyChanged(()=>this.JoursSemaine);
				}
			}
		}
		#endregion

		#region CurrentGroupe
		private Groupe _currentGroupe;

		/// <summary>
		/// Obtient/Définit le groupe courant
		/// </summary>
		public Groupe CurrentGroupe {
			get { return this._currentGroupe; }
			set {
				if (this._currentGroupe != value) {
					this._currentGroupe = value;
					this.RaisePropertyChanged(() => this.CurrentGroupe);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<JourSemaine> repoJourSemaine;
		private Repository<Groupe> repoGroupe;
		private Repository<Saison> repoSaison;
		#endregion

		public FormulaireGroupeVM() {
			this.CreateRepositories();
			this.CurrentGroupe = new Groupe();
			this.UCParentCode = CodesUC.ConsultationGroupes;
			this.PopulateCombos();

			this.CurrentGroupe.Saison = this.repoSaison.GetAll().Where(s => s.EstSaisonCourante).FirstOrDefault();
		}

		private void CreateRepositories() {
			this.repoGroupe = new Repository<Groupe>(this._context);
			this.repoSaison = new Repository<Saison>(this._context);
			this.repoJourSemaine = new Repository<JourSemaine>(this._context);
		}

		private void PopulateCombos() {
			this.JoursSemaine = this.repoJourSemaine.GetAll().OrderBy(j => j.Numero);
		}

		protected override void PrepareValuesForTreatment() {
			// on change la date minimum par le minimum accepté par sql server (01/01/1753)
			this.CurrentGroupe.HeureDebut = this.CurrentGroupe.HeureDebut.AddYears(1752);
			this.CurrentGroupe.HeureFin = this.CurrentGroupe.HeureFin.AddYears(1752);

			this.CurrentGroupe.Libelle = (this.CurrentGroupe.Libelle == null) ? null : this.CurrentGroupe.Libelle.ToUpperInvariant();
		}

		protected override bool CurrentElementExists() {
			// on vérifie l'existence en se basant sur le jour et le libelle
			return this.repoGroupe.GetAll().Where(
				g => g.JourSemaine == this.CurrentGroupe.JourSemaine 
					&& g.Libelle.Equals(this.CurrentGroupe.Libelle, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if(string.IsNullOrWhiteSpace(this.CurrentGroupe.Libelle)) {
				errors.Add(ResGroupes.ErrLibelleObligatoire);
			}

			if (this.CurrentGroupe.JourSemaine == null) {
				errors.Add(ResGroupes.ErrJourObligatoire);
			}

			if (this.CurrentGroupe.HeureDebut == DateTime.MinValue) {
				errors.Add(ResGroupes.ErrHeureDebutObligatoire);
			}

			if (this.CurrentGroupe.HeureFin == DateTime.MinValue) {
				errors.Add(ResGroupes.ErrHeureFinObligatoire);
			}

			if(errors.Count == 0 && 
				(this.CurrentGroupe.HeureDebut.Hour > this.CurrentGroupe.HeureFin.Hour
				|| (this.CurrentGroupe.HeureDebut.Hour == this.CurrentGroupe.HeureFin.Hour && this.CurrentGroupe.HeureDebut.Minute >= this.CurrentGroupe.HeureFin.Minute))) {

				errors.Add(ResGroupes.ErrHeureFinInfHeureSup);
			}
			
			return errors.Count == 0;
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentGroupe.ID = Guid.NewGuid();
				this.repoGroupe.Add(this.CurrentGroupe);
				this.repoGroupe.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
