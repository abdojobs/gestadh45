using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInscriptionUCViewModel : ViewModelBaseFormulaire
	{
		private ICollectionView mAdherents;
		private ICollectionView mGroupesSaisonCourante;
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
					this.RaisePropertyChanged("Adherents");
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
					this.RaisePropertyChanged("GroupesSaisonCourante");
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
					this.RaisePropertyChanged("Inscription");
					this.RaisePropertyChanged("CertificatRemis");
				}
			}
		}

		public FormulaireInscriptionUCViewModel() {
            this.Inscription = new Inscription();
			this.Inscription.Commentaire = string.Empty;

			this.InitialisationListeAdherents();
			this.InitialisationListeGroupes();

			this.CodeUCOrigine = CodesUC.ConsultationInscriptions;
			base.EstEdition = false;
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.Inscription != null && base.EstEdition) {
				InscriptionDao.GetInstance(ViewModelLocator.Context).Refresh(this.Inscription);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.Inscription.Commentaire == null) {
				this.Inscription.Commentaire = string.Empty;
			}

			if (this.VerifierSaisie() 
				&& base.EstEdition 
				&& InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)) {

				InscriptionDao.GetInstance(ViewModelLocator.Context).Update(this.Inscription);

				base.ExecuteEnregistrerCommand();
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition 
				&& !InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)) {

				InscriptionDao.GetInstance(ViewModelLocator.Context).Create(this.Inscription);

				base.ExecuteEnregistrerCommand();
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
				);
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(AdherentDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(GroupeDao.GetInstance(ViewModelLocator.Context).ListSaisonCourante());
			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		protected override bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (this.Inscription.Adherent == null) {
				this.mErreurs.Add(ResErreurs.Inscription_AdherentObligatoire);
			}

			if (this.Inscription.Groupe == null) {
				this.mErreurs.Add(ResErreurs.Inscription_GroupeObligatoire);
			}

			if (!this.EstEdition
				&& this.mErreurs.Count == 0
				&& InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)) {

				this.mErreurs.Add(ResErreurs.Inscription_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
