using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Inscriptions
{
	public class FormulaireInscriptionUCViewModel : ViewModelBaseFormulaire
	{
		private ICollectionView mAdherents;
		private ICollectionView mGroupesSaisonCourante;
		private ICollectionView mStatutsInscription;
		private Inscription mInscription;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public ICollectionView Adherents {
			get {
				return this.mAdherents;
			}
			set {
				if (this.mAdherents != value) {
					this.mAdherents = value;
					this.RaisePropertyChanged(() => this.Adherents);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des groupes de la saison courante
		/// </summary>
		public ICollectionView GroupesSaisonCourante {
			get {
				return this.mGroupesSaisonCourante;
			}
			set {
				if (this.mGroupesSaisonCourante != value) {
					this.mGroupesSaisonCourante = value;
					this.RaisePropertyChanged(() => this.GroupesSaisonCourante);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des statuts d'inscription
		/// </summary>
		public ICollectionView StatutsInscription {
			get {
				return this.mStatutsInscription;
			}
			set {
				if (this.mStatutsInscription != value) {
					this.mStatutsInscription = value;
					this.RaisePropertyChanged(() => this.StatutsInscription);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Inscription Inscription {
			get {
				return this.mInscription;
			}
			set {
				if (this.mInscription != value) {
					this.mInscription = value;
					this.RaisePropertyChanged(() => this.Inscription);
				}
			}
		}

		public FormulaireInscriptionUCViewModel() {
			this.Inscription = new Inscription();

			this.InitialisationListeAdherents();
			this.InitialisationListeGroupes();
			this.InitialisationListeStatutsInscription();

			this.CodeUCOrigine = CodesUC.ConsultationInscriptions;
			base.EstEdition = false;

			// initialisation du statut par défaut de l'inscription (dans App.config)
			int defaultStatudId = 0;
			bool recupStatut = int.TryParse(ConfigurationManager.AppSettings["DefaultStatutInscription"], out defaultStatudId);
			this.Inscription.StatutInscription = ViewModelLocator.DaoStatutInscription.Read(defaultStatudId);
		}

		public override void ExecuteAnnulerCommand() {
			if (this.Inscription != null && base.EstEdition) {
				ViewModelLocator.DaoSaison.Refresh(this.Inscription.Groupe.Saison);
				ViewModelLocator.DaoGroupe.Refresh(this.Inscription.Groupe);
				ViewModelLocator.DaoVille.Refresh(this.Inscription.Adherent.Ville);
				ViewModelLocator.DaoAdherent.Refresh(this.Inscription.Adherent);
				ViewModelLocator.DaoInscription.Refresh(this.Inscription);
			}
			base.ExecuteAnnulerCommand();
		}

		public override void ExecuteEnregistrerCommand() {
			var msg = new MsgSelectionElement<Inscription>(this.Inscription);
	
			if (this.VerifierSaisie() 
				&& base.EstEdition
				&& ViewModelLocator.DaoInscription.Exists(this.Inscription)) {
					ViewModelLocator.DaoInscription.Update(this.Inscription);
				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition
				&& !ViewModelLocator.DaoInscription.Exists(this.Inscription)) {

					ViewModelLocator.DaoInscription.Create(this.Inscription);

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoGroupe.ListSaisonCourante());
			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		private void InitialisationListeStatutsInscription() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoStatutInscription.List());
			defaultView.SortDescriptions.Add(new SortDescription("Ordre", ListSortDirection.Ascending));
			this.StatutsInscription = defaultView;
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
				&& ViewModelLocator.DaoInscription.Exists(this.Inscription)) {

					lErreurs.Add(ResErreurs.Inscription_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
