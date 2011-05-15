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
		public ICommand InscrireCommand { get; set; }
		
		private Adherent mAdherent;
		private ICollectionView mAdherents;

		private IAdherentDao mDaoAdherent;

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
					this.RaisePropertyChanged(()=>this.Adherent);
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
					this.RaisePropertyChanged(()=>this.Adherents);
				}
			}
		}

		public ConsultationAdherentsUCViewModel() {
			this.mDaoAdherent = this.mDaoFactory.GetAdherentDao();

			this.InitialisationListeAdherents();

			this.CreateInscrireCommand();

			Messenger.Default.Register<NotificationMessageSelectionElement<Adherent>>(this, this.SelectionnerAdherent);
		}

		public override bool CanExecuteEditerCommand() {
			return (this.Adherent != null);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Adherent != null
				&& this.mDaoAdherent.Exists(this.Adherent)
				&& !this.mDaoAdherent.IsUsed(this.Adherent)
				);
		}

		public bool CanExecuteInscrireCommand() {
			return (this.Adherent != null);
		}

		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand(
				this.ExecuteInscrireCommand, 
				this.CanExecuteInscrireCommand
			);
		}

		public override void ExecuteAfficherDetailsCommand(object pAdherent) {
			if (pAdherent != null && pAdherent is Adherent) {
				this.Adherent = pAdherent as Adherent;
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
			}
		}

		public override void ExecuteFiltrerListeCommand(string pFiltre) {
			if (string.IsNullOrEmpty(pFiltre)) {
				this.Adherents.Filter = null;
			}
			else {
				this.Adherents.Filter = (p) => ((Adherent)p).ToString().ToUpper().Contains(pFiltre.ToUpper());
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
				this.mDaoAdherent.Delete(this.Adherent);
				this.InitialisationListeAdherents();
				this.Adherent = null;
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void SelectionnerAdherent(NotificationMessageSelectionElement<Adherent> msg) {
			this.Adherent = msg.Content;
			this.RaisePropertyChanged(() => this.Adherent);
		}
	}
}
