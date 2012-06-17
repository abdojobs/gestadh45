using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.TranchesAgeVM
{
	public class ConsultationTranchesAgeVM : VMConsultationBase
	{
		#region TrancheAges
		private IOrderedEnumerable<TrancheAge> _tranchesAge;

		/// <summary>
		/// Obtient/Définit la liste des tranches d'âge
		/// </summary>
		public IOrderedEnumerable<TrancheAge> TranchesAge {
			get { return this._tranchesAge; }
			set {
				if (this._tranchesAge != value) {
					this._tranchesAge = value;
					this.RaisePropertyChanged(() => this.TranchesAge);
				}
			}
		}
		#endregion

		#region SelectedTrancheAge
		private TrancheAge _selectedTrancheAge;

		/// <summary>
		/// Obtient/Définit la tranche d'âge sélectionnée
		/// </summary>
		public TrancheAge SelectedTrancheAge {
			get { return this._selectedTrancheAge; }
			set {
				if (this._selectedTrancheAge != value) {
					this._selectedTrancheAge = value;
					this.RaisePropertyChanged(() => this.SelectedTrancheAge);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<TrancheAge> _repoMain;
		#endregion

		public ConsultationTranchesAgeVM() {
			this._repoMain = new Repository<TrancheAge>(this._context);
			this.PopulateTranchesAge();
		}

		private void PopulateTranchesAge() {
			this.TranchesAge = this._repoMain.GetAll().OrderBy(t => t.AgeInf);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is TrancheAge) {
				this.SelectedTrancheAge = selectedItem as TrancheAge;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedTrancheAge != null;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedTrancheAge != null) {
				this._repoMain.Delete(this.SelectedTrancheAge);
				this._repoMain.Save();
				this.PopulateTranchesAge();
				this.SelectedTrancheAge = this.TranchesAge.FirstOrDefault();

				this.ShowUserNotification(ResTranchesAge.InfoTrancheAgeSupprimee);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireTrancheAge);
		}
		#endregion
	}
}
