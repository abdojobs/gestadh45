﻿using System.ComponentModel;
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
	public class ConsultationSaisonsUCViewModel : ViewModelBaseConsultation
	{
		private Saison mSaison;
		private ICollectionView mSaisons;

		public ConsultationSaisonsUCViewModel() {
			this.InitialisationListeSaisons();
			this.CreateDefinirSaisonCouranteCommand();
			this.CreateAfficherDetailsSaisonCommand();
			this.CreateSupprimerSaisonCommand();
			this.CreateCreerCommand();
		}

		public bool CanExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			return (
				pSaison != null 
				&& !pSaison.EstSaisonCouranteBool
				);
		}

		public bool CanExecuteSupprimerSaisonCommand() {
			return (
				this.Saison != null 
				&& SaisonDao.GetInstance(ViewModelLocator.Context).Exist(this.Saison) 
				&& !SaisonDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Saison)
			);
		}

		private void CreateAfficherDetailsSaisonCommand() {
			this.AfficherDetailsSaisonCommand = new RelayCommand<Saison>(
				this.ExecuteAfficherDetailsSaisonCommand
			);
		}

		private void CreateDefinirSaisonCouranteCommand() {
			this.DefinirSaisonCouranteCommand = new RelayCommand<Saison>(
				this.ExecuteDefinirSaisonCouranteCommand, 
				this.CanExecuteDefinirSaisonCouranteCommand
			);
		}

		private void CreateSupprimerSaisonCommand() {
			this.SupprimerSaisonCommand = new RelayCommand(
				this.ExecuteSupprimerSaisonCommand, 
				this.CanExecuteSupprimerSaisonCommand
			);
		}

		public void ExecuteAfficherDetailsSaisonCommand(Saison pSaison) {
			if (pSaison != null) {
				this.Saison = pSaison;
			}
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireSaison)
			);
		}

		public void ExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			if (pSaison != null) {
				Saison lOldSaisonCourante = SaisonDao.GetInstance(ViewModelLocator.Context).ReadSaisonCourante();
				lOldSaisonCourante.EstSaisonCouranteBool = false;
				SaisonDao.GetInstance(ViewModelLocator.Context).Update(lOldSaisonCourante);

				pSaison.EstSaisonCouranteBool = true;

				SaisonDao.GetInstance(ViewModelLocator.Context).Update(pSaison);
				this.InitialisationListeSaisons();

				this.Saison = null;
				this.Saison = pSaison;

				Messenger.Default.Send<NotificationMessage<Saison>>(
					new NotificationMessage<Saison>(pSaison, TypesNotification.ChangementSaisonCourante)
				);
			}
		}

		public void ExecuteSupprimerSaisonCommand() {
			if (this.Saison != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprSaison, 
					this.ExecuteSupprimerSaisonCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerSaisonCommand();
		}

		private void ExecuteSupprimerSaisonCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				SaisonDao.GetInstance(ViewModelLocator.Context).Delete(this.Saison);
				this.InitialisationListeSaisons();
				this.Saison = null;
			}
		}

		private void InitialisationListeSaisons() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				SaisonDao.GetInstance(ViewModelLocator.Context).List()
			);

			foreach (Saison lSaison in defaultView) {
				SaisonDao.GetInstance(ViewModelLocator.Context).Refresh(lSaison);
			}

			defaultView.SortDescriptions.Add(new SortDescription("AnneeDebut", ListSortDirection.Ascending));
			this.Saisons = defaultView;
		}

		public ICommand AfficherDetailsSaisonCommand { get; set; }

		public ICommand DefinirSaisonCouranteCommand { get; set; }

		public Saison Saison {
			get {
				return this.mSaison;
			}
			set {
				if (this.mSaison != value) {
					this.mSaison = value;
					this.RaisePropertyChanged("Saison");
				}
			}
		}

		public ICollectionView Saisons {
			get {
				return this.mSaisons;
			}
			set {
				if (this.mSaisons != value) {
					this.mSaisons = value;
					this.RaisePropertyChanged("Saisons");
				}
			}
		}

		public ICommand SupprimerSaisonCommand { get; set; }
	}
}