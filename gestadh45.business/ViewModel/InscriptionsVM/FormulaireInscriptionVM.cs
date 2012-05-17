using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.InscriptionsVM
{
	public class FormulaireInscriptionVM : VMFormulaireBase
	{
		#region Adherents
		private IOrderedEnumerable<Adherent> _adherents;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public IOrderedEnumerable<Adherent> Adherents {
			get { return this._adherents; }
			set {
				if (this._adherents != value) {
					this._adherents = value;
					this.RaisePropertyChanged(() => this.Adherents);
				}
			}
		}
		#endregion

		#region Groupes
		private IOrderedEnumerable<Groupe> _groupes;

		/// <summary>
		/// Obtient/Définit la liste des groupes
		/// </summary>
		public IOrderedEnumerable<Groupe> Groupes {
			get { return this._groupes; }
			set {
				if (this._groupes != value) {
					this._groupes = value;
					this.RaisePropertyChanged(() => this.Groupes);
				}
			}
		}
		#endregion

		#region Statuts
		private IOrderedEnumerable<StatutInscription> _statuts;

		/// <summary>
		/// Obtient/Définit la liste des statuts
		/// </summary>
		public IOrderedEnumerable<StatutInscription> Statuts {
			get { return this._statuts; }
			set {
				if (this._statuts != value) {
					this._statuts = value;
					this.RaisePropertyChanged(() => this.Statuts);
				}
			}
		}
		#endregion

		#region CurrentInscription
		private Inscription _currentInscription;

		/// <summary>
		/// Obtient/Définit l'inscription courante
		/// </summary>
		public Inscription CurrentInscription {
			get { return this._currentInscription; }
			set {
				if (this._currentInscription != value) {
					this._currentInscription = value;
					this.RaisePropertyChanged(() => this.CurrentInscription);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Adherent> repoAdherent;
		private Repository<Groupe> repoGroupe;
		private Repository<Inscription> repoInscription;
		private Repository<StatutInscription> repoStatuts;
		#endregion

		#region Constructeurs
		/// <summary>
		/// Constructeur pour le mode création
		/// </summary>
		public FormulaireInscriptionVM() {
			this.UCParentCode = CodesUC.ConsultationInscriptions;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentInscription = new Inscription() { Cotisation = 0 };
		}

		/// <summary>
		/// Formulaire pour le mode édition
		/// </summary>
		/// <param name="idInscription">ID de l'inscription à éditer</param>
		public FormulaireInscriptionVM(Guid idInscription) {
			this.UCParentCode = CodesUC.ConsultationInscriptions;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentInscription = this.repoInscription.GetByKey(idInscription);
		}
		#endregion

		private void PopulateCombos() {
			// en mode création on n'alimente la liste qu'avec les adhérents qui n'ont pas encore d'inscription sur la saison
			if (!this.IsEditMode) {
				this.Adherents = this.repoAdherent.GetAll()
					.Where(a => a.Inscriptions
						.Where(i => i.Groupe.Saison.EstSaisonCourante).Count() == 0
					)
					.OrderBy(a => a.ToString()
				);
			}
			else {
				this.Adherents = this.repoAdherent.GetAll().OrderBy(a => a.ToString());
			}

			this.Groupes = this.repoGroupe.GetAll()
				.Where(g => g.Saison.EstSaisonCourante)
				.OrderBy(g => g.JourSemaine.Numero);
			this.Statuts = this.repoStatuts.GetAll().OrderBy(s => s.Ordre);
		}

		private void CreateRepositories() {
			this.repoGroupe = new Repository<Groupe>(this._context);
			this.repoAdherent = new Repository<Adherent>(this._context);
			this.repoInscription = new Repository<Inscription>(this._context);
			this.repoStatuts = new Repository<StatutInscription>(this._context);
		}

		/// <summary>
		/// Vérifie que cette inscription n'existe pas déjà (adhérent + groupe)
		/// </summary>
		/// <returns>Booléen indiquant si l'inscription existe déjà ou non</returns>
		protected override bool CurrentElementExists() {
			return this.repoInscription.GetAll().Where(
					i => i.Adherent == this.CurrentInscription.Adherent 
					&& i.Groupe == this.CurrentInscription.Groupe
				).Count() != 0;
		}

		protected override bool CheckFormValidity(System.Collections.Generic.List<string> errors) {
			if (this.CurrentInscription.Adherent == null) {
				errors.Add(ResInscriptions.ErrAdherentObligatoire);
			}

			if (this.CurrentInscription.Groupe == null) {
				errors.Add(ResInscriptions.ErrGroupeObligatoire);
			}

			if (this.CurrentInscription.StatutInscription == null) {
				errors.Add(ResInscriptions.ErrStatutObligatoire);
			}

			if (this.CurrentInscription.Cotisation == null) {
				errors.Add(ResInscriptions.ErrCotisationInvalide);
			}

			if (errors.Count == 0 && !this.IsEditMode && this.CurrentElementExists()) {
				errors.Add(ResInscriptions.ErrInscriptionExiste);
			}

			return errors.Count == 0;
		}

		/// <summary>
		/// Ajoute automatiquement un adhérent à l'inscription à partir de son ID
		/// </summary>
		/// <param name="idAdherent">ID de l'adhérent</param>
		public void SetAdherent(Guid idAdherent) {
			this.CurrentInscription.Adherent = this.repoAdherent.GetByKey(idAdherent);
			this.RaisePropertyChanged(() => this.CurrentInscription);
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet Inscription avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (IsEditMode) {
				this.repoInscription.Reload(this.CurrentInscription);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentInscription.DateModification = DateTime.Now;

				if (this.IsEditMode) {
					this.repoInscription.Edit(this.CurrentInscription);
				}
				else {
					this.CurrentInscription.ID = Guid.NewGuid();
					this.CurrentInscription.DateCreation = DateTime.Now;
					this.repoInscription.Add(this.CurrentInscription);
				}

				this.repoInscription.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
