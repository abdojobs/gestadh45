using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInscriptionUCViewModel : ViewModelBaseFormulaire
	{
		private ICollectionView _adherents;
		private ICollectionView _groupesSaisonCourante;
		private ICollectionView _statutsInscription;
		private Inscription _inscription;

		private InscriptionDao _daoInscription;
		private GroupeDao _daoGroupe;
		private IAdherentDao _daoAdherent;
		private StatutInscriptionDao _daoStatutInscription;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public ICollectionView Adherents {
			get {
				return this._adherents;
			}
			set {
				if (this._adherents != value) {
					this._adherents = value;
					this.RaisePropertyChanged(() => this.Adherents);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des groupes de la saison courante
		/// </summary>
		public ICollectionView GroupesSaisonCourante {
			get {
				return this._groupesSaisonCourante;
			}
			set {
				if (this._groupesSaisonCourante != value) {
					this._groupesSaisonCourante = value;
					this.RaisePropertyChanged(() => this.GroupesSaisonCourante);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des statuts d'inscription
		/// </summary>
		public ICollectionView StatutsInscription {
			get {
				return this._statutsInscription;
			}
			set {
				if (this._statutsInscription != value) {
					this._statutsInscription = value;
					this.RaisePropertyChanged(() => this.StatutsInscription);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Inscription Inscription {
			get {
				return this._inscription;
			}
			set {
				if (this._inscription != value) {
					this._inscription = value;
					this.RaisePropertyChanged(() => this.Inscription);
				}
			}
		}

		public FormulaireInscriptionUCViewModel() {
			this._daoInscription = new InscriptionDao(ViewModelLocator.DataSource);
			this._daoGroupe = new GroupeDao(ViewModelLocator.DataSource);
			this._daoAdherent = DaoFactory.GetAdherentDao(ViewModelLocator.DataSource);
			this._daoStatutInscription = new StatutInscriptionDao(ViewModelLocator.DataSource);

			this.CreateSelectionnerAdherentCommand();
			this.CreateSelectionnerGroupeCommand();
			this.CreateSelectionnerStatutInscriptionCommand();

			this.Inscription = new Inscription();

			this.InitialisationFormulaire();

			this.CodeUCOrigine = CodesUC.ConsultationInscriptions;
			base.EstEdition = false;

			// initialisation du statut par défaut de l'inscription (dans App.config)
			int defaultStatudId = 0;
			bool recupStatut = int.TryParse(ConfigurationManager.AppSettings["DefaultStatutInscription"], out defaultStatudId);
			this.Inscription.StatutInscription = this._daoStatutInscription.Read(defaultStatudId);
		}

		public void SetInscription(Inscription pInscription) {
			this.InitialisationFormulaire();
			this.Inscription = pInscription;
			this.RaisePropertyChanged(() => this.Inscription);
		}

		public void SetAdherent(Adherent pAdherent) {
			this.InitialisationFormulaire();
			this.Inscription.Adherent = pAdherent;
			this.RaisePropertyChanged(() => this.Inscription);
		}

		public override void ExecuteEnregistrerCommand() {
			var msg = new NotificationMessageSelectionElement<Inscription>(this.Inscription);

			bool saisieValide = this.VerifierSaisie();

			if (saisieValide
				&& base.EstEdition
				&& this._daoInscription.Exists(this.Inscription)) {
					this._daoInscription.Update(this.Inscription);
				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else if (saisieValide
				&& !base.EstEdition
				&& !this._daoInscription.Exists(this.Inscription)) {

					this._daoInscription.Create(this.Inscription);

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		#region SelectionnerAdherentCommand
		public ICommand SelectionnerAdherentCommand { get; set; }

		private void CreateSelectionnerAdherentCommand() {
			this.SelectionnerAdherentCommand = new RelayCommand<Adherent>(
				this.ExecuteSelectionnerAdherentCommand,
				this.CanExecuteSelectionnerAdherentCommand
			);
		}

		public bool CanExecuteSelectionnerAdherentCommand(Adherent pAdherent) {
			return true;
		}

		public void ExecuteSelectionnerAdherentCommand(Adherent pAdherent) {
			this.Inscription.Adherent = pAdherent;
			this.RaisePropertyChanged(() => this.Inscription);
		}
		#endregion

		#region SelectionnerGroupeCommand
		public ICommand SelectionnerGroupeCommand { get; set; }

		private void CreateSelectionnerGroupeCommand() {
			this.SelectionnerGroupeCommand = new RelayCommand<Groupe>(
				this.ExecuteSelectionnerGroupeCommand,
				this.CanExecuteSelectionnerGroupeCommand
			);
		}

		public bool CanExecuteSelectionnerGroupeCommand(Groupe pGroupe) {
			return true;
		}

		public void ExecuteSelectionnerGroupeCommand(Groupe pGroupe) {
			this.Inscription.Groupe = pGroupe;
			this.RaisePropertyChanged(() => this.Inscription);
		}
		#endregion

		#region SelectionnerStatutInscriptionCommand
		public ICommand SelectionnerStatutInscriptionCommand { get; set; }

		private void CreateSelectionnerStatutInscriptionCommand() {
			this.SelectionnerStatutInscriptionCommand = new RelayCommand<StatutInscription>(
				this.ExecuteSelectionnerStatutInscriptionCommand,
				this.CanExecuteSelectionnerStatutInscriptionCommand
			);
		}

		public bool CanExecuteSelectionnerStatutInscriptionCommand(StatutInscription pStatut) {
			return true;
		}

		public void ExecuteSelectionnerStatutInscriptionCommand(StatutInscription pStatut) {
			this.Inscription.StatutInscription = pStatut;
			this.RaisePropertyChanged(() => this.Inscription);
		}
		#endregion

		private void InitialisationFormulaire() {
			ICollectionView defaultViewAdherents = CollectionViewSource.GetDefaultView(this._daoAdherent.List());
			defaultViewAdherents.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultViewAdherents.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultViewAdherents;

			ICollectionView defaultViewGroupes = CollectionViewSource.GetDefaultView(this._daoGroupe.ListSaisonCourante());
			defaultViewGroupes.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultViewGroupes.SortDescriptions.Add(new SortDescription("HeureDebutDT", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultViewGroupes;

			ICollectionView defaultViewStatuts = CollectionViewSource.GetDefaultView(this._daoStatutInscription.List());
			defaultViewStatuts.SortDescriptions.Add(new SortDescription("Ordre", ListSortDirection.Ascending));
			this.StatutsInscription = defaultViewStatuts;
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (this.Inscription.Adherent == null) {
				lErreurs.Add(ResErreurs.Inscription_AdherentObligatoire);
			}

			if (this.Inscription.Groupe == null) {
				lErreurs.Add(ResErreurs.Inscription_GroupeObligatoire);
			}

			if (this.Inscription.StatutInscription == null) {
				lErreurs.Add(ResErreurs.Inscription_StatutObligatoire);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this._daoInscription.Exists(this.Inscription)) {

					lErreurs.Add(ResErreurs.Inscription_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
