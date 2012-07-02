using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireCampagneVerificationVM : VMFormulaireBase
	{
		#region CurrentCampagneVerification
		private CampagneVerification _currentCampagneVerification;

		/// <summary>
		/// Gets or sets the current campagne verification.
		/// </summary>
		/// <value>
		/// The current campagne verification.
		/// </value>
		public CampagneVerification CurrentCampagneVerification {
			get { return this._currentCampagneVerification; }
			set {
				if (this._currentCampagneVerification != value) {
					this._currentCampagneVerification = value;
					this.RaisePropertyChanged(() => this.CurrentCampagneVerification);
				}
			}
		}
		#endregion

		#region EquipementsDisponibles
		private ObservableCollection<Equipement> _equipementsDisponibles;

		/// <summary>
		/// Gets or sets the equipements disponibles.
		/// </summary>
		/// <value>
		/// The equipements disponibles.
		/// </value>
		public ObservableCollection<Equipement> EquipementsDisponibles {
			get { return this._equipementsDisponibles; }
			set {
				if (this._equipementsDisponibles != value) {
					this._equipementsDisponibles = value;
					this.RaisePropertyChanged(() => this.EquipementsDisponibles);
				}
			}
		}
		#endregion

		#region EquipementsInclus
		private ObservableCollection<Equipement> _equipementsInclus;

		/// <summary>
		/// Gets or sets the equipements inclus.
		/// </summary>
		/// <value>
		/// The equipements inclus.
		/// </value>
		public ObservableCollection<Equipement> EquipementsInclus {
			get { return this._equipementsInclus; }
			set {
				if (this._equipementsInclus != value) {
					this._equipementsInclus = value;
					this.RaisePropertyChanged(() => this.EquipementsInclus);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<CampagneVerification> _repoCampagneVerification;
		private Repository<Equipement> _repoEquipement;
		private Repository<StatutVerification> _repoStatutsVerifications;
		private Repository<Verification> _repoVerifications;
		#endregion

		private IList<Equipement> _allEquipements;

		#region constructeurs
		public FormulaireCampagneVerificationVM() {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = false;

			this.CreateRepositories();

			this._allEquipements = this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).ToList();

			this.FillEquipementsDisponiblesList();
			this.CreateCommands();

			this.CurrentCampagneVerification = new CampagneVerification()
			{
				Date = DateTime.Now
			};
		}

		/// <summary>
		/// Constructeur pour le mode édition
		/// </summary>
		/// <param name="idDureeDeVie">ID de l'objet à éditer</param>
		public FormulaireCampagneVerificationVM(Guid idCampagneVerification) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.CreateCommands();

			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagneVerification);

			this._allEquipements = new List<Equipement>();
			foreach (var verif in this.CurrentCampagneVerification.Verifications) {
				this._allEquipements.Add(verif.Equipement);
			}

			this.FillEquipementInclusList();
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
			this._repoEquipement = new Repository<Equipement>(this._context);
			this._repoStatutsVerifications = new Repository<StatutVerification>(this._context);
			this._repoVerifications = new Repository<Verification>(this._context);
		}

		private void FillEquipementsDisponiblesList() {
			this.EquipementsDisponibles = new ObservableCollection<Equipement>(this._allEquipements);
			this.EquipementsInclus = new ObservableCollection<Equipement>();
		}

		private void FillEquipementInclusList() {
			this.EquipementsDisponibles = new ObservableCollection<Equipement>();
			this.EquipementsInclus = new ObservableCollection<Equipement>(this._allEquipements);
		}

		protected override bool CheckFormValidity(List<string> errors) {			
			if (string.IsNullOrWhiteSpace(this.CurrentCampagneVerification.Libelle)) {
				errors.Add(ResCampagnesVerification.ErrLibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.CurrentCampagneVerification.Responsable)) {
				errors.Add(ResCampagnesVerification.ErrResponsableObligatoire);
			}

			if (!this.IsEditMode && this.EquipementsInclus.Count == 0) {
				errors.Add(ResCampagnesVerification.ErrAucunEquipement);
			}

			return errors.Count == 0;
		}

		private void CreateCommands() {
			this.CreateAddEquipementCommand();
			this.CreateAddAllEquipementsCommand();
			this.CreateRemoveEquipementCommand();
			this.CreateRemoveAllEquipementsCommand();
		}

		#region AddEquipementCommand
		public ICommand AddEquipementCommand { get; set; }

		private void CreateAddEquipementCommand() {
			this.AddEquipementCommand = new RelayCommand<Equipement>(
				this.ExecuteAddEquipementCommand,
				this.CanExecuteAddEquipementCommand
			);
		}

		public bool CanExecuteAddEquipementCommand(Equipement equip) {
			return !this.IsEditMode && equip != null;
		}

		public void ExecuteAddEquipementCommand(Equipement equip) {
			if (!this.IsEditMode && equip != null) {
				this.EquipementsDisponibles.Remove(equip);
				this.EquipementsInclus.Add(equip);
			}
		}
		#endregion

		#region AddAllEquipementsCommand
		public ICommand AddAllEquipementsCommand { get; set; }

		private void CreateAddAllEquipementsCommand() {
			this.AddAllEquipementsCommand = new RelayCommand(
				this.ExecuteAddAllEquipementsCommand,
				this.CanExecuteAddAllEquipementsCommand
			);
		}

		public bool CanExecuteAddAllEquipementsCommand() {
			return !this.IsEditMode;
		}

		public void ExecuteAddAllEquipementsCommand() {
			if (!this.IsEditMode) {
				this.FillEquipementInclusList();
			}
		}
		#endregion

		#region RemoveEquipementCommand
		public ICommand RemoveEquipementCommand { get; set; }

		private void CreateRemoveEquipementCommand() {
			this.RemoveEquipementCommand = new RelayCommand<Equipement>(
				this.ExecuteRemoveEquipementCommand,
				this.CanExecuteRemoveEquipementCommand
			);
		}

		public bool CanExecuteRemoveEquipementCommand(Equipement equip) {
			return !this.IsEditMode && equip != null;
		}

		public void ExecuteRemoveEquipementCommand(Equipement equip) {
			if (!this.IsEditMode && equip != null) {
				this.EquipementsInclus.Remove(equip);
				this.EquipementsDisponibles.Add(equip);
			}
		}
		#endregion

		#region RemoveAllEquipementsCommand
		public ICommand RemoveAllEquipementsCommand { get; set; }

		private void CreateRemoveAllEquipementsCommand() {
			this.RemoveAllEquipementsCommand = new RelayCommand(
				this.ExecuteRemoveAllEquipementsCommand,
				this.CanExecuteRemoveAllEquipementsCommand
			);
		}

		public bool CanExecuteRemoveAllEquipementsCommand() {
			return !this.IsEditMode;
		}

		public void ExecuteRemoveAllEquipementsCommand() {
			if (!this.IsEditMode) {
				this.FillEquipementsDisponiblesList();
			}
		}
		#endregion

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet courant avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (this.IsEditMode) {
				this._repoCampagneVerification.Reload(this.CurrentCampagneVerification);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				if (this.IsEditMode) {
					// enregistrement des modifs de la campagne
					this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
					this._repoCampagneVerification.Save();
				}
				else {
					// enregistrement de la campagne
					this.CurrentCampagneVerification.ID = Guid.NewGuid();
					this._repoCampagneVerification.Add(this.CurrentCampagneVerification);
					this._repoCampagneVerification.Save();

					// création des vérifications
					var statutDefaut = this._repoStatutsVerifications.GetAll().Where(s => s.EstDefaut).FirstOrDefault();

					foreach (Equipement equip in this.EquipementsInclus) {
						var verif = new Verification()
						{
							ID = Guid.NewGuid(),
							CampagneVerification = this.CurrentCampagneVerification,
							Equipement = equip,
							StatutVerification = statutDefaut
						};

						this._repoVerifications.Add(verif);
					}

					this._repoVerifications.Save();
				}	

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
