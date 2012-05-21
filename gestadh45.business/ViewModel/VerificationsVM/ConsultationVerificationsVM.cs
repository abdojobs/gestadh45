using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.VerificationsVM
{
	public class ConsultationVerificationsVM : VMConsultationBase
	{
		#region Verifications
		private IOrderedEnumerable<Verification> _verifications;

		/// <summary>
		/// Gets or sets the verifications.
		/// </summary>
		/// <value>
		/// The verifications.
		/// </value>
		public IOrderedEnumerable<Verification> Verifications {
			get { return this._verifications; }
			set {
				if (this._verifications != value) {
					this._verifications = value;
					this.RaisePropertyChanged(() => this.Verifications);
				}
			}
		}
		#endregion

		#region SelectedVerification
		private Verification _selectedVerification;

		/// <summary>
		/// Gets or sets the selected verification.
		/// </summary>
		/// <value>
		/// The selected verification.
		/// </value>
		public Verification SelectedVerification {
			get { return this._selectedVerification; }
			set {
				if (this._selectedVerification != value) {
					this._selectedVerification = value;
					this.RaisePropertyChanged(() => this.SelectedVerification);
				}
			}
		}
		#endregion

		#region repository
		private Repository<Verification> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationVerificationsVM() {
			this._repoMain = new Repository<Verification>(this._context);
			this.PopulateVerifications();
		}
		#endregion

		private void PopulateVerifications() {
			this.Verifications = this._repoMain.GetAll()
				.OrderByDescending(v => v.DateVerification)
				.ThenBy(v => v.Equipement.Numero);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Verification) {
				this.SelectedVerification = selectedItem as Verification;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return true;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedVerification != null) {
				this._repoMain.Delete(this.SelectedVerification);
				this._repoMain.Save();
				this.PopulateVerifications();
				this.SelectedVerification = this.Verifications.FirstOrDefault();

				this.ShowUserNotification(ResVerifications.InfoVerificationSupprimee);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireVerification));
		}
		#endregion
	}
}
