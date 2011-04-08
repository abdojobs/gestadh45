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

		/// <summary>
		/// Obtient/Définit l'adhérent à afficher
		/// </summary>
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

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
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
		public ICommand InscrireCommand { get; set; }

		public ConsultationAdherentsUCViewModel() {
			this.InitialisationListeAdherents();

			this.CreateAfficherDetailsAdherentCommand();
			this.CreateInscrireCommand();
		}

		public override bool CanExecuteEditerCommand() {
			return (this.Adherent != null);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Adherent != null
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)
				&& !AdherentDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Adherent)
				);
		}

		public bool CanExecuteInscrireCommand() {
			return (this.Adherent != null);
		}

		private void CreateAfficherDetailsAdherentCommand() {
			this.AfficherDetailsAdherentCommand = new RelayCommand<Adherent>(
				this.ExecuteAfficherDetailsAdherentCommand
			);
		}

		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand(
				this.ExecuteInscrireCommand, 
				this.CanExecuteInscrireCommand
			);
		}

		public void ExecuteAfficherDetailsAdherentCommand(Adherent pAdherent) {
			if (pAdherent != null) {
				this.Adherent = pAdherent;
			}
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireAdherent)
			);
		}

		public override void ExecuteEditerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC<Adherent>>(
				new NotificationMessageChangementUC<Adherent>(
					CodesUC.FormulaireAdherent, 
					this.Adherent
				)
			);
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Adherent != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprAdherent,
					this.ExecuteSupprimerAdherentCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
				this.CreateSupprimerCommand();
			}
		}

		public void ExecuteInscrireCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC<Adherent>>(
				new NotificationMessageChangementUC<Adherent>(
					CodesUC.FormulaireInscription,
					this.Adherent
				)
			);
		}

		/// <summary>
		/// Méthode appellée à la réception de la réponse au message de confirmation de suppression
		/// </summary>
		/// <param name="pResult">Résultat de la demande de confirmation</param>
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
	}
}
