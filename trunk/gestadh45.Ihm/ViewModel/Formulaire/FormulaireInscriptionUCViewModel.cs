﻿using System.Collections.Generic;
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
	public class FormulaireInscriptionUCViewModel : ViewModelBaseFormulaire
	{
		private ICollectionView mAdherents;
		private ICollectionView mGroupesSaisonCourante;
		private Inscription mInscription;

		public FormulaireInscriptionUCViewModel() {
			this.Inscription = new Inscription();
			this.InitialisationListeAdherents();
			this.InitialisationListeGroupes();
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
			if (this.Inscription != null && base.EstEdition) {
				InscriptionDao.GetInstance(ViewModelLocator.Context).Refresh(this.Inscription);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() 
				&& base.EstEdition 
				&& InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)) {

				InscriptionDao.GetInstance(ViewModelLocator.Context).Update(this.Inscription);
				Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>("ConsultationInscriptions", "ChangementUserControl"));
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition 
				&& !InscriptionDao.GetInstance(ViewModelLocator.Context).Exist(this.Inscription)) {

				InscriptionDao.GetInstance(ViewModelLocator.Context).Create(this.Inscription);
				Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>("ConsultationInscriptions", "ChangementUserControl"));
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(ResMessages.TypeNotification_Erreur, this.ChaineErreurs)
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

		public bool CertificatRemis {
			get {
				return (this.Inscription.CertificatMedicalRemis == 1L);
			}
			set {
				if ((this.Inscription.CertificatMedicalRemis == 1L) != value) {
					this.Inscription.CertificatMedicalRemis = 1L;
					this.RaisePropertyChanged("CertificatRemis");
					this.RaisePropertyChanged("Inscription");
				}
			}
		}

		public ICommand EnregistrerCommand { get; set; }

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

		private bool VerifierSaisie() {
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
