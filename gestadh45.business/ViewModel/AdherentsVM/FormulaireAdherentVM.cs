using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
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
		/// Constructeur pour le mode création
		/// </summary>
		public FormulaireAdherentVM() {
			this.UCParentCode = CodesUC.ConsultationAdherents;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentAdherent = new Adherent();
			this.CurrentAdherent.DateNaissance = DateTime.Now;

			this.CreateSaveAndJoinCommand();
			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulateCombos());
		}

		/// <summary>
		/// Constructeur pour le mode édition. On doit passer par l'id pour récupérer l'adhérent car on a changé de contexte.
		/// </summary>
		/// <param name="idAdherent">ID de l'adhérent à éditer</param>
		public FormulaireAdherentVM(Guid idAdherent) {
			this.UCParentCode = CodesUC.ConsultationAdherents;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentAdherent = this.repoAdherent.GetByKey(idAdherent);

			this.CreateSaveAndJoinCommand();
			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulateCombos());
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
					a => a.Nom.Equals(this.CurrentAdherent.Nom, StringComparison.OrdinalIgnoreCase)
					&& a.Prenom.Equals(this.CurrentAdherent.Prenom, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentAdherent.Nom = (this.CurrentAdherent.Nom == null) ? null : this.CurrentAdherent.Nom.ToUpperInvariant();
			this.CurrentAdherent.Prenom = (this.CurrentAdherent.Prenom == null) ? null : this.CurrentAdherent.Prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.CurrentAdherent.Prenom);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Nom)) {
				errors.Add(ResAdherents.ErrNomObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Prenom)) {
				errors.Add(ResAdherents.ErrPrenomObligatoire);
			}

			if (this.CurrentAdherent.DateNaissance == DateTime.MinValue) {
				errors.Add(ResAdherents.ErrDateNaissanceObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.CurrentAdherent.Adresse)) {
				errors.Add(ResAdherents.ErrAdresseObligatoire);
			}

			if (this.CurrentAdherent.Ville == null) {
				errors.Add(ResAdherents.ErrVilleObligatoire);
			}

			if (errors.Count == 0 && !this.IsEditMode && this.CurrentElementExists()) {
				errors.Add(ResAdherents.ErrAdherentExiste);
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
				this.CurrentAdherent.DateModification = DateTime.Now;
				
				if (this.IsEditMode) {
					this.repoAdherent.Edit(this.CurrentAdherent);
				}
				else {
					this.CurrentAdherent.ID = Guid.NewGuid();
					this.CurrentAdherent.DateCreation = DateTime.Now;
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

		#region SaveAndJoinCommand
		public ICommand SaveAndJoinCommand { get; set; }

		private void CreateSaveAndJoinCommand() {
			this.SaveAndJoinCommand = new RelayCommand(
				this.ExecuteSaveAndJoinCommand, 
				this.CanExecuteSaveAndJoinCommand
			);
		}

		public bool CanExecuteSaveAndJoinCommand() {
			return true;
		}

		public void ExecuteSaveAndJoinCommand() {
			this.PrepareValuesForTreatment();
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentAdherent.DateModification = DateTime.Now;

				if (this.IsEditMode) {
					this.repoAdherent.Edit(this.CurrentAdherent);
				}
				else {
					this.CurrentAdherent.ID = Guid.NewGuid();
					this.CurrentAdherent.DateCreation = DateTime.Now;
					this.repoAdherent.Add(this.CurrentAdherent);
				}

				this.repoAdherent.Save();

				this.ClearUserNotifications();

				if (this.IsWindowMode) {
					Messenger.Default.Send(new NMRefreshDatas());
					Messenger.Default.Send(new NMCloseWindow());
				}

				Messenger.Default.Send<NMShowUC<Adherent>>(
					new NMShowUC<Adherent>(CodesUC.FormulaireInscription, this.CurrentAdherent)
				);
			}
			else {
				this.ShowUserNotifications(errors);
			}			
		}
		#endregion
	}
}
