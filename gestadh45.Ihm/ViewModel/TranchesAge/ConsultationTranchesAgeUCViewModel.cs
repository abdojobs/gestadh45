using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.TranchesAge
{
	public class ConsultationTranchesAgeUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
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
				ViewModelLocator.DaoTrancheAge.Delete(this.TrancheAge);
				this.InitialisationListeTranchesAge();
				this.TrancheAge = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionTrancheAge);
			}
		}
		#endregion

		#region EditerCommand
		public override bool CanExecuteEditerCommand() {
			return (this.TrancheAge != null);
		}

		public override void ExecuteEditerCommand() {
			base.ExecuteEditerCommand();

			Messenger.Default.Send<MsgAfficherUC<TrancheAge>>(
				new MsgAfficherUC<TrancheAge>(
					CodesUC.FormulaireTrancheAge,
					MsgAfficherUC.TypeAffichage.Interne,
					this.TrancheAge
				)
			);
		}
		#endregion

		#region AfficherDetailsCommand
		public override void ExecuteAfficherDetailsCommand(object pTrancheAge) {
			if (pTrancheAge != null && pTrancheAge is TrancheAge) {
				this.TrancheAge = pTrancheAge as TrancheAge;
			}
		}
		#endregion

		#region CreerCommand
		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireTrancheAge);
		}
		#endregion

		#region private methods
		private void InitialisationListeTranchesAge() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoTrancheAge.List());
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
