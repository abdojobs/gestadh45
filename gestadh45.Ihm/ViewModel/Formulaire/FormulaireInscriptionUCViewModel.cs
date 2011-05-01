using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using gestadh45.dao;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInscriptionUCViewModel : ViewModelBaseFormulaire
	{
		private ICollectionView mAdherents;
		private ICollectionView mGroupesSaisonCourante;
		private Inscription mInscription;

		private IInscriptionDao mDaoInscription;
		private IGroupeDao mDaoGroupe;
		private IAdherentDao mDaoAdherent;

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
				}
			}
		}

		public FormulaireInscriptionUCViewModel() {
			this.mDaoInscription = this.mDaoFactory.GetInscriptionDao();
			this.mDaoGroupe = this.mDaoFactory.GetGroupeDao();
			this.mDaoAdherent = this.mDaoFactory.GetAdherentDao();

            this.Inscription = new Inscription();

			this.InitialisationListeAdherents();
			this.InitialisationListeGroupes();

			this.CodeUCOrigine = CodesUC.ConsultationInscriptions;
			base.EstEdition = false;
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.Inscription != null && base.EstEdition) {
				this.mDaoInscription.Refresh(this.Inscription);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.Inscription.Commentaire == null) {
				this.Inscription.Commentaire = string.Empty;
			}

			if (this.VerifierSaisie() 
				&& base.EstEdition
				&& this.mDaoInscription.Exists(this.Inscription)) {
					this.mDaoInscription.Update(this.Inscription);
				base.ExecuteEnregistrerCommand();
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition
				&& !this.mDaoInscription.Exists(this.Inscription)) {

					this.mDaoInscription.Create(this.Inscription);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.ErreursVisibles = true;
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoGroupe.ListSaisonCourante());
			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (this.Inscription.Adherent == null) {
				lErreurs.Add(ResErreurs.Inscription_AdherentObligatoire);
			}

			if (this.Inscription.Groupe == null) {
				lErreurs.Add(ResErreurs.Inscription_GroupeObligatoire);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this.mDaoInscription.Exists(this.Inscription)) {

					lErreurs.Add(ResErreurs.Inscription_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
