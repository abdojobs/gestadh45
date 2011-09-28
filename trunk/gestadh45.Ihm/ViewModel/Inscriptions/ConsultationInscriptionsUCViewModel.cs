﻿using System.ComponentModel;
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

namespace gestadh45.Ihm.ViewModel.Inscriptions
{
	public class ConsultationInscriptionsUCViewModel : ViewModelBaseConsultation
	{	
		private Inscription mInscription;
		private ICollectionView mInscriptionsSaisonCourante;

		private IInscriptionDao mDaoInscription;
		private IInfosClubDao mDaoInfosClub;

		/// <summary>
		/// Obtient/Définit l'inscription à afficher
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

		/// <summary>
		/// Obtient/Définit la liste des inscriptions de la saison courante
		/// </summary>
		public ICollectionView InscriptionsSaisonCourante {
			get {
				return this.mInscriptionsSaisonCourante;
			}
			set {
				if (this.mInscriptionsSaisonCourante != value) {
					this.mInscriptionsSaisonCourante = value;
					this.RaisePropertyChanged(() => this.InscriptionsSaisonCourante);
				}
			}
		}

		public ConsultationInscriptionsUCViewModel() {
			this.mDaoInscription = this.mDaoFactory.GetInscriptionDao();
			this.mDaoInfosClub = this.mDaoFactory.GetInfosClubDao();

			this.InitialisationListeInscriptions();

			this.CreateGenererDocumentCommand();
			this.CreateGenererVCardCommand();
			this.CreateAfficherAdherentCommand();

			Messenger.Default.Register<MsgSelectionElement<Inscription>>(this, this.SelectionnerInscription);
		}

		#region CreerCommand
		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireInscription);
		}
		#endregion

		#region EditerCommand
		public override bool CanExecuteEditerCommand() {
			return (this.Inscription != null);
		}

		public override void ExecuteEditerCommand() {
			base.ExecuteEditerCommand();

			Messenger.Default.Send<MsgAfficherUC<Inscription>>(
				new MsgAfficherUC<Inscription>(
					CodesUC.FormulaireInscription, 
					MsgAfficherUC.TypeAffichage.Interne,
					this.Inscription
				)
			);
		}
		#endregion

		#region SupprimerCommand
		public override bool CanExecuteSupprimerCommand() {
			return this.Inscription != null;
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Inscription != null) {
				DialogMessageConfirmation message =
					new DialogMessageConfirmation(
						ResMessages.MessageConfirmSupprInscription,
						this.ExecuteSupprimerInscriptionCommandCallBack
					);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
		}

		private void ExecuteSupprimerInscriptionCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this.mDaoInscription.Delete(this.Inscription);
				this.InitialisationListeInscriptions();
				this.Inscription = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionInscription);
			}
		}
		#endregion

		#region AfficherDetailsCommand
		public override void ExecuteAfficherDetailsCommand(object pInscription) {
			if (pInscription != null && pInscription is Inscription) {
				this.Inscription = pInscription as Inscription;
			}
		}
		#endregion

		#region GenererDocumentCommand
		public ICommand GenererDocumentCommand { get; set; }

		private void CreateGenererDocumentCommand() {
			this.GenererDocumentCommand = new RelayCommand<string>(
				this.ExecuteGenererDocumentCommand,
				this.CanExecuteGenererDocumentCommand
			);
		}

		public bool CanExecuteGenererDocumentCommand(string pCodeDocument) {
			return (this.Inscription != null);
		}

		public void ExecuteGenererDocumentCommand(string pCodeDocument) {
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
		#endregion

		#region GenererVCardCommand
		public ICommand GenererVCardCommand { get; set; }

		private void CreateGenererVCardCommand() {
			this.GenererVCardCommand = new RelayCommand(
				this.ExecuteGenererVCardCommand,
				this.CanExecuteGenererVCardCommand
			);
		}

		public bool CanExecuteGenererVCardCommand() {
			return (this.Inscription != null);
		}

		public void ExecuteGenererVCardCommand() {
			if (this.Inscription != null) {
				NotificationMessageActionFileDialog<string> message =
					new NotificationMessageActionFileDialog<string>(
						TypesNotification.SaveFileDialog,
						ResVCards.Extension,
						this.Inscription.Adherent.ToString(),
						callbackmessage =>
						{
							this.GenererVCard(callbackmessage);
						}
					);

				Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
			}
		}
		#endregion

		#region AfficherAdherentCommand
		public ICommand AfficherAdherentCommand { get; set; }

		private void CreateAfficherAdherentCommand() {
			this.AfficherAdherentCommand = new RelayCommand(
				this.ExecuteAfficherAdherentCommand,
				this.CanExecuteAfficherAdherentCommand
			);
		}

		public bool CanExecuteAfficherAdherentCommand() {
			return (this.Inscription != null && this.Inscription.Adherent != null);
		}

		public void ExecuteAfficherAdherentCommand() {
			if (this.Inscription != null && this.Inscription.Adherent != null) {
				var msg = new MsgAfficherUC<Adherent>(CodesUC.ConsultationAdherents, MsgAfficherUC.TypeAffichage.Interne, this.Inscription.Adherent);
				Messenger.Default.Send(msg);
			}
		}
		#endregion

		#region methodes privees
		private void GenererDocument(string pSaveFilePath, string pCodeDocument) {
			if (!string.IsNullOrWhiteSpace(pSaveFilePath)) {
				InfosClub infosClub = mDaoInfosClub.Read();
				DonneesDocument donnees = ServiceDocumentAdaptateur.InscriptionToDonneesDocument(infosClub, this.Inscription);
				GenerateurDocumentPDF generateur = new GenerateurDocumentPDF(donnees, pSaveFilePath);

				generateur.CreerDocument(pCodeDocument);

				this.AfficherInformationIhm(ResMessages.MessageInfoGenerationDocument);
			}
		}

		private void GenererVCard(string pSaveFilePath) {
			if(!string.IsNullOrWhiteSpace(pSaveFilePath)) {
				DonneesVCard donnees = ServiceVCardAdaptateur.InscriptionToDonneesVCard(this.Inscription);

				VCardGenerateur generateur = new VCardGenerateur(donnees, pSaveFilePath);
				generateur.CreerVCard();

				this.AfficherInformationIhm(ResMessages.MessageInfoGenerationVCard);
			}
		}

		private void InitialisationListeInscriptions() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoInscription.ListSaisonCourante());
			defaultView.GroupDescriptions.Add(new PropertyGroupDescription("Groupe"));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.JourSemaine", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Groupe.HeureDebut", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Prenom", ListSortDirection.Ascending));
			this.InscriptionsSaisonCourante = defaultView;
		}

		private string CreerNomFichierDocument(string pCodeDocument) {
			string lRetour = string.Empty;

			switch (pCodeDocument) {
				case GenerateurDocumentBase.CodeInscriptionPdf:
					lRetour = string.Format(
						"{0} - {1}",
						ResDocuments.PrefixeNomFichierInscription, this.Inscription.Adherent.ToString());
					break;

				case GenerateurDocumentBase.CodeAttestationPdf:
					lRetour = string.Format(
						"{0} - {1}",
						ResDocuments.PrefixeNomFichierAttestation, this.Inscription.Adherent.ToString());
					break;
			}

			return lRetour;
		}

		private void SelectionnerInscription(MsgSelectionElement<Inscription> msg) {
			this.Inscription = msg.Content;
			this.RaisePropertyChanged(() => this.Inscription);
		}
		#endregion		
	}
}