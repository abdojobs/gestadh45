using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.AdherentsVM
{
	public class FormulaireAdherentVM : VMFormulaireBase
	{
		#region Villes
		private IOrderedEnumerable<Ville> _villes;

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public IOrderedEnumerable<Ville> Villes {
			get { return this._villes; }
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}
		#endregion

		#region Sexes
		private IOrderedEnumerable<Sexe> _sexes;

		/// <summary>
		/// Obtient/Définit la liste des sexes
		/// </summary>
		public IOrderedEnumerable<Sexe> Sexes {
			get { return this._sexes; }
			set {
				if (this._sexes != value) {
					this._sexes = value;
					this.RaisePropertyChanged(() => this.Sexes);
				}
			}
		}
		#endregion

		#region CurrentAdherent
		private Adherent _currentAdherent;

		/// <summary>
		/// Obtient/Définit l'adhérent courant du formulaire
		/// </summary>
		public Adherent CurrentAdherent {
			get { return this._currentAdherent; }
			set {
				if (this._currentAdherent != value) {
					this._currentAdherent = value;
					this.RaisePropertyChanged(() => this.CurrentAdherent);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Ville> repoVille;
		private Repository<Sexe> repoSexe;
		private Repository<Adherent> repoAdherent;
		#endregion

		#region Constructeurs
		/// <summary>
		/// COnstructeur pour le mode création
		/// </summary>
		public FormulaireAdherentVM() {
			this.UCParentCode = CodesUC.ConsultationAdherents;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentAdherent = new Adherent();
			this.CurrentAdherent.DateNaissance = DateTime.Now;
		}

		/// <summary>
		/// Constructeur pour le mode édition. On doit passer par l'id pour récupérer l'adhérent car on a changé de contexte.
		/// </summary>
		/// <param name="idAdherent">ID de l'adhérent à éditer</param>
		public FormulaireAdherentVM(int idAdherent) {
			this.UCParentCode = CodesUC.ConsultationAdherents;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentAdherent = this.repoAdherent.GetByKey(idAdherent);
		}
		#endregion

		private void CreateRepositories() {
			this.repoAdherent = new Repository<Adherent>(this._context);
			this.repoSexe = new Repository<Sexe>(this._context);
			this.repoVille = new Repository<Ville>(this._context);
		}

		private void PopulateCombos() {
			this.Villes = this.repoVille.GetAll().OrderBy(v => v.ToString());
			this.Sexes = this.repoSexe.GetAll().OrderBy(s => s.ToLongString());
		}

		/// <summary>
		/// Vérifie que cet adhérent n'existe pas déjà (nom + prénom)
		/// </summary>
		/// <returns>Booléen indiquant si l'adhérent existe déjà ou non</returns>
		protected override bool CurrentElementExists() {
			return this.repoAdherent.GetAll().Where(
					a => a.Nom.ToUpperInvariant().Equals(this.CurrentAdherent.Nom.ToUpperInvariant())
					&& a.Prenom.ToUpperInvariant().Equals(this.CurrentAdherent.Prenom.ToUpperInvariant())
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentAdherent.Nom = (this.CurrentAdherent.Nom == null) ? null : this.CurrentAdherent.Nom.ToUpperInvariant();
			this.CurrentAdherent.Prenom = (this.CurrentAdherent.Prenom == null) ? null : this.CurrentAdherent.Prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.CurrentAdherent.Prenom);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Nom)) {
				errors.Add("Le nom est obligatoire");
			}

			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Prenom)) {
				errors.Add("Le prénom est obligatoire");
			}

			if (this.CurrentAdherent.DateNaissance == DateTime.MinValue) {
				errors.Add("La date de naissance est obligatoire");
			}

			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Adresse)) {
				errors.Add("L'adresse est obligatoire");
			}

			if (this.CurrentAdherent.Ville == null) {
				errors.Add("La ville est obligatoire");
			}

			if (errors.Count == 0 && !this.IsEditMode && this.CurrentElementExists()) {
				errors.Add("Cet adhérent existe déjà");
			}

			return errors.Count == 0;
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet Adherent avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (IsEditMode) {
				this.repoAdherent.Reload(this.CurrentAdherent);
			}
			
			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				if (this.IsEditMode) {
					this.repoAdherent.Edit(this.CurrentAdherent);
				}
				else {
					this.repoAdherent.Add(this.CurrentAdherent);
				}

				this.repoAdherent.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}			
		}
		#endregion
	}
}
