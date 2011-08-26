using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Villes
{
	public class ConsultationVillesUCViewModel : ViewModelBaseConsultation
	{
		private Ville _ville;
		private ICollectionView _villes;
		private IVilleDao _daoVille;

		/// <summary>
		/// Obtient/Définit la ville à afficher
		/// </summary>
		public Ville Ville {
			get {
				return this._ville;
			}
			set {
				if (this._ville != value) {
					this._ville = value;
					this.RaisePropertyChanged(() => this.Ville);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public ICollectionView Villes {
			get {
				return this._villes;
			}
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}

		public ConsultationVillesUCViewModel() {
			this._daoVille = DaoFactory.GetVilleDao(ViewModelLocator.DataSource);
			this.InitialisationListeVilles();

			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this, 
				(msg) => this.SelectionnerVille(msg.Content)
			);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Ville != null
				&& this._daoVille.Exists(this.Ville)
				&& !this._daoVille.IsUsed(this.Ville)
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
				this._daoVille.Delete(this.Ville);
				this.InitialisationListeVilles();
				this.Ville = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionVille);
			}
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				this._daoVille.List()
			);

			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireVille);
		}

		private void SelectionnerVille(Ville pVille) {
			this.Ville = pVille;
			this.RaisePropertyChanged(() => this.Ville);
		}
	}
}
