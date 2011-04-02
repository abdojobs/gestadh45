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
	public class ConsultationVillesUCViewModel : ViewModelBaseConsultation
	{
		private Ville mVille;
		private ICollectionView mVilles;

		public ConsultationVillesUCViewModel() {
			this.InitialisationListeVilles();
			this.CreateAfficherDetailsVilleCommand();
			this.CreateSupprimerVilleCommand();
			base.CreateCreerCommand();
		}

		public bool CanExecuteSupprimerVilleCommand() {
			return (
				this.Ville != null
				&& VilleDao.GetInstance(ViewModelLocator.Context).Exist(this.Ville)
				&& !VilleDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Ville)
				);
		}

		private void CreateAfficherDetailsVilleCommand() {
			this.AfficherDetailsVilleCommand = new RelayCommand<Ville>(
				this.ExecuteAfficherDetailsVilleCommand
			);
		}

		private void CreateSupprimerVilleCommand() {
			this.SupprimerVilleCommand = new RelayCommand(
				this.ExecuteSupprimerVilleCommand, 
				this.CanExecuteSupprimerVilleCommand
			);
		}

		public void ExecuteAfficherDetailsVilleCommand(Ville pVille) {
			if (pVille != null) {
				this.Ville = pVille;
			}
		}

		public void ExecuteSupprimerVilleCommand() {
			if (this.Ville != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprVille, 
					this.ExecuteSupprimerVilleCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerVilleCommand();
		}

		private void ExecuteSupprimerVilleCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				VilleDao.GetInstance(ViewModelLocator.Context).Delete(this.Ville);
				this.InitialisationListeVilles();
				this.Ville = null;
			}
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				VilleDao.GetInstance(ViewModelLocator.Context).List()
			);

			foreach (Ville lVille in defaultView) {
				VilleDao.GetInstance(ViewModelLocator.Context).Refresh(lVille);
			}

			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public ICommand AfficherDetailsVilleCommand { get; set; }

		public ICommand SupprimerVilleCommand { get; set; }

		public Ville Ville {
			get {
				return this.mVille;
			}
			set {
				if (this.mVille != value) {
					this.mVille = value;
					this.RaisePropertyChanged("Ville");
				}
			}
		}

		public ICollectionView Villes {
			get {
				return this.mVilles;
			}
			set {
				if (this.mVilles != value) {
					this.mVilles = value;
					this.RaisePropertyChanged("Villes");
				}
			}
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireVille)
			);
		}
	}
}
