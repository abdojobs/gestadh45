using gestadh45.dao;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.ViewModel.Adherents;
using gestadh45.Ihm.ViewModel.Groupes;
using gestadh45.Ihm.ViewModel.InfosClubs;
using gestadh45.Ihm.ViewModel.Inscriptions;
using gestadh45.Ihm.ViewModel.Saisons;
using gestadh45.Ihm.ViewModel.Stats;
using gestadh45.Ihm.ViewModel.Tools;
using gestadh45.Ihm.ViewModel.TranchesAge;
using gestadh45.Ihm.ViewModel.Villes;

namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelLocator
	{
		public static string DataSource = null;

		public ViewModelLocator() {
			// initialisation du VM principal
			if (_main == null) {
				_main = new MainViewModel();
			}
		}

		public static void Cleanup() {
			_main.Cleanup();
			_main = null;
		}

		#region DAO
		public static IAdherentDao DaoAdherent { get; set; }
		public static IGroupeDao DaoGroupe { get; set; }
		public static IInfosClubDao DaoInfosClub { get; set; }
		public static IInscriptionDao DaoInscription { get; set; }
		public static IJourSemaineDao DaoJourSemaine { get; set; }
		public static ISaisonDao DaoSaison { get; set; }
		public static ISexeDao DaoSexe { get; set; }
		public static IStatutInscriptionDao DaoStatutInscription { get; set; }
		public static ITrancheAgeDao DaoTrancheAge { get; set; }
		public static IVilleDao DaoVille { get; set; }
		public static IAppUserDao DaoAppUser { get; set; }
		#endregion

		#region session utilisateur
		public static SessionIhm CurrentSession { get; set; }
		#endregion

		#region MainView
		private static MainViewModel _main;

		public static MainViewModel MainStatic {
			get {
				if (_main == null) {
					_main = new MainViewModel();
				}
				return _main;
			}
		}
		#endregion

		#region ConsultationInfosClub
		private static ConsultationInfosClubUCViewModel _consultationInfosClubVM;

		public static ConsultationInfosClubUCViewModel ConsultationInfosClubVMStatic {
			get {
				if (_consultationInfosClubVM != null) {
					_consultationInfosClubVM.Cleanup();
					_consultationInfosClubVM = null;
				}

				_consultationInfosClubVM = new ConsultationInfosClubUCViewModel();

				return _consultationInfosClubVM;
			}
		}
		#endregion

		#region FormulaireInfosClub
		private static FormulaireInfosClubUCViewModel _formulaireInfosClubVM;

		public static FormulaireInfosClubUCViewModel FormulaireInfosClubVMStatic {
			get {
				if (_formulaireInfosClubVM != null) {
					_formulaireInfosClubVM.Cleanup();
					_formulaireInfosClubVM = null;
				}

				_formulaireInfosClubVM = new FormulaireInfosClubUCViewModel();

				return _formulaireInfosClubVM;
			}
		}
		#endregion

		#region ConsultationSaisons
		private static ConsultationSaisonsUCViewModel _consultationSaisonsVM;

		public static ConsultationSaisonsUCViewModel ConsultationSaisonsVMStatic {
			get {
				if (_consultationSaisonsVM != null) {
					_consultationSaisonsVM.Cleanup();
					_consultationSaisonsVM = null;
				}

				_consultationSaisonsVM = new ConsultationSaisonsUCViewModel();

				return _consultationSaisonsVM;
			}
		}
		#endregion

		#region FormulaireSaison
		private static FormulaireSaisonUCViewModel _formulaireSaisonVM;

		public static FormulaireSaisonUCViewModel FormulaireSaisonVMStatic {
			get {
				if (_formulaireSaisonVM != null) {
					_formulaireSaisonVM.Cleanup();
					_formulaireSaisonVM = null;
				}

				_formulaireSaisonVM = new FormulaireSaisonUCViewModel();

				return _formulaireSaisonVM;
			}
		}
		#endregion

		#region ConsultationVilles
		private static ConsultationVillesUCViewModel _consultationVillesVM;

		public static ConsultationVillesUCViewModel ConsultationVillesVMStatic {
			get {
				if (_consultationVillesVM != null) {
					_consultationVillesVM.Cleanup();
					_consultationVillesVM = null;
				}

				_consultationVillesVM = new ConsultationVillesUCViewModel();

				return _consultationVillesVM;
			}
		}
		#endregion

		#region FormulaireVille
		private static FormulaireVilleUCViewModel _formulaireVilleVM;

		public static FormulaireVilleUCViewModel FormulaireVilleVMStatic {
			get {
				if (_formulaireVilleVM != null) {
					_formulaireVilleVM.Cleanup();
					_formulaireVilleVM = null;
				}

				_formulaireVilleVM = new FormulaireVilleUCViewModel();

				return _formulaireVilleVM;
			}
		}
		#endregion

		#region ConsultationAdherents
		private static ConsultationAdherentsUCViewModel _consultationAdherentsVM;

		public static ConsultationAdherentsUCViewModel ConsultationAdherentsVMStatic {
			get {
				if (_consultationAdherentsVM != null) {
					_consultationAdherentsVM.Cleanup();
					_consultationAdherentsVM = null;
				}

				_consultationAdherentsVM = new ConsultationAdherentsUCViewModel();

				return _consultationAdherentsVM;
			}
		}
		#endregion

		#region FormulaireAdherent
		private static FormulaireAdherentUCViewModel _formulaireAdherentVM;

		public static FormulaireAdherentUCViewModel FormulaireAdherentVMStatic {
			get {
				if (_formulaireAdherentVM != null) {
					_formulaireAdherentVM.Cleanup();
					_formulaireAdherentVM = null;
				}

				_formulaireAdherentVM = new FormulaireAdherentUCViewModel();

				return _formulaireAdherentVM;
			}
		}
		#endregion

		#region ConsultationsInscriptions
		private static ConsultationInscriptionsUCViewModel _consultationInscriptionsVM;

		public static ConsultationInscriptionsUCViewModel ConsultationInscriptionsVMStatic {
			get {
				if (_consultationInscriptionsVM != null) {
					_consultationInscriptionsVM.Cleanup();
					_consultationInscriptionsVM = null;
				}

				_consultationInscriptionsVM = new ConsultationInscriptionsUCViewModel();

				return _consultationInscriptionsVM;
			}
		}
		#endregion

		#region FormulaireInscription
		private static FormulaireInscriptionUCViewModel _formulaireInscriptionVM;

		public static FormulaireInscriptionUCViewModel FormulaireInscriptionVMStatic {
			get {
				if (_formulaireInscriptionVM != null) {
					_formulaireInscriptionVM.Cleanup();
					_formulaireInscriptionVM = null;
				}

				_formulaireInscriptionVM = new FormulaireInscriptionUCViewModel();

				return _formulaireInscriptionVM;
			}
		}
		#endregion

		#region ConsultationGroupes
		private static ConsultationGroupesUCViewModel _consultationGroupesVM;

		public static ConsultationGroupesUCViewModel ConsultationGroupesVMStatic {
			get {
				if (_consultationGroupesVM != null) {
					_consultationGroupesVM.Cleanup();
					_consultationGroupesVM = null;
				}

				_consultationGroupesVM = new ConsultationGroupesUCViewModel();

				return _consultationGroupesVM;
			}
		}
		#endregion

		#region FormulaireGroupe
		private static FormulaireGroupeUCViewModel _formulaireGroupeVM;

		public static FormulaireGroupeUCViewModel FormulaireGroupeVMStatic {
			get {
				if (_formulaireGroupeVM != null) {
					_formulaireGroupeVM.Cleanup();
					_formulaireGroupeVM = null;
				}

				_formulaireGroupeVM = new FormulaireGroupeUCViewModel();

				return _formulaireGroupeVM;
			}
		}
		#endregion

		#region GraphsSaisonCourante
		private static GraphsSaisonCouranteUCViewModel _graphsSaisonCouranteVM;

		public static GraphsSaisonCouranteUCViewModel GraphsSaisonCouranteVMStatic {
			get {
				if (_graphsSaisonCouranteVM != null) {
					_graphsSaisonCouranteVM.Cleanup();
					_graphsSaisonCouranteVM = null;
				}

				_graphsSaisonCouranteVM = new GraphsSaisonCouranteUCViewModel();

				return _graphsSaisonCouranteVM;
			}
		}
		#endregion

		#region StatsSaisonCourante
		private static StatsSaisonCouranteUCViewModel _statsSaisonCouranteVM;

		public static StatsSaisonCouranteUCViewModel StatsSaisonCouranteVMStatic
		{
			get
			{
				if (_statsSaisonCouranteVM != null)
				{
					_statsSaisonCouranteVM.Cleanup();
					_statsSaisonCouranteVM = null;
				}

				_statsSaisonCouranteVM = new StatsSaisonCouranteUCViewModel();

				return _statsSaisonCouranteVM;
			}
		}
		#endregion

		#region FicheEffectif
		private static FicheEffectifUCViewModel _ficheEffectifVM;

		public static FicheEffectifUCViewModel FicheEffectifVMStatic {
			get {
				if (_ficheEffectifVM != null)
				{
					_ficheEffectifVM.Cleanup();
					_ficheEffectifVM = null;
				}

				_ficheEffectifVM = new FicheEffectifUCViewModel();

				return _ficheEffectifVM;
			}
		}
		#endregion

		#region RepartitionEffectif
		private static RepartitionEffectifUCViewModel _repartitionEffectifVM;

		public static RepartitionEffectifUCViewModel RepartitionEffectifVMStatic {
			get {
				if (_repartitionEffectifVM != null) {
					_repartitionEffectifVM.Cleanup();
					_repartitionEffectifVM = null;
				}

				_repartitionEffectifVM = new RepartitionEffectifUCViewModel();

				return _repartitionEffectifVM;
			}
		}
		#endregion

		#region ConsultationTranchesAge
		private static ConsultationTranchesAgeUCViewModel _consultationTranchesAgeVM;

		public static ConsultationTranchesAgeUCViewModel ConsultationTranchesAgeVMStatic {
			get {
				if (_consultationTranchesAgeVM != null) {
					_consultationTranchesAgeVM.Cleanup();
					_consultationTranchesAgeVM = null;
				}

				_consultationTranchesAgeVM = new ConsultationTranchesAgeUCViewModel();

				return _consultationTranchesAgeVM;
			}
		}
		#endregion

		#region FormulaireTrancheAge
		private static FormulaireTrancheAgeUCViewModel _formulaireTranchesAgeVM;

		public static FormulaireTrancheAgeUCViewModel FormulaireTrancheAgeVMStatic {
			get {
				if (_formulaireTranchesAgeVM != null) {
					_formulaireTranchesAgeVM.Cleanup();
					_formulaireTranchesAgeVM = null;
				}

				_formulaireTranchesAgeVM = new FormulaireTrancheAgeUCViewModel();

				return _formulaireTranchesAgeVM;
			}
		}
		#endregion

		#region ExportUC
		private static ExportUCViewModel _ExportVM;

		public static ExportUCViewModel ExportVMStatic {
			get {
				if (_ExportVM != null) {
					_ExportVM.Cleanup();
					_ExportVM = null;
				}

				_ExportVM = new ExportUCViewModel();

				return _ExportVM;
			}
		}
		#endregion
	}
}
