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
	public class ConsultationAdherentsUCViewModel : ViewModelBaseConsultation
	{
		private Adherent mAdherent;
		private ICollectionView mAdherents;

		public ConsultationAdherentsUCViewModel() {
			this.InitialisationListeAdherents();
			this.CreateAfficherDetailsAdherentCommand();
			this.CreateSupprimerAdherentCommand();
			base.CreateCreerCommand();
			this.CreateEditerCommand();
			this.CreateInscrireCommand();
		}

		public bool CanExecuteEditerCommand(string pCodeUC) {
			return (this.Adherent != null);
		}

		public bool CanExecuteInscrireCommand(string pCodeUC) {
			return (this.Adherent != null);
		}

		public bool CanExecuteSupprimerAdherentCommand() {
			return (
				this.Adherent != null 
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent) 
				&& !AdherentDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Adherent)
				);
		}

		private void CreateAfficherDetailsAdherentCommand() {
			this.AfficherDetailsAdherentCommand = new RelayCommand<Adherent>(
				this.ExecuteAfficherDetailsAdherentCommand
			);
		}

		private void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand<string>(
				this.ExecuteEditerCommand, 
				this.CanExecuteEditerCommand
			);
		}

		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand<string>(
				this.ExecuteInscrireCommand, 
				this.CanExecuteInscrireCommand
			);
		}

		private void CreateSupprimerAdherentCommand() {
			this.SupprimerAdherentCommand = new RelayCommand(
				this.ExecuteSupprimerAdherentCommand, 
				this.CanExecuteSupprimerAdherentCommand
			);
		}

		public void ExecuteAfficherDetailsAdherentCommand(Adherent pAdherent) {
			if (pAdherent != null) {
				this.Adherent = pAdherent;
			}
		}

		public void ExecuteEditerCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageFormulaire<string, Adherent>>(new NotificationMessageFormulaire<string, Adherent>(pCodeUC, TypesNotification.ChangementUC, this.Adherent));
		}

		public void ExecuteInscrireCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageFormulaire<string, Adherent>>(new NotificationMessageFormulaire<string, Adherent>(pCodeUC, TypesNotification.ChangementUC, this.Adherent));
		}

		public void ExecuteSupprimerAdherentCommand() {
			if (this.Adherent != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprAdherent, 
					this.ExecuteSupprimerAdherentCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
				this.CreateSupprimerAdherentCommand();
			}
		}

		private void ExecuteSupprimerAdherentCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				AdherentDao.GetInstance(ViewModelLocator.Context).Delete(this.Adherent);
				this.InitialisationListeAdherents();
				this.Adherent = null;
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(AdherentDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
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

		public ICommand AfficherDetailsAdherentCommand { get; set; }

		public ICommand EditerCommand { get; set; }

		public ICommand InscrireCommand { get; set; }

		public ICommand SupprimerAdherentCommand { get; set; }
	}
}
