using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.dal;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class ConsultationCampagnesVerificationVM : VMConsultationBase
	{
		
		#region CampagnesVerification
		private IOrderedEnumerable<CampagneVerification> _campagnesVerification;

		/// <summary>
		/// Obtient/Définit la liste des campagnes de vérification
		/// </summary>
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
		/// Obtient/Définit la campagne de vérification sélectionnée
		/// </summary>
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
		private Repository<Verification> _repoVerification;
		#endregion

		#region Constructeur
		public ConsultationCampagnesVerificationVM() {
			this._repoMain = new Repository<CampagneVerification>(this._context);
			this._repoVerification = new Repository<Verification>(this._context);
			this.CreateSaisirCommand();

			this.PopulateCampagnesVerification();
		}
		#endregion

		private void PopulateCampagnesVerification() {
			this.CampagnesVerification = this._repoMain.GetAll().OrderBy(c => c.Date);
		}

		/// <summary>
		/// Supprime toutes les vérifications d'une campagne
		/// </summary>
		private  void DeleteVerifications() {
			while (this.SelectedCampagneVerification.Verifications.Count > 0) {
				this._repoVerification.Delete(this.SelectedCampagneVerification.Verifications.First());
			}

			this._repoVerification.Save();
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
			return this.SelectedCampagneVerification != null
				&& !this.SelectedCampagneVerification.EstValidee;	// une campagne ne peut être supprimée si son flag "EstValidee" est à true
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedCampagneVerification != null && !this.SelectedCampagneVerification.EstValidee) {
				Messenger.Default.Send(new NMAskConfirmationDialog<bool>(this.ExecuteDeleteCommandCallBack, ResCampagnesVerification.TexteConfirmationSuppression));
			}
		}

		private void ExecuteDeleteCommandCallBack(bool deleteConfirmation) {
			if (deleteConfirmation) {
				this.DeleteVerifications();

				this._repoMain.Delete(this.SelectedCampagneVerification);
				this._repoMain.Save();

				this.PopulateCampagnesVerification();
				this.SelectedCampagneVerification = this.CampagnesVerification.FirstOrDefault();
				this.ShowUserNotification(ResCampagnesVerification.InfoCampagneSupprimee);
			}
		}
		#endregion
	
		#region CreateCommand
		public override bool CanExecuteCreateCommand() {
			return this._repoMain.GetAll().Count(c => !c.EstValidee) == 0; // on ne peut créer une nouvelle campagne que si il n'en existe aucune non validée
		}

		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireCreationCampagneVerification));
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
			// la commande Saisir est activée seulement si une campagne est sélectionnée
			// ET qu'elle n'est pas encore validée
			return this.SelectedCampagneVerification != null
				&& !this.SelectedCampagneVerification.EstValidee;
		}

		public void ExecuteSaisirCommand() {
			Messenger.Default.Send<NMShowUC<CampagneVerification>>(
				new NMShowUC<CampagneVerification>(CodesUC.FormulaireSaisieCampagneVerification, this.SelectedCampagneVerification)
			);
		}
		#endregion

		#region ReportCommand
		public override bool CanExecuteReportCommand(string codeReport) {
			return this.SelectedCampagneVerification != null;
		}

		public override void ExecuteReportCommand(string codeReport) {
			switch (codeReport) {
				case CodesReport.VerificationEquipementExcel:
					Messenger.Default.Send(
							new NMActionFileDialog<string>(
							ResCommon.ExtensionExcel, 
							string.Format(ResCampagnesVerification.NomFichierRapportCampagneVerification, this.SelectedCampagneVerification.Date.ToString("yyyyMMdd")), 
							this.GenerateReportCampagneVerification
						)
					);
					break;

				default:
					break;
			}
		}

		private void GenerateReportCampagneVerification(string nomFichier) {
			if (nomFichier != null) {
				var gen = new ReportGenerator<ReportVerificationEquipement>(
						ServiceReportingAdapter.CampagneVerificationToReportVerificationEquipement(this.SelectedCampagneVerification),
						nomFichier
					);

				gen.SetTitle(string.Format(ResCampagnesVerification.TitreRapportVerificationEquipement, this.SelectedCampagneVerification.Date.ToShortDateString()));
				gen.SetSubTitle(string.Format(ResCampagnesVerification.SousTitreRapportVerificationEquipement, this.SelectedCampagneVerification.Responsable, this.SelectedCampagneVerification.NbEquipements));
				gen.GenerateExcelReport();

				this.ShowUserNotification(string.Format(ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		#endregion
	}
}
