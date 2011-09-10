﻿using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.dao;
using gestadh45.Ihm.ServiceAdaptateurs;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.service.Documents;
using gestadh45.service.VCards;

namespace gestadh45.Ihm.ViewModel.Groupes
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
					this.RaisePropertyChanged(() => this.Groupe);
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

		public ConsultationGroupesUCViewModel() {
			this.mDaoInfosCLub = this.mDaoFactory.GetInfosClubDao();
			this.mDaoGroupe = this.mDaoFactory.GetGroupeDao();

			this.InitialisationListeGroupes();

			this.CreateGenererDocumentsGroupeCommand();
			this.CreateGenererVCardsGroupeCommand();
			this.CreateExtraireMailsCommand();

			Messenger.Default.Register<NotificationMessageSelectionElement<Groupe>>(this, this.SelectionnerGroupe);
		}

		#region CreerCommand
		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireGroupe);
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
		}

		private void ExecuteSupprimerGroupeCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this.mDaoGroupe.Delete(this.Groupe);
				this.InitialisationListeGroupes();
				this.Groupe = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionGroupe);
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
				lSb.Append(lInscription.Adherent.ChaineMails);
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
				InfosClub infosClub = this.mDaoInfosCLub.Read();

				foreach (Inscription inscription in this.Groupe.Inscriptions) {
					DonneesDocument donnees = ServiceDocumentAdaptateur.InscriptionToDonneesDocument(infosClub, inscription);

					string saveFilePath;
					GenerateurDocumentBase lGenerateur;

					switch (pCodeDocument) {
						case GenerateurDocumentBase.CodeInscriptionPdf:
							saveFilePath = string.Format(
								"{0}\\{1} - {2}{3}",
								pSaveFolder,
								ResDocuments.PrefixeNomFichierInscription,
								inscription.Adherent.ToString(),
								ResDocuments.ExtensionFichierPdf
							);

							lGenerateur = new GenerateurDocumentPDF(donnees, saveFilePath);
							lGenerateur.CreerDocumentInscription();
							break;

						case GenerateurDocumentBase.CodeAttestationPdf:
							saveFilePath = string.Format(
								"{0}\\{1} - {2}{3}",
								pSaveFolder,
								ResDocuments.PrefixeNomFichierAttestation,
								inscription.Adherent.ToString(),
								ResDocuments.ExtensionFichierPdf
							);

							lGenerateur = new GenerateurDocumentPDF(donnees, saveFilePath);
							lGenerateur.CreerDocumentAttestation();
							break;
					}
				}

				this.AfficherInformationIhm(ResMessages.MessageInfoGenerationDocumentsGroupe);
			}
		}

		private void GenererVCardsGroupe(string pSaveFolder) {
			if (pSaveFolder != null) {
				foreach (Inscription inscription in this.Groupe.Inscriptions) {
					DonneesVCard donnees = ServiceVCardAdaptateur.InscriptionToDonneesVCard(inscription);
					string saveFilePath = pSaveFolder + "\\" + inscription.Adherent.ToString() + ResVCards.Extension;

					VCardGenerateur generateur = new VCardGenerateur(donnees, saveFilePath);
					generateur.CreerVCard();
				}

				this.AfficherInformationIhm(ResMessages.MessageInfoGenerationVCardsGroupe);
			}
		}

		private void SelectionnerGroupe(NotificationMessageSelectionElement<Groupe> msg) {
			this.Groupe = msg.Content;
			this.RaisePropertyChanged(() => this.Groupe);
		}
		#endregion		
	}
}