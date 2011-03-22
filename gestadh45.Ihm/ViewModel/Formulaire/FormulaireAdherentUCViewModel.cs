using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireAdherentUCViewModel : ViewModelBaseFormulaire
	{
		private Adherent mAdherent;
		private ICollectionView mSexes;
		private ICollectionView mVilles;

		public FormulaireAdherentUCViewModel() {
			this.Adherent = new Adherent();
			this.Adherent.Adresse = new Adresse();
			this.Adherent.Contact = new Contact();
			this.InitialisationListeVilles();
			this.InitialisationListeSexes();
			base.CreateAnnulerCommand();
			this.CreateEnregistrerCommand();
			base.EstEdition = false;
		}

		public bool CanExecuteEnregistrerCommand() {
			return true;
		}

		private void CreateEnregistrerCommand() {
			this.EnregistrerCommand = new RelayCommand(
				this.ExecuteEnregistrerCommand, 
				this.CanExecuteEnregistrerCommand
			);
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if ((this.Adherent != null) && base.EstEdition) {
				AdherentDao.GetInstance(ViewModelLocator.Context).Refresh(this.Adherent);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() 
				&& base.EstEdition 
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {
				
				AdherentDao.GetInstance(ViewModelLocator.Context).Update(this.Adherent);
				Messenger.Default.Send<NotificationMessage<string>>(
					new NotificationMessage<string>("ConsultationAdherents", "ChangementUserControl")
				);
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition 
				&& !AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {

				AdherentDao.GetInstance(ViewModelLocator.Context).Create(this.Adherent);
				Messenger.Default.Send<NotificationMessage<string>>(
					new NotificationMessage<string>("ConsultationAdherents", "ChangementUserControl")
				);
			}
			else {
				Messenger.Default.Send<NotificationMessageErreur>(
					new NotificationMessageErreur(ResMessages.CodeErreur, this.ChaineErreurs)
				);
			}
		}

		private void InitialisationListeSexes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(SexeDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("LibelleCourt", ListSortDirection.Descending));
			this.Sexes = defaultView;
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(VilleDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public Adherent Adherent {
			get {
				return this.mAdherent;
			}
			set {
				if (this.mAdherent != value) {
					this.mAdherent = value;
					this.RaisePropertyChanged("Adherent");
				}
			}
		}

		public ICommand EnregistrerCommand { get; set; }

		public ICollectionView Sexes {
			get {
				return this.mSexes;
			}
			set {
				if (this.mSexes != value) {
					this.mSexes = value;
					this.RaisePropertyChanged("Sexes");
				}
			}
		}

		public ICollectionView Villes {
			get {
				return this.mVilles;
			}
			set {
				if (this.mVilles != value) {
					this.mVilles = value;
					this.RaisePropertyChanged("Villes");
				}
			}
		}

		private bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if(string.IsNullOrWhiteSpace(this.Adherent.Nom)) {
				this.mErreurs.Add(ResErreurs.Adherent_NomObligatoire);
			}

			if(string.IsNullOrWhiteSpace(this.Adherent.Prenom)) {
				this.mErreurs.Add(ResErreurs.Adherent_PrenomObligatoire);
			}

			if (this.Adherent.DateNaissance == null) {
				this.mErreurs.Add(ResErreurs.Adherent_DateNaissanceObligatoire);
			}

			if (!this.EstEdition 
				&& this.mErreurs.Count == 0 
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {

				this.mErreurs.Add(ResErreurs.Adherent_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
