using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class ConsultationCampagnesVerificationVM : VMConsultationBase
	{
		#region CampagnesVerification
		private IOrderedEnumerable<CampagneVerification> _campagnesVerification;

		/// <summary>
		/// Gets or sets the campagnes verification.
		/// </summary>
		/// <value>
		/// The campagnes verification.
		/// </value>
		public IOrderedEnumerable<CampagneVerification> CampagnesVerification {
			get { return this._campagnesVerification; }
			set {
				if (this._campagnesVerification != value) {
					this._campagnesVerification = value;
					this.RaisePropertyChanged(() => this.CampagnesVerification);
				}
			}
		}
		#endregion

		#region SelectedCampagneVerification
		private CampagneVerification _selectedCampagneVerification;

		/// <summary>
		/// Gets or sets the selected campagne verification.
		/// </summary>
		/// <value>
		/// The selected campagne verification.
		/// </value>
		public CampagneVerification SelectedCampagneVerification {
			get { return this._selectedCampagneVerification; }
			set {
				if (this._selectedCampagneVerification != value) {
					this._selectedCampagneVerification = value;
					this.RaisePropertyChanged(() => this.SelectedCampagneVerification);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<CampagneVerification> _repoMain;
		private Repository<Verification> _repoVerifications;
		#endregion

		#region Constructeur
		public ConsultationCampagnesVerificationVM() {
			this.CreateRepositories();
			this.PopulateCampagnesVerification();
			this.CreateSaisirCommand();
		}
		#endregion

		private void CreateRepositories() {
			this._repoMain = new Repository<CampagneVerification>(this._context);
			this._repoVerifications = new Repository<Verification>(this._context);
		}

		private void PopulateCampagnesVerification() {
			this.CampagnesVerification = this._repoMain.GetAll().OrderBy(c => c.Date);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is CampagneVerification) {
				this.SelectedCampagneVerification = selectedItem as CampagneVerification;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedCampagneVerification != null && !this.SelectedCampagneVerification.EstFermee;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedCampagneVerification != null) {
				// suppression des verifications associées
				for (int i = 0; i < this.SelectedCampagneVerification.Verifications.Count; i++) {
					this._repoVerifications.Delete(this.SelectedCampagneVerification.Verifications.ElementAt(i));
				}

				this._repoVerifications.Save();
				
				// suppression de la campagne
				this._repoMain.Delete(this.SelectedCampagneVerification);
				this._repoMain.Save();

				this.PopulateCampagnesVerification();
				this.SelectedCampagneVerification = this.CampagnesVerification.FirstOrDefault();

				this.ShowUserNotification(ResCampagnesVerification.InfoCampagneVerificationSupprimee);
			}
		}
		#endregion

		#region EditCommand
		public override bool CanExecuteEditCommand() {
			return this.SelectedCampagneVerification != null;
		}

		public override void ExecuteEditCommand() {
			if (this.SelectedCampagneVerification != null) {
				Messenger.Default.Send<NMShowUC<CampagneVerification>>(
					new NMShowUC<CampagneVerification>(CodesUC.FormulaireCampagneVerification, this.SelectedCampagneVerification)
				);
			}
		}
		#endregion

		#region CreateCommand
		public override bool CanExecuteCreateCommand() {
			// on ne peut créer une nouvelle campagne que si il n'en existe aucune déjà ouverte
			return this._repoMain.GetAll().Where(c => !c.EstFermee).Count() == 0;
		}

		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireCampagneVerification);
		}
		#endregion

		#region SaisirCommand
		public ICommand SaisirCommand { get; set; }

		private void CreateSaisirCommand() {
			this.SaisirCommand = new RelayCommand(
				this.ExecuteSaisirCommand,
				this.CanExecuteSaisirCommand
			);
		}

		public bool CanExecuteSaisirCommand() {
			return this.SelectedCampagneVerification != null && !this.SelectedCampagneVerification.EstFermee;
		}

		public void ExecuteSaisirCommand() {
			if (this.SelectedCampagneVerification != null && !this.SelectedCampagneVerification.EstFermee) {
				Messenger.Default.Send(
					new NMShowUC<CampagneVerification>(
						CodesUC.SaisieVerifications, 
						this.SelectedCampagneVerification
					)
				);
			}
		}
		#endregion
	}
}
