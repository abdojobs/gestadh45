using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Saisons
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
					this.RaisePropertyChanged(() => this.Saison);
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
					this.RaisePropertyChanged(() => this.Saisons);
				}
			}
		}

		public ConsultationSaisonsUCViewModel() {
			this.InitialisationListeSaisons();

			this.CreateDefinirSaisonCouranteCommand();

			Messenger.Default.Register<MsgSelectionElement<Saison>>(this, this.SelectionnerSaison);
		}

		public bool CanExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			return (
				pSaison != null 
				&& !pSaison.EstSaisonCourante
				);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Saison != null
				&& ViewModelLocator.DaoSaison.Exists(this.Saison)
				&& !ViewModelLocator.DaoSaison.IsUsed(this.Saison)
				&& !this.Saison.EstSaisonCourante	// on ne peut pas supprimer la saison courante
			);
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
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireSaison);
		}

		public void ExecuteDefinirSaisonCouranteCommand(Saison pSaison) {
			if (pSaison != null) {
				Saison lOldSaisonCourante = ViewModelLocator.DaoSaison.ReadSaisonCourante();
				lOldSaisonCourante.EstSaisonCourante = false;
				ViewModelLocator.DaoSaison.Update(lOldSaisonCourante);

				pSaison.EstSaisonCourante = true;

				ViewModelLocator.DaoSaison.Update(pSaison);
				this.InitialisationListeSaisons();

				this.Saison = null;
				this.Saison = pSaison;

				Messenger.Default.Send<NotificationMessage<Saison>>(
					new NotificationMessage<Saison>(pSaison, TypesNotification.ChangementSaisonCourante)
				);
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
		}

		private void ExecuteSupprimerSaisonCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				ViewModelLocator.DaoSaison.Delete(this.Saison);
				this.InitialisationListeSaisons();
				this.Saison = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionSaison);
			}
		}

		private void InitialisationListeSaisons() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				ViewModelLocator.DaoSaison.List()
			);

			foreach (Saison lSaison in defaultView) {
				ViewModelLocator.DaoSaison.Refresh(lSaison);
			}

			defaultView.SortDescriptions.Add(new SortDescription("AnneeDebut", ListSortDirection.Ascending));
			this.Saisons = defaultView;
		}

		private void SelectionnerSaison(MsgSelectionElement<Saison> msg) {
			this.Saison = msg.Content;
			this.RaisePropertyChanged(() => this.Saison);
		}
	}
}
