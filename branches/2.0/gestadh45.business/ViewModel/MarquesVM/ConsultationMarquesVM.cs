using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.MarquesVM
{
	public class ConsultationMarquesVM : VMConsultationBase
	{
		#region Marques
		private IOrderedEnumerable<Marque> _marques;

		/// <summary>
		/// Gets or sets the marques.
		/// </summary>
		/// <value>
		/// The marques.
		/// </value>
		public IOrderedEnumerable<Marque> Marques {
			get {
				return this._marques;
			}

			set {
				if (this._marques != value) {
					this._marques = value;
					this.RaisePropertyChanged(() => this.Marques);
				}
			}
		}
		#endregion

		#region SelectedMarque
		private Marque _selectedMarque;

		/// <summary>
		/// Gets or sets the marque.
		/// </summary>
		/// <value>
		/// The marque.
		/// </value>
		public Marque SelectedMarque {
			get {
				return this._selectedMarque;
			}

			set {
				if (this._selectedMarque != value) {
					this._selectedMarque = value;
					this.RaisePropertyChanged(() => this.SelectedMarque);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Marque> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationMarquesVM() {
			this._repoMain = new Repository<Marque>(this._context);
			this.PopulateMarques();
		}
		#endregion

		private void PopulateMarques() {
			this.Marques = this._repoMain.GetAll().OrderBy((m) => m.ToString());
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Marque) {
				this.SelectedMarque = selectedItem as Marque;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedMarque != null && this.SelectedMarque.Equipements.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedMarque != null) {
				this._repoMain.Delete(this.SelectedMarque);
				this._repoMain.Save();
				this.PopulateMarques();
				this.SelectedMarque = this.Marques.FirstOrDefault();

				this.ShowUserNotification(ResMarques.InfoMarqueSupprimee);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireMarque));
		}
		#endregion
	}
}
