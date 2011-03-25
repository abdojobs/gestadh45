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
using gestadh45.service.Documents;

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
				this.ExecuteAfficherDetailsInscriptionCommand
			);
		}

		protected void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand<string>(
				this.ExecuteEditerCommand, 
				this.CanExecuteEditerCommand
			);
		}

		private void CreateGenererDocumentCommand() {
			this.GenererDocumentCommand = new RelayCommand<string>(
				this.ExecuteGenererDocumentCommand, 
				this.CanExecuteGenererDocumentCommand
			);
		}

		private void CreateSupprimerInscriptionCommand() {
			this.SupprimerInscriptionCommand = new RelayCommand(
				this.ExecuteSupprimerInscriptionCommand, 
				this.CanExecuteSupprimerInscriptionCommand
			);
		}

		public void ExecuteAfficherDetailsInscriptionCommand(Inscription pInscription) {
			if (pInscription != null) {
				this.Inscription = pInscription;
			}
		}

		public void ExecuteEditerCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageFormulaire<string, Inscription>>(
				new NotificationMessageFormulaire<string, Inscription>(pCodeUC, TypesNotification.ChangementUC, this.Inscription));
		}

		public void ExecuteGenererDocumentCommand(string pCodeDocument)
		{
			if (this.Inscription != null) {
				NotificationMessageActionFileDialog<string> message =
					new NotificationMessageActionFileDialog<string>(
						TypesNotification.SaveFileDialog,
						ResDocuments.ExtensionFichierPdf,
						this.CreerNomFichierDocument(pCodeDocument),
						callbackmessage =>
						{
							this.GenererDocument(callbackmessage, pCodeDocument);
						}
					);

				Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
			}
		}

		private void GenererDocument(string pSaveFilePath, string pCodeDocument) {
			InfosClub lInfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();
			DonneesDocument lDonnees = DonneesDocumentAdaptateur.CreerDonneesDocument(lInfosClub, this.Inscription);
			GenerateurDocumentPDF lGenerateur = new GenerateurDocumentPDF(lDonnees, pSaveFilePath);

			try {
				switch (pCodeDocument) {
					case GenerateurDocumentBase.CodeInscriptionPdf:
						lGenerateur.CreerDocumentInscription();
						break;

					case GenerateurDocumentBase.CodeAttestationPdf:
						lGenerateur.CreerDocumentAttestation();
						break;
				}

				Messenger.Default.Send(new NotificationMessage(ResMessages.MessageInfoGenerationDocument));
			}
			catch (Exception lEx) {
				NotificationMessageUtilisateur message =
					new NotificationMessageUtilisateur(
						TypesNotification.Erreur,
						lEx.Message
					);

				Messenger.Default.Send<NotificationMessageUtilisateur>(message);
			}
		}

		public void ExecuteSupprimerInscriptionCommand() {
			if (this.Inscription != null) {
				DialogMessageConfirmation message = 
					new DialogMessageConfirmation(
						ResMessages.MessageConfirmSupprInscription, 
						this.ExecuteSupprimerInscriptionCommandCallBack
					);

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

		private string CreerNomFichierDocument(string pCodeDocument) {
			string lRetour = string.Empty;

			switch (pCodeDocument) {
				case GenerateurDocumentBase.CodeInscriptionPdf:
					lRetour =  string.Format(
						"{0} - {1}", 
						ResDocuments.PrefixeNomFichierInscription, this.Inscription.Adherent.ToString());
					break;

				case GenerateurDocumentBase.CodeAttestationPdf:
					lRetour =  string.Format(
						"{0} - {1}", 
						ResDocuments.PrefixeNomFichierAttestation, this.Inscription.Adherent.ToString());
					break;
			}

			return lRetour;
		}
	}
}
