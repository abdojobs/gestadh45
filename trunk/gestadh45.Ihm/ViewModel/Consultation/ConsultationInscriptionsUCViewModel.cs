using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInscriptionsUCViewModel : ViewModelBaseConsultation
	{
		private long mIdVilleClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read().Adresse.ID_Ville;
		private Inscription mInscription;
		private ICollectionView mInscriptionsSaisonCourante;

		public ConsultationInscriptionsUCViewModel() {
			this.InitialisationListeInscriptions();
			this.CreateAfficherDetailsInscriptionCommand();
			this.CreateSupprimerInscriptionCommand();
			this.CreateEditerCommand();
			base.CreateCreerCommand();
			this.CreateGenererDocumentCommand();
		}

		public bool CanExecuteEditerCommand(string pCodeUC) {
			return (this.Inscription != null);
		}

		public bool CanExecuteGenererDocumentCommand(string pCodeDocument) {
			return (this.Inscription != null);
		}

		public bool CanExecuteSupprimerInscriptionCommand() {
			return (
				this.Inscription != null 
				&& InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)
				);
		}

		private void CreateAfficherDetailsInscriptionCommand() {
			this.AfficherDetailsInscriptionCommand = new RelayCommand<Inscription>(
				new Action<Inscription>(this, this.ExecuteAfficherDetailsInscriptionCommand));
		}

		protected void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand<string>(
				new Action<string>(this, this.ExecuteEditerCommand), 
				new Predicate<string>(this, this.CanExecuteEditerCommand));
		}

		private void CreateGenererDocumentCommand() {
			this.GenererDocumentCommand = new RelayCommand<string>(
				new Action<string>(this, this.ExecuteGenererDocumentCommand), 
				new Predicate<string>(this, this.CanExecuteGenererDocumentCommand));
		}

		private void CreateSupprimerInscriptionCommand() {
			this.SupprimerInscriptionCommand = new RelayCommand(
				new Action(this, this.ExecuteSupprimerInscriptionCommand), 
				new Func<bool>(this, this.CanExecuteSupprimerInscriptionCommand));
		}

		public void ExecuteAfficherDetailsInscriptionCommand(Inscription pInscription) {
			if (pInscription != null) {
				this.Inscription = pInscription;
			}
		}

		public void ExecuteEditerCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageFormulaire<string, Inscription>>(
				new NotificationMessageFormulaire<string, Inscription>(pCodeUC, "ChangementUserControl", this.Inscription));
		}

		public void ExecuteGenererDocumentCommand(string pCodeDocument)
		{
			// TODO implémenter l'appel d'un file dialog avec le nom du fichier et rediriger le resultat vers this.GenererDocument(string pSaveFilePath, string pCodeDocument)
		}

		public void ExecuteSupprimerInscriptionCommand() {
			if (this.Inscription != null) {
				DialogMessageConfirmation message = 
					new DialogMessageConfirmation(
						ResMessages.MessageConfirmSupprInscription, 
						new Action<MessageBoxResult>(this, this.ExecuteSupprimerInscriptionCommandCallBack));
				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerInscriptionCommand();
		}

		private void ExecuteSupprimerInscriptionCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				InscriptionDao.GetInstance(ViewModelLocator.Context).Delete(this.Inscription);
				this.InitialisationListeInscriptions();
				this.Inscription = null;
			}
		}

		private void GenererDocument(string pSaveFilePath, string pCodeDocument) {
			// implémenter
			throw new NotImplementedException("La génération de document n'est pas encore implémentée");
		}

		private void InitialisationListeInscriptions() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(InscriptionDao.GetInstance(ViewModelLocator.Context).ListSaisonCourante());
			defaultView.GroupDescriptions.Add(new PropertyGroupDescription("Groupe"));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.JourSemaine", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.HeureDebut", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Prenom", ListSortDirection.Ascending));
			this.InscriptionsSaisonCourante = defaultView;
		}

		public ICommand AfficherDetailsInscriptionCommand { get; set; }

		public ICommand EditerCommand { get; set; }

		public ICommand GenererDocumentCommand { get; set; }

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

		public ICollectionView InscriptionsSaisonCourante {
			get {
				return this.mInscriptionsSaisonCourante;
			}
			set {
				if (this.mInscriptionsSaisonCourante != value) {
					this.mInscriptionsSaisonCourante = value;
					this.RaisePropertyChanged("InscriptionsSaisonCourante");
				}
			}
		}

		public ICommand SupprimerInscriptionCommand { get; set; }
	}
}
