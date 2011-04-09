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
	public class ConsultationGroupesUCViewModel : ViewModelBaseConsultation
	{
		public ICommand GenererDocumentsGroupeCommand { get; set; }
		
		private Groupe mGroupe;
		private ICollectionView mGroupesSaisonCourante;

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
			this.InitialisationListeGroupes();

			this.CreateGenererDocumentsGroupeCommand();
		}

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

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Groupe != null 
				&& GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe) 
				&& !GroupeDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Groupe)
			);
		}

		public override void ExecuteAfficherDetailsCommand(object pGroupe) {
			if (pGroupe != null && pGroupe is Groupe) {
				this.Groupe = pGroupe as Groupe;
			}
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
				GroupeDao.GetInstance(ViewModelLocator.Context).Delete(this.Groupe);
				this.InitialisationListeGroupes();
				this.Groupe = null;
			}
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				GroupeDao.GetInstance(ViewModelLocator.Context).ListSaisonCourante()
			);

			foreach (Groupe lGroupe in defaultView) {
				GroupeDao.GetInstance(ViewModelLocator.Context).Refresh(lGroupe);
			}

			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireGroupe)
			);
		}

		private void GenererDocumentsGroupe(string pSaveFolder, string pCodeDocument) {
			if (pSaveFolder != null) {
				InfosClub lInfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();

				try {
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
				catch (Exception lEx) {
					NotificationMessageUtilisateur message =
						new NotificationMessageUtilisateur(
							TypesNotification.Erreur,
							lEx.Message
						);

					Messenger.Default.Send<NotificationMessageUtilisateur>(message);
				}
			}
		}
	}
}
