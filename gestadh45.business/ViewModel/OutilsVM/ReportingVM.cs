using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.business.IhmObjects;
using gestadh45.dal;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class ReportingVM : VMConsultationBase
	{
		#region ListeReports
		private ICollection<ChoixItemIhm> _listeReports;

		/// <summary>
		/// Gets or sets the liste reports.
		/// </summary>
		/// <value>
		/// The liste reports.
		/// </value>
		public ICollection<ChoixItemIhm> ListeReports {
			get {
				return this._listeReports;
			}

			set {
				if (this._listeReports != value) {
					this._listeReports = value;
					this.RaisePropertyChanged(() => this.ListeReports);
				}
			}
		}
		#endregion

		#region TitreReport
		private string _titreReport;

		/// <summary>
		/// Gets or sets the titre report.
		/// </summary>
		/// <value>
		/// The titre report.
		/// </value>
		public string TitreReport {
			get {
				return this._titreReport;
			}

			set {
				if (this._titreReport != value) {
					this._titreReport = value;
					this.RaisePropertyChanged(() => this.TitreReport);
				}
			}
		}
		#endregion

		#region ReportDatas
		private ICollection<ITemplateReport> _reportDatas;

		/// <summary>
		/// Gets or sets the report datas
		/// </summary>
		/// <value>
		/// The report datas
		/// </value>
		public ICollection<ITemplateReport> ReportDatas {
			get {
				return this._reportDatas;
			}

			set {
				if (this._reportDatas != value) {
					this._reportDatas = value;
					this.RaisePropertyChanged(() => this.ReportDatas);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Inscription> _repoInscriptions;
		private Repository<TrancheAge> _repoTranchesAge;
		private Repository<InfosClub> _repoInfosClub;
		private Repository<Equipement> _repoEquipement;
		#endregion

		#region Data collections
		private ICollection<Inscription> _inscriptionsSaisonCourante;
		private ICollection<TrancheAge> _tranchesAge;
		private ICollection<Equipement> _equipements;
		#endregion

		private const string ResourceBaseName = "gestadh45.business.ViewModel.OutilsVM.ResReporting";

		#region Constructeur
		public ReportingVM() {
			this.PopulateListeReports();
			this.CreateChangeReportCommand();

			this.CreateRepositories();
			this.PopulateDataCollections();			
		}
		#endregion

		private void CreateRepositories() {
			this._repoInscriptions = new Repository<Inscription>(this._context);
			this._repoTranchesAge = new Repository<TrancheAge>(this._context);
			this._repoInfosClub = new Repository<InfosClub>(this._context);
			this._repoEquipement = new Repository<Equipement>(this._context);
		}

		private void PopulateDataCollections() {
			this._inscriptionsSaisonCourante = this._repoInscriptions.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante).ToList();
			this._tranchesAge = this._repoTranchesAge.GetAll().OrderBy(t => t.AgeInf).ToList();
			this._equipements = this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).OrderBy(e => e.Numero).ToList();
		}

		private void PopulateListeReports() {
			this.ListeReports = new List<ChoixItemIhm>();
			this.ListeReports.Add(new ChoixItemIhm(ResourceBaseName, CodesReport.InventaireCompletEquipementExcel));
			this.ListeReports.Add(new ChoixItemIhm(ResourceBaseName, CodesReport.InventaireSimpleEquipementExcel));
			this.ListeReports.Add(new ChoixItemIhm(ResourceBaseName, CodesReport.ListeAdherents));
			this.ListeReports.Add(new ChoixItemIhm(ResourceBaseName, CodesReport.RepartitionAdherentsAge));
		}

		#region ChangeReportCommand
		public ICommand ChangeReportCommand {
			get;
			set;
		}

		private void CreateChangeReportCommand() {
			this.ChangeReportCommand = new RelayCommand<ChoixItemIhm>(
				this.ExecuteChangeReportCommand,
				this.CanExecuteChangeReportCommand
			);
		}

		public bool CanExecuteChangeReportCommand(ChoixItemIhm choixReport) {
			return true;
		}

		public void ExecuteChangeReportCommand(ChoixItemIhm choixReport) {
			this.TitreReport = choixReport.ToString();
			
			// TODO alimenter les données
		}
		#endregion
	}
}
