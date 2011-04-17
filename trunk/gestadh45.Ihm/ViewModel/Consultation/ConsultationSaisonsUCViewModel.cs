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
	public class ConsultationSaisonsUCViewModel : ViewModelBaseConsultation
	{
		public ICommand DefinirSaisonCouranteCommand { get; set; }

		private Saison mSaison;
		private ICollectionView mSaisons;

		/// <summary>
		/// Obtient/Définit la saison à afficher
		/// </summary>
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

		/// <summary>
		/// Obtient/Définit la liste des saisons
		/// </summary>
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

		public ConsultationSaisonsUCViewModel() {
			this.InitialisationListeSaisons();

			this.CreateDefinirSaisonCouranteCommand();
		}

		public bool CanExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			return (
				pSaison != null 
				&& !pSaison.EstSaisonCouranteBool
				);
		}

		public override bool CanExecuteSupprimerCommand() {
			try {
				return (
					this.Saison != null
					&& SaisonDao.GetInstance(ViewModelLocator.Context).Exist(this.Saison)
					&& !SaisonDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Saison)
					&& !this.Saison.EstSaisonCouranteBool	// on ne peut pas supprimer la saison courante
				);
			}
			catch (Exception lEx) {
				this.EnvoyerNotificationUtilisateur(TypesNotification.ErreurFatale, lEx.Message);
				return false;
			}
		}

		private void CreateDefinirSaisonCouranteCommand() {
			this.DefinirSaisonCouranteCommand = new RelayCommand<Saison>(
				this.ExecuteDefinirSaisonCouranteCommand, 
				this.CanExecuteDefinirSaisonCouranteCommand
			);
		}

		public override void ExecuteAfficherDetailsCommand(object pSaison) {
			if (pSaison != null && pSaison is Saison) {
				this.Saison = pSaison as Saison;
			}
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireSaison)
			);
		}

		public void ExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			if (pSaison != null) {
				try {
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
				catch (Exception lEx) {
					this.EnvoyerNotificationUtilisateur(TypesNotification.ErreurFatale, lEx.Message);
				}
			}
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Saison != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprSaison, 
					this.ExecuteSupprimerSaisonCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerCommand();
		}

		private void ExecuteSupprimerSaisonCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				try {
					SaisonDao.GetInstance(ViewModelLocator.Context).Delete(this.Saison);
					this.InitialisationListeSaisons();
					this.Saison = null;
				}
				catch (Exception lEx) {
					this.EnvoyerNotificationUtilisateur(TypesNotification.ErreurFatale, lEx.Message);
				}
			}
		}

		private void InitialisationListeSaisons() {
			try {
				ICollectionView defaultView = CollectionViewSource.GetDefaultView(
					SaisonDao.GetInstance(ViewModelLocator.Context).List()
				);

				foreach (Saison lSaison in defaultView) {
					SaisonDao.GetInstance(ViewModelLocator.Context).Refresh(lSaison);
				}

				defaultView.SortDescriptions.Add(new SortDescription("AnneeDebut", ListSortDirection.Ascending));
				this.Saisons = defaultView;
			}
			catch (Exception lEx) {
				this.EnvoyerNotificationUtilisateur(TypesNotification.ErreurFatale, lEx.Message);
			}
		}
	}
}
