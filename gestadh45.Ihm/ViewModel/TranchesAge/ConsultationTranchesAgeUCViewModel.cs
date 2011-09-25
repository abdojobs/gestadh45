using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using System.Windows;

namespace gestadh45.Ihm.ViewModel.TranchesAge
{
	public class ConsultationTranchesAgeUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
		private ITrancheAgeDao _daoTrancheAge;

		private ICollectionView _tranchesAge;
		private TrancheAge _trancheAge;
		#endregion

		#region properties
		/// <summary>
		/// Obtient/Définit la collection de tranches d'âge
		/// </summary>
		public ICollectionView TranchesAge {
			get { return this._tranchesAge; }
			set {
				if (this._tranchesAge != value) {
					this._tranchesAge = value;
					this.RaisePropertyChanged(() => this.TranchesAge);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la tranche d'âge courante
		/// </summary>
		public TrancheAge TrancheAge {
			get { return this._trancheAge; }
			set {
				if (this._trancheAge != value) {
					this._trancheAge = value;

					this.RaisePropertyChanged(() => this.TrancheAge);
				}
			}
		}
		#endregion

		#region constructor
		public ConsultationTranchesAgeUCViewModel() {
			this._daoTrancheAge = this.mDaoFactory.GetTrancheAgeDao();
			this.InitialisationListeTranchesAge();

			Messenger.Default.Register<MsgSelectionElement<TrancheAge>>(this, this.SelectionnerTrancheAge);
		}
		#endregion

		#region SupprimerCommand
		public override bool CanExecuteSupprimerCommand() {
			return this.TrancheAge != null;
		}

		public override void ExecuteSupprimerCommand() {
			if (this.TrancheAge != null) {
				DialogMessageConfirmation message =
					new DialogMessageConfirmation(
						ResMessages.MessageConfirmSupprTrancheAge,
						this.ExecuteSupprimerTrancheAgeCommandCallBack
					);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
		}

		private void ExecuteSupprimerTrancheAgeCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this._daoTrancheAge.Delete(this.TrancheAge);
				this.InitialisationListeTranchesAge();
				this.TrancheAge = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionTrancheAge);
			}
		}
		#endregion

		#region private methods
		private void InitialisationListeTranchesAge() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoTrancheAge.List());
			defaultView.SortDescriptions.Add(new SortDescription("AgeInf", ListSortDirection.Ascending));
			this.TranchesAge = defaultView;
		}

		private void SelectionnerTrancheAge(MsgSelectionElement<TrancheAge> msg) {
			this.TrancheAge = msg.Content;
			this.RaisePropertyChanged(() => this.TrancheAge);
		}
		#endregion
	}
}
