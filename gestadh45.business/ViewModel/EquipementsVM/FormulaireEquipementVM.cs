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
		#region Modeles
		private IOrderedEnumerable<Modele> _modeles;

		/// <summary>
		/// Obtient/Définit la liste des couleurs
		/// </summary>
		public IOrderedEnumerable<Modele> Modeles {
			get { return this._modeles; }
			set {
				if (this._modeles != value) {
					this._modeles = value;
					this.RaisePropertyChanged(() => this.Modeles);
				}
			}
		}
		#endregion				


		#region Localisations
		private IOrderedEnumerable<Localisation> _localisations;

		/// <summary>
		/// Obtient/Définit la liste des localisations
		/// </summary>
		public IOrderedEnumerable<Localisation> Localisations {
			get { return this._localisations; }
			set {
				if (this._localisations != value) {
					this._localisations = value;
					this.RaisePropertyChanged(() => this.Localisations);
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
		private Repository<Equipement> _repoEquipement;
		private Repository<Modele> _repoModele;
		private Repository<Localisation> _repoLocalisation;
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
			this._repoModele = new Repository<Modele>(this._context);
			this._repoLocalisation = new Repository<Localisation>(this._context);
		}

		private void PopulateCombos() {
			this.Modeles = this._repoModele.GetAll().OrderBy(c => c.ToString());
			this.Localisations = this._repoLocalisation.GetAll().OrderBy(l => l.Libelle);
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

			if (this.CurrentEquipement.Modele == null) {
				errors.Add(ResEquipements.ErrModeleObligatoire);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(string.Format(ResEquipements.ErrEquipementExiste, this.CurrentEquipement.Numero));
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
