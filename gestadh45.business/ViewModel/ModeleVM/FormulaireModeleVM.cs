using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.business.ViewModel.ModeleVM
{
	public class FormulaireModeleVM : VMFormulaireBase
	{

		#region Categories
		private IOrderedEnumerable<Categorie> _categories;

		/// <summary>
		/// Obtient/Définit la liste des catégories
		/// </summary>
		public IOrderedEnumerable<Categorie> Categories {
			get { return this._categories; }
			set {
				if (this._categories != value) {
					this._categories = value;
					this.RaisePropertyChanged(() => this.Categories);
				}
			}
		}
		#endregion

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

		#region CurrentModele
		private Modele _currentModele;

		/// <summary>
		/// Obtient/Définit le modèle courant
		/// </summary>
		public Modele CurrentModele {
			get { return this._currentModele; }
			set {
				if (this._currentModele != value) {
					this._currentModele = value;
					this.RaisePropertyChanged(() => this.CurrentModele);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Modele> _repoModele;
		private Repository<Categorie> _repoCategories;
		private Repository<Marque> _repoMarques;
		#endregion

		#region Constructeur
		public FormulaireModeleVM() {
			this._repoModele = new Repository<Modele>(this._context);
			this._repoCategories = new Repository<Categorie>(this._context);
			this._repoMarques = new Repository<Marque>(this._context);

			this.CurrentModele = new Modele();
			this.UCParentCode = CodesUC.ConsultationModeles;
			this.PopulateCombo();

			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulateCombo());
		}
		#endregion

		private void PopulateCombo() {
			this.Categories = this._repoCategories.GetAll().OrderBy(c => c.Libelle);
			this.Marques = this._repoMarques.GetAll().OrderBy(m => m.Libelle);
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();

			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentModele.ID = Guid.NewGuid();
				this._repoModele.Add(this.CurrentModele);
				this._repoModele.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override void PrepareValuesForTreatment() {
			this.CurrentModele.Nom = (this.CurrentModele.Nom == null) ? null : this.CurrentModele.Nom.ToUpperInvariant();

			// on remplace les null par des chaines vides (histoire de faciliter la comparaison ultérieure)
			this.CurrentModele.Couleur1 = this.CurrentModele.Couleur1 ?? string.Empty;
			this.CurrentModele.Couleur2 = this.CurrentModele.Couleur2 ?? string.Empty;
			this.CurrentModele.Couleur3 = this.CurrentModele.Couleur3 ?? string.Empty;

			// on trim les valeurs
			this.CurrentModele.Couleur1 = this.CurrentModele.Couleur1.Trim();
			this.CurrentModele.Couleur2 = this.CurrentModele.Couleur2.Trim();
			this.CurrentModele.Couleur3 = this.CurrentModele.Couleur3.Trim();
		}

		protected override bool CurrentElementExists() {
			// critères d'unicité : catégorie + marque + nom du modèle + couleur1 + couleur2 + couleur3
			return this._repoModele.GetAll().Where(
					m => m.Nom.Equals(this.CurrentModele.Nom, StringComparison.OrdinalIgnoreCase)
						&& m.Marque.ID == this.CurrentModele.Marque.ID
						&& m.Categorie.ID == this.CurrentModele.Categorie.ID
						&& m.Couleur1.Equals(this.CurrentModele.Couleur1, StringComparison.OrdinalIgnoreCase)
						&& m.Couleur2.Equals(this.CurrentModele.Couleur2, StringComparison.OrdinalIgnoreCase)
						&& m.Couleur3.Equals(this.CurrentModele.Couleur3, StringComparison.OrdinalIgnoreCase)
				).Count() != 0;
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentModele.Nom)) {
				errors.Add(ResModeles.ErrNomObligatoire);
			}

			if (this.CurrentModele.Categorie == null) {
				errors.Add(ResModeles.ErrCategorieObligatoire);
			}

			if (this.CurrentModele.Marque == null) {
				errors.Add(ResModeles.ErrMarqueObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()){ 
				errors.Add(ResModeles.ErrModeleExiste);
			}

			return errors.Count == 0;
		}
	}
}
