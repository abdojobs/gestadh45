using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.EquipementsVM
{
	public class FormulaireEquipementVM : VMFormulaireBase
	{
		#region Marques
		private IOrderedEnumerable<Marque> _marques;

		/// <summary>
		/// Gets or sets the marques.
		/// </summary>
		/// <value>
		/// The marques.
		/// </value>
		public IOrderedEnumerable<Marque> Marques {
			get {
				return this._marques;
			}

			set {
				if (this._marques != value) {
					this._marques = value;
					this.RaisePropertyChanged(() => this.Marques);
				}
			}
		}
		#endregion

		#region Categories
		private IOrderedEnumerable<Categorie> _categories;

		/// <summary>
		/// Gets or sets the categories.
		/// </summary>
		/// <value>
		/// The categories.
		/// </value>
		public IOrderedEnumerable<Categorie> Categories {
			get {
				return this._categories;
			}

			set {
				if (this._categories != value) {
					this._categories = value;
					this.RaisePropertyChanged(() => this.Categories);
				}
			}
		}
		#endregion

		#region DureesDeVie
		private IOrderedEnumerable<DureeDeVie> _dureesDeVie;

		/// <summary>
		/// Obtient/Définit la liste des durées de vie
		/// </summary>
		public IOrderedEnumerable<DureeDeVie> DureesDeVie {
			get { return this._dureesDeVie; }
			set {
				if (this._dureesDeVie != value) {
					this._dureesDeVie = value;
					this.RaisePropertyChanged(() => this.DureesDeVie);
				}
			}
		}
		#endregion

		
		#region Couleurs
		private IOrderedEnumerable<Couleur> _couleurs;

		/// <summary>
		/// Obtient/Définit la liste des couleurs
		/// </summary>
		public IOrderedEnumerable<Couleur> Couleurs {
			get { return this._couleurs; }
			set {
				if (this._couleurs != value) {
					this._couleurs = value;
					this.RaisePropertyChanged(() => this.Couleurs);
				}
			}
		}
		#endregion
				

		#region CurrentEquipement
		private Equipement _currentEquipement;

		/// <summary>
		/// Gets or sets the current equipement.
		/// </summary>
		/// <value>
		/// The current equipement.
		/// </value>
		public Equipement CurrentEquipement {
			get {
				return this._currentEquipement;
			}

			set {
				if (this._currentEquipement != value) {
					this._currentEquipement = value;
					this.RaisePropertyChanged(() => this.CurrentEquipement);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Marque> _repoMarque;
		private Repository<Equipement> _repoEquipement;
		private Repository<Categorie> _repoCategorie;
		private Repository<DureeDeVie> _repoDureeDeVie;
		private Repository<Couleur> _repoCouleur;
		#endregion

		#region Constructeurs
		/// <summary>
		/// Initializes a new instance of the <see cref="FormulaireEquipementVM"/> class.
		/// </summary>
		public FormulaireEquipementVM() {
			this.UCParentCode = CodesUC.ConsultationEquipements;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentEquipement = new Equipement ();

			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulateCombos());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormulaireEquipementVM"/> class.
		/// </summary>
		/// <param name="idEquipement">The id equipement.</param>
		public FormulaireEquipementVM(Guid idEquipement) {
			this.UCParentCode = CodesUC.ConsultationEquipements;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.PopulateCombos();

			this.CurrentEquipement = this._repoEquipement.GetByKey(idEquipement);

			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulateCombos());
		}
		#endregion

		private void CreateRepositories() {
			this._repoEquipement = new Repository<Equipement>(this._context);
			this._repoMarque = new Repository<Marque>(this._context);
			this._repoCategorie = new Repository<Categorie>(this._context);
			this._repoDureeDeVie = new Repository<DureeDeVie>(this._context);
			this._repoCouleur = new Repository<Couleur>(this._context);
		}

		private void PopulateCombos() {
			this.Marques = this._repoMarque.GetAll().OrderBy(m => m.ToString());
			this.Categories = this._repoCategorie.GetAll().OrderBy(c => c.ToString());
			this.DureesDeVie = this._repoDureeDeVie.GetAll().OrderBy(d => d.Libelle);
			this.Couleurs = this._repoCouleur.GetAll().OrderBy(c => c.ToString());
		}

		/// <summary>
		/// Vérifie que cet équipement n'existe pas déjà (numéro)
		/// </summary>
		/// <returns>Booléen indiquant si l'équipement existe déjà ou non</returns>
		protected override bool CurrentElementExists() {
			return this._repoEquipement.GetAll().Where(
					e => e.Numero.Equals(this.CurrentEquipement.Numero, StringComparison.OrdinalIgnoreCase)
						&& e.ID != this.CurrentEquipement.ID
				).Count() != 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentEquipement.Numero = (this.CurrentEquipement.Numero == null) ? null : this.CurrentEquipement.Numero.ToUpperInvariant();
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentEquipement.Numero)) {
				errors.Add(ResEquipements.ErrNumeroObligatoire);
			}

			if (this.CurrentEquipement.Marque == null) {
				errors.Add(ResEquipements.ErrMarqueObligatoire);
			}

			if (this.CurrentEquipement.Categorie == null) {
				errors.Add(ResEquipements.ErrCategorieObligatoire);
			}

			if (this.CurrentEquipement.DureeDeVie == null) {
				errors.Add(ResEquipements.ErrDureeDeVieObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResEquipements.ErrEquipementExiste);
			}

			return errors.Count == 0;
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet Equipement avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (IsEditMode) {
				this._repoEquipement.Reload(this.CurrentEquipement);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentEquipement.DateModification = DateTime.Now;

				if (this.IsEditMode) {
					this._repoEquipement.Edit(this.CurrentEquipement);
				}
				else {
					this.CurrentEquipement.ID = Guid.NewGuid();
					this.CurrentEquipement.DateCreation = DateTime.Now;
					this._repoEquipement.Add(this.CurrentEquipement);
				}

				this._repoEquipement.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
