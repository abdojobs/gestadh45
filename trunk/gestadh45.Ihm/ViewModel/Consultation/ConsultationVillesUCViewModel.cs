using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
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
		private IVilleDao mDaoVille;

		/// <summary>
		/// Obtient/Définit la ville à afficher
		/// </summary>
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

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
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

		public ConsultationVillesUCViewModel() {
			this.mDaoVille = this.mDaoFactory.GetVilleDao();
			this.InitialisationListeVilles();
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Ville != null
				&& this.mDaoVille.Exists(this.Ville)
				&& !this.mDaoVille.IsUsed(this.Ville)
				);
		}

		public override void ExecuteAfficherDetailsCommand(object pVille) {
			if (pVille != null && pVille is Ville) {
				this.Ville = pVille as Ville;
			}
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Ville != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprVille, 
					this.ExecuteSupprimerVilleCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
		}

		private void ExecuteSupprimerVilleCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this.mDaoVille.Delete(this.Ville);
				this.InitialisationListeVilles();
				this.Ville = null;
			}
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				this.mDaoVille.List()
			);

			foreach (Ville lVille in defaultView) {
				this.mDaoVille.Refresh(lVille);
			}

			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireVille)
			);
		}
	}
}
