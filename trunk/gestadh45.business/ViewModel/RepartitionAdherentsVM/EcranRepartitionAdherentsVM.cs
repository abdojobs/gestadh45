using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.IhmObjects;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.dal;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ViewModel.RepartitionAdherentsVM
{
	public class EcranRepartitionAdherentsVM : VMConsultationBase
	{
		#region TranchesEffectif
		private IList<ReportRepartitionAdherentsAge> _tranchesEffectif;

		/// <summary>
		/// Obtient/Définit la liste des tranches d'effectif
		/// </summary>
		public IList<ReportRepartitionAdherentsAge> TranchesEffectif {
			get {
				return this._tranchesEffectif;
			}
			set {
				if (this._tranchesEffectif != value) {
					this._tranchesEffectif = value;
					this.RaisePropertyChanged(() => this.TranchesEffectif);
				}
			}
		}
		#endregion

		#region champs privés
		private ICollection<Inscription> _inscriptionsSaisonCourante;
		private Ville _villeResident;
		#endregion

		#region Repositories
		private Repository<Inscription> _daoInscriptions;
		private Repository<InfosClub> _daoInfosClub;
		private Repository<TrancheAge> _daoTranchesAge;
		#endregion

		#region Constructeur
		public EcranRepartitionAdherentsVM() {
			this.CreateDao();

			this._inscriptionsSaisonCourante =this._daoInscriptions.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante).ToList();
			this._villeResident = this._daoInfosClub.GetFirst().Ville;

			this.InitialisationTranchesEffectif();
		}
		#endregion

		private void CreateDao() {
			this._daoInscriptions = new Repository<Inscription>(this._context);
			this._daoInfosClub = new Repository<InfosClub>(this._context);
			this._daoTranchesAge = new Repository<TrancheAge>(this._context);
		}

		private void InitialisationTranchesEffectif() {
			this.TranchesEffectif = ServiceReportingAdapter.InscriptionsToReportRepartitionAdherentsAge(
				this._daoTranchesAge.GetAll(), 
				this._villeResident, 
				this._inscriptionsSaisonCourante
			).ToList();
		}

		#region ReportCommand
		public override bool CanExecuteReportCommand(string codeReport) {
			return this._daoTranchesAge.GetAll().Count > 0;
		}

		public override void ExecuteReportCommand(string codeReport) {
			switch (codeReport) {
				case CodesReport.RepartitionAdherentsAge:
					Messenger.Default.Send(
						new NMActionFileDialog<string>(
							ResCommon.ExtensionExcel,
							ResRepartitionAdherents.NomFichierRapportRepartitionAdherentsAge,
							this.GenerateReportRepartitionAdherentsAge
						)
					);
					break;

				default:
					break;
			}
		}

		private void GenerateReportRepartitionAdherentsAge(string nomFichier) {
			if (nomFichier != null) {
				var gen = new ReportGenerator<ReportRepartitionAdherentsAge>(
						this.TranchesEffectif,
						nomFichier
					);

				gen.SetTitle(ResRepartitionAdherents.TitreRapportRepartitionAdherentsAge);
				gen.SetSubTitle(string.Format(ResRepartitionAdherents.SousTitreRepartitionAdherentsAge, this._inscriptionsSaisonCourante.Count));
				gen.GenerateExcelReport();

				this.ShowUserNotification(string.Format(ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		#endregion
	}
}
