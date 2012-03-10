using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using gestadh45.dal;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.business.ViewModel.InscriptionsVM
{
	public class FormulaireInscriptionVM : VMFormulaireBase
	{
		#region ListeAdherents
		private IOrderedEnumerable<Adherent> _listeAdherents;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public IOrderedEnumerable<Adherent> ListeAdherents {
			get { return this._listeAdherents; }
			set {
				if (this._listeAdherents != value) {
					this._listeAdherents = value;
					this.RaisePropertyChanged(() => this.ListeAdherents);
				}
			}
		}
		#endregion

		#region ListeSexes
		private IOrderedEnumerable<Sexe> _listeSexes;

		/// <summary>
		/// Obtient/Définit la liste des sexes
		/// </summary>
		public IOrderedEnumerable<Sexe> ListeSexes {
			get { return this._listeSexes; }
			set {
				if (this._listeSexes != value) {
					this._listeSexes = value;
					this.RaisePropertyChanged(() => this.ListeSexes);
				}
			}
		}
		#endregion

		#region ListeGroupes
		private IOrderedEnumerable<Groupe> _listeGroupes;

		/// <summary>
		/// Obtient/Définit la liste des groupes
		/// </summary>
		public IOrderedEnumerable<Groupe> ListeGroupes {
			get { return this._listeGroupes; }
			set {
				if (this._listeGroupes != value) {
					this._listeGroupes = value;
					this.RaisePropertyChanged(() => this.ListeGroupes);
				}
			}
		}
		#endregion

		#region ListeVilles
		private IOrderedEnumerable<Ville> _listeVilles;

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public IOrderedEnumerable<Ville> ListeVilles {
			get { return this._listeVilles; }
			set {
				if (this._listeVilles != value) {
					this._listeVilles = value;
					this.RaisePropertyChanged(() => this.ListeVilles);
				}
			}
		}
		#endregion

		#region CurrentAdherent
		private Adherent _currentAdherent;

		/// <summary>
		/// Obtient/Définit l'adhérent courant
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

		#region NouveauAdherent
		private bool _nouveauAdherent;

		/// <summary>
		/// Obtient/Définit un booléen indiquant si on doit créer un nouvel adhérent
		/// </summary>
		public bool NouveauAdherent {
			get { return this._nouveauAdherent; }
			set {
				if (this._nouveauAdherent != value) {
					this._nouveauAdherent = value;
					this.RaisePropertyChanged(() => this.NouveauAdherent);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<Adherent> _repoAdherent;
		private Repository<Sexe> _repoSexe;
		private Repository<Groupe> _repoGroupe;
		private Repository<Inscription> _repoInscription;
		private Repository<Ville> _repoVille;
		#endregion

		#region Constructeurs
		/// <summary>
		/// Créé une nouvelle instance de FormulaireInscriptionVM en mode création
		/// </summary>
		public FormulaireInscriptionVM() {
			this.UCParentCode = CodesUC.ConsultationInscription;
			this.IsEditMode = false;

			this.CreateRepositories();
			this.PopulateCombos();
			this.CreateCommands();

			this.NouveauAdherent = true;

			// TODO bouchon
			this.IsEditMode = true;
			this.NouveauAdherent = false;
			var tt = this._repoInscription.GetAll().FirstOrDefault();

			this.CurrentAdherent = tt.Adherent;
			this.CurrentInscription = tt;
		}

		/// <summary>
		/// Créé une nouvelle instance de FormulaireInscriptionVM en mode édition, chargé avec l'inscription passée en paramètre
		/// </summary>
		/// <param name="inscription">Inscription à éditer</param>
		public FormulaireInscriptionVM(Inscription inscription) {
			this.UCParentCode = CodesUC.ConsultationInscription;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.PopulateCombos();
			this.CreateCommands();

			this.CurrentAdherent = inscription.Adherent;
			this.CurrentInscription = inscription;
			this.NouveauAdherent = false;
		}
		#endregion

		#region CancelCommand
		public override void ExecuteCancelCommand() {
			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				// on enregistre l'adhérent
				if (this.NouveauAdherent) {
					this._repoAdherent.Add(this.CurrentAdherent);
				}
				else {
					this._repoAdherent.Edit(this.CurrentAdherent);
				}

				this._repoAdherent.Save();

				// on lie l'adhérent à l'inscription
				this.CurrentInscription.Adherent = this.CurrentAdherent;

				// on enregistre l'inscription
				if (this.IsEditMode) {
					this._repoInscription.Edit(this.CurrentInscription);
				}
				else {
					this._repoInscription.Add(this.CurrentInscription);
				}

				this._repoInscription.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		#region ChargerAdherentCommand
		public ICommand ChargerAdherentCommand { get; set; }

		private void CreateChargerAdherentCommand() {
			this.ChargerAdherentCommand = new RelayCommand<Adherent>(
				this.ExecuteChargerAdherentCommand,
				this.CanExecuteChargerAdherentCommand
			);
		}

		public bool CanExecuteChargerAdherentCommand(Adherent selectedItem) {
			return selectedItem != null;
		}

		public void ExecuteChargerAdherentCommand(Adherent selectedItem) {
			this.CurrentAdherent = selectedItem;
		}
		#endregion

		private void PopulateCombos() {
			this.ListeAdherents = this._repoAdherent.GetAll().OrderBy(a => a.ToString());
			this.ListeGroupes = this._repoGroupe.GetAll().OrderBy(g => g.Libelle);
			this.ListeSexes = this._repoSexe.GetAll().OrderBy(s => s.LibelleCourt);
			this.ListeVilles = this._repoVille.GetAll().OrderBy(v => v.ToString());
		}

		private void CreateRepositories() {
			this._repoAdherent = new Repository<Adherent>(this._context);
			this._repoGroupe = new Repository<Groupe>(this._context);
			this._repoInscription = new Repository<Inscription>(this._context);
			this._repoSexe = new Repository<Sexe>(this._context);
			this._repoVille = new Repository<Ville>(this._context);
		}

		private void CreateCommands() {
			this.CreateChargerAdherentCommand();
		}
	}
}
