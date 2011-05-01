using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;
using gestadh45.service.Documents;
using gestadh45.service.VCards;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationGroupesUCViewModel : ViewModelBaseConsultation
	{		
		private Groupe mGroupe;
		private ICollectionView mGroupesSaisonCourante;

		private IInfosClubDao mDaoInfosCLub;
		private IGroupeDao mDaoGroupe;

		/// <summary>
		/// Obtient/Définit le groupe à afficher
		/// </summary>
		public Groupe Groupe {
			get {
				return this.mGroupe;
			}
			set {
				if (this.mGroupe != value) {
					this.mGroupe = value;
					this.RaisePropertyChanged("Groupe");
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

		public ConsultationGroupesUCViewModel() {
			this.mDaoInfosCLub = this.mDaoFactory.GetInfosClubDao();
			this.mDaoGroupe = this.mDaoFactory.GetGroupeDao();

			this.InitialisationListeGroupes();

			this.CreateGenererDocumentsGroupeCommand();
			this.CreateGenererVCardsGroupeCommand();
			this.CreateExtraireMailsCommand();
		}

		#region CreerCommand
		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireGroupe)
			);
		}
		#endregion

		#region SupprimerCommand
		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Groupe != null
				&& this.mDaoGroupe.Exists(this.Groupe)
				&& !this.mDaoGroupe.IsUsed(this.Groupe)
			);
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Groupe != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprGroupe,
					this.ExecuteSupprimerGroupeCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerCommand();
		}

		private void ExecuteSupprimerGroupeCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this.mDaoGroupe.Delete(this.Groupe);
				this.InitialisationListeGroupes();
				this.Groupe = null;
			}
		}
		#endregion

		#region AfficherDetailsCommand
		public override void ExecuteAfficherDetailsCommand(object pGroupe) {
			if (pGroupe != null && pGroupe is Groupe) {
				this.Groupe = pGroupe as Groupe;
			}
		}
		#endregion

		#region GenererDocumentsGroupeCommand
		public ICommand GenererDocumentsGroupeCommand { get; set; }

		private void CreateGenererDocumentsGroupeCommand() {
			this.GenererDocumentsGroupeCommand = new RelayCommand<string>(
				this.ExecuteGenererDocumentsGroupeCommand,
				this.CanExecuteGenererDocumentsGroupeCommand
			);
		}

		public bool CanExecuteGenererDocumentsGroupeCommand(string pCodeDocument) {
			return (this.Groupe != null);
		}

		public void ExecuteGenererDocumentsGroupeCommand(string pCodeDocument) {
			if (this.Groupe != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							this.GenererDocumentsGroupe(callbackmessage, pCodeDocument);
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region GenererVCardsGroupeCommand
		public ICommand GenererVCardsGroupeCommand { get; set; }

		private void CreateGenererVCardsGroupeCommand() {
			this.GenererVCardsGroupeCommand = new RelayCommand(
				this.ExecuteGenererVCardsGroupeCommand,
				this.CanExecuteGenererVCardsGroupeCommand
			);
		}

		public bool CanExecuteGenererVCardsGroupeCommand() {
			return (this.Groupe != null);
		}

		public void ExecuteGenererVCardsGroupeCommand() {
			if (this.Groupe != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							this.GenererVCardsGroupe(callbackmessage);
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region ExtraireMailsCommand
		public ICommand ExtraireMailsCommand { get; set; }

		private void CreateExtraireMailsCommand() {
			this.ExtraireMailsCommand = new RelayCommand(
				this.ExecuteExtraireMailsCommand,
				this.CanExecuteExtraireMailsCommand
			);
		}

		public bool CanExecuteExtraireMailsCommand() {
			return (this.Groupe != null);
		}

		public void ExecuteExtraireMailsCommand() {
			StringBuilder lSb = new StringBuilder();

			foreach (Inscription lInscription in this.Groupe.Inscriptions) {
				lSb.Append(lInscription.Adherent.Contact.ChaineMails);
			}

			Messenger.Default.Send<NotificationMessageConsultationExtractions>(
				new NotificationMessageConsultationExtractions(lSb.ToString())
			);
		}
		#endregion

		#region methodes privees
		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				this.mDaoGroupe.ListSaisonCourante()
			);

			foreach (Groupe lGroupe in defaultView) {
				this.mDaoGroupe.Refresh(lGroupe);
			}

			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		private void GenererDocumentsGroupe(string pSaveFolder, string pCodeDocument) {
			if (pSaveFolder != null) {
				InfosClub lInfosClub = this.mDaoInfosCLub.Read();

				foreach (Inscription lInscription in this.Groupe.Inscriptions) {
					DonneesDocument lDonnees = DonneesDocumentAdaptateur.CreerDonneesDocument(lInfosClub, lInscription);

					string lSaveFilePath;
					GenerateurDocumentBase lGenerateur;

					switch (pCodeDocument) {
						case GenerateurDocumentBase.CodeInscriptionPdf:
							lSaveFilePath = string.Format(
								"{0}\\{1} - {2}{3}",
								pSaveFolder,
								ResDocuments.PrefixeNomFichierInscription,
								lInscription.Adherent.ToString(),
								ResDocuments.ExtensionFichierPdf
							);

							lGenerateur = new GenerateurDocumentPDF(lDonnees, lSaveFilePath);
							lGenerateur.CreerDocumentInscription();
							break;

						case GenerateurDocumentBase.CodeAttestationPdf:
							lSaveFilePath = string.Format(
								"{0}\\{1} - {2}{3}",
								pSaveFolder,
								ResDocuments.PrefixeNomFichierAttestation,
								lInscription.Adherent.ToString(),
								ResDocuments.ExtensionFichierPdf
							);

							lGenerateur = new GenerateurDocumentPDF(lDonnees, lSaveFilePath);
							lGenerateur.CreerDocumentAttestation();
							break;
					}
				}

				Messenger.Default.Send(
					new NotificationMessageUtilisateur(
						TypesNotification.Information,
						ResMessages.MessageInfoGenerationDocumentsGroupe
					)
				);
			}
		}

		private void GenererVCardsGroupe(string pSaveFolder) {
			if (pSaveFolder != null) {
				foreach (Inscription lInscription in this.Groupe.Inscriptions) {
					DonneesVCard lDonnees = DonneesVCardAdaptateur.CreerDonneesVCard(lInscription);
					string lSaveFilePath = pSaveFolder + "\\" + lInscription.Adherent.ToString() + ResVCards.Extension;

					VCardGenerateur lGenerateur = new VCardGenerateur(lDonnees, lSaveFilePath);
					lGenerateur.CreerVCard();
				}

				Messenger.Default.Send(
					new NotificationMessageUtilisateur(
						TypesNotification.Information,
						ResMessages.MessageInfoGenerationVCardsGroupe
					)
				);
			}
		}
		#endregion		
	}
}
