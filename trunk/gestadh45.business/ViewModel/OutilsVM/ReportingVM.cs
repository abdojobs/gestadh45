using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.business.IhmObjects;
using gestadh45.business.ServicesAdapters;
using gestadh45.dal;
using gestadh45.services.Reporting;

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

		#region ReportDatas
		private ICollectionView _reportDatas;

		/// <summary>
		/// Gets or sets the report datas
		/// </summary>
		/// <value>
		/// The report datas
		/// </value>
		public ICollectionView ReportDatas {
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

		private const string ResourceBaseName = "gestadh45.business.ViewModel.OutilsVM.ResReporting";

		#region Constructeur
		public ReportingVM() {
			this.PopulateListeReports();
			this.CreateChangeReportCommand();

			this.CreateRepositories();	
		}
		#endregion

		private void CreateRepositories() {
			this._repoInscriptions = new Repository<Inscription>(this._context);
			this._repoTranchesAge = new Repository<TrancheAge>(this._context);
			this._repoInfosClub = new Repository<InfosClub>(this._context);
			this._repoEquipement = new Repository<Equipement>(this._context);
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
			CollectionViewSource src = new CollectionViewSource();

			switch (choixReport.Code) {
				case CodesReport.InventaireSimpleEquipementExcel:
					src.Source = ServiceReportingAdapter.EquipementsToReportInventaireEquipementSimple(
						this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).OrderBy(e => e.Numero).ToList()
					);
					break;

				case CodesReport.InventaireCompletEquipementExcel:
					src.Source = ServiceReportingAdapter.EquipementsToReportInventaireEquipementComplet(
						this._repoEquipement.GetAll().Where(e => !e.EstAuRebut).OrderBy(e => e.Numero).ToList()
					);
					break;

				case CodesReport.ListeAdherents:
					src.Source = ServiceReportingAdapter.InscriptionsToListeAdherents(
						this._repoInscriptions.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante).OrderBy(i => i.Adherent.ToString()).ToList()
					);
					break;

				case CodesReport.RepartitionAdherentsAge:
					src.Source = ServiceReportingAdapter.InscriptionsToReportRepartitionAdherentsAge(
						this._repoTranchesAge.GetAll().OrderBy(t => t.AgeInf).ToList(),
						this._repoInfosClub.GetFirst().Ville,
						this._repoInscriptions.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante).ToList()
					);
					break;

				default:
					src.Source = null;
					break;
			}


			this.ReportDatas = src.View;
		}
		#endregion
	}
}
