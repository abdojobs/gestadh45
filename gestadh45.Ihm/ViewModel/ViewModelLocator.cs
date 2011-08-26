using gestadh45.Ihm.ViewModel.Adherents;
using gestadh45.Ihm.ViewModel.Groupes;
using gestadh45.Ihm.ViewModel.InformationsCLub;
using gestadh45.Ihm.ViewModel.Inscriptions;
using gestadh45.Ihm.ViewModel.ParametresApplication;
using gestadh45.Ihm.ViewModel.Saisons;
using gestadh45.Ihm.ViewModel.Stats;
using gestadh45.Ihm.ViewModel.Villes;

namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelLocator
	{
		public static string DataSource = null;

		public ViewModelLocator() {
			CreateMain();
		}

		public static void Cleanup() {
			ClearMain();
		}

		#region MainView
		private static MainViewModel _main;

		public static void ClearMain() {
			_main.Cleanup();
			_main = null;
		}

		public static void CreateMain() {
			if (_main == null) {
				_main = new MainViewModel();
			}
		}

		public static MainViewModel MainStatic {
			get {
				if (_main == null) {
					CreateMain();
				}
				return _main;
			}
		}
		#endregion

		#region ConsultationInfosClub
		private static ConsultationInfosClubUCViewModel _consultationInfosClubVM;

		public static void ClearConsultationInfosClubVMStatic() {
			if (_consultationInfosClubVM != null) {
				_consultationInfosClubVM.Cleanup();
				_consultationInfosClubVM = null;
			}
		}

		public static void CreateConsultationInfosClubVMStatic() {
			if (_consultationInfosClubVM == null) {
				_consultationInfosClubVM = new ConsultationInfosClubUCViewModel();
			}
		}

		public static ConsultationInfosClubUCViewModel ConsultationInfosClubVMStatic {
			get {
				ClearConsultationInfosClubVMStatic();
				CreateConsultationInfosClubVMStatic();
				return _consultationInfosClubVM;
			}
		}
		#endregion

		#region FormulaireInfosClub
		private static FormulaireInfosClubUCViewModel _formulaireInfosClubVM;

		public static void ClearFormulaireInfosClubVMStatic() {
			if (_formulaireInfosClubVM != null) {
				_formulaireInfosClubVM.Cleanup();
				_formulaireInfosClubVM = null;
			}
		}

		public static void CreateFormulaireInfosClubVMStatic() {
			if (_formulaireInfosClubVM == null) {
				_formulaireInfosClubVM = new FormulaireInfosClubUCViewModel();
			}
		}

		public static FormulaireInfosClubUCViewModel FormulaireInfosClubVMStatic {
			get {
				ClearFormulaireInfosClubVMStatic();
				CreateFormulaireInfosClubVMStatic();
				return _formulaireInfosClubVM;
			}
		}
		#endregion

		#region ConsultationParamsApplication
		private static ConsultationParamsApplicationViewModel _consultationParamsApplicationVM;

		public static void ClearConsultationParamsApplicationVMStatic() {
			if (_consultationParamsApplicationVM != null) {
				_consultationParamsApplicationVM.Cleanup();
				_consultationParamsApplicationVM = null;
			}
		}

		public static void CreateConsultationParamsApplicationVMStatic() {
			if (_consultationParamsApplicationVM == null) {
				_consultationParamsApplicationVM = new ConsultationParamsApplicationViewModel();
			}
		}

		public static ConsultationParamsApplicationViewModel ConsultationParamsApplicationVMStatic {
			get {
				ClearConsultationParamsApplicationVMStatic();
				CreateConsultationParamsApplicationVMStatic();
				return _consultationParamsApplicationVM;
			}
		}
		#endregion

		#region FormulaireParamsApplication
		private static FormulaireParamsApplicationUCViewModel _formulaireParamsApplicationVM;

		public static void ClearFormulaireParamsApplicationVMStatic() {
			if (_formulaireParamsApplicationVM != null) {
				_formulaireParamsApplicationVM.Cleanup();
				_formulaireParamsApplicationVM = null;
			}
		}

		public static void CreateFormulaireParamsApplicationVMStatic() {
			if (_formulaireParamsApplicationVM == null) {
				_formulaireParamsApplicationVM = new FormulaireParamsApplicationUCViewModel();
			}
		}

		public static FormulaireParamsApplicationUCViewModel FormulaireParamsApplicationVMStatic {
			get {
				ClearFormulaireParamsApplicationVMStatic();
				CreateFormulaireParamsApplicationVMStatic();
				return _formulaireParamsApplicationVM;
			}
		}
		#endregion

		#region ConsultationSaisons
		private static ConsultationSaisonsUCViewModel _consultationSaisonsVM;

		public static void ClearConsultationSaisonsVMStatic() {
			if (_consultationSaisonsVM != null) {
				_consultationSaisonsVM.Cleanup();
				_consultationSaisonsVM = null;
			}
		}

		public static void CreateConsultationSaisonsVMStatic() {
			if (_consultationSaisonsVM == null) {
				_consultationSaisonsVM = new ConsultationSaisonsUCViewModel();
			}
		}

		public static ConsultationSaisonsUCViewModel ConsultationSaisonsVMStatic {
			get {
				ClearConsultationSaisonsVMStatic();
				CreateConsultationSaisonsVMStatic();
				return _consultationSaisonsVM;
			}
		}
		#endregion

		#region FormulaireSaison
		private static FormulaireSaisonUCViewModel _formulaireSaisonVM;

		public static void ClearFormulaireSaisonVMStatic() {
			if (_formulaireSaisonVM != null) {
				_formulaireSaisonVM.Cleanup();
				_formulaireSaisonVM = null;
			}
		}

		public static void CreateFormulaireSaisonVMStatic() {
			if (_formulaireSaisonVM == null) {
				_formulaireSaisonVM = new FormulaireSaisonUCViewModel();
			}
		}

		public static FormulaireSaisonUCViewModel FormulaireSaisonVMStatic {
			get {
				ClearFormulaireSaisonVMStatic();
				CreateFormulaireSaisonVMStatic();
				return _formulaireSaisonVM;
			}
		}
		#endregion

		#region ConsultationVilles
		private static ConsultationVillesUCViewModel _consultationVillesVM;

		public static void ClearConsultationVillesVMStatic() {
			if (_consultationVillesVM != null) {
				_consultationVillesVM.Cleanup();
				_consultationVillesVM = null;
			}
		}

		public static void CreateConsultationVillesVMStatic() {
			if (_consultationVillesVM == null) {
				_consultationVillesVM = new ConsultationVillesUCViewModel();
			}
		}

		public static ConsultationVillesUCViewModel ConsultationVillesVMStatic {
			get {
				ClearConsultationVillesVMStatic();
				CreateConsultationVillesVMStatic();
				return _consultationVillesVM;
			}
		}
		#endregion

		#region FormulaireVille
		private static FormulaireVilleUCViewModel _formulaireVilleVM;

		public static void ClearFormulaireVilleVMStatic() {
			if (_formulaireVilleVM != null) {
				_formulaireVilleVM.Cleanup();
				_formulaireVilleVM = null;
			}
		}

		public static void CreateFormulaireVilleVMStatic() {
			if (_formulaireVilleVM == null) {
				_formulaireVilleVM = new FormulaireVilleUCViewModel();
			}
		}

		public static FormulaireVilleUCViewModel FormulaireVilleVMStatic {
			get {
				ClearFormulaireVilleVMStatic();
				CreateFormulaireVilleVMStatic();
				return _formulaireVilleVM;
			}
		}
		#endregion

		#region ConsultationAdherents
		private static ConsultationAdherentsUCViewModel _consultationAdherentsVM;

		public static void ClearConsultationAdherentsVMStatic() {
			if (_consultationAdherentsVM != null) {
				_consultationAdherentsVM.Cleanup();
				_consultationAdherentsVM = null;
			}
		}

		public static void CreateConsultationAdherentsVMStatic() {
			if (_consultationAdherentsVM == null) {
				_consultationAdherentsVM = new ConsultationAdherentsUCViewModel();
			}
		}

		public static ConsultationAdherentsUCViewModel ConsultationAdherentsVMStatic {
			get {
				ClearConsultationAdherentsVMStatic();
				CreateConsultationAdherentsVMStatic();
				return _consultationAdherentsVM;
			}
		}
		#endregion

		#region FormulaireAdherent
		private static FormulaireAdherentUCViewModel _formulaireAdherentVM;

		public static void ClearFormulaireAdherentVMStatic() {
			if (_formulaireAdherentVM != null) {
				_formulaireAdherentVM.Cleanup();
				_formulaireAdherentVM = null;
			}
		}

		public static void CreateFormulaireAdherentVMStatic() {
			if (_formulaireAdherentVM == null) {
				_formulaireAdherentVM = new FormulaireAdherentUCViewModel();
			}
		}

		public static FormulaireAdherentUCViewModel FormulaireAdherentVMStatic {
			get {
				ClearFormulaireAdherentVMStatic();
				CreateFormulaireAdherentVMStatic();
				return _formulaireAdherentVM;
			}
		}
		#endregion

		#region ConsultationsInscriptions
		private static ConsultationInscriptionsUCViewModel _consultationInscriptionsVM;

		public static void ClearConsultationInscriptionsVMStatic() {
			if (_consultationInscriptionsVM != null) {
				_consultationInscriptionsVM.Cleanup();
				_consultationInscriptionsVM = null;
			}
		}

		public static void CreateConsultationInscriptionsVMStatic() {
			if (_consultationInscriptionsVM == null) {
				_consultationInscriptionsVM = new ConsultationInscriptionsUCViewModel();
			}
		}

		public static ConsultationInscriptionsUCViewModel ConsultationInscriptionsVMStatic {
			get {
				ClearConsultationInscriptionsVMStatic();
				CreateConsultationInscriptionsVMStatic();
				return _consultationInscriptionsVM;
			}
		}
		#endregion

		#region FormulaireInscription
		private static FormulaireInscriptionUCViewModel _formulaireInscriptionVM;

		public static void ClearFormulaireInscriptionVMStatic() {
			if (_formulaireInscriptionVM != null) {
				_formulaireInscriptionVM.Cleanup();
				_formulaireInscriptionVM = null;
			}
		}

		public static void CreateFormulaireInscriptionVMStatic() {
			if (_formulaireInscriptionVM == null) {
				_formulaireInscriptionVM = new FormulaireInscriptionUCViewModel();
			}
		}

		public static FormulaireInscriptionUCViewModel FormulaireInscriptionVMStatic {
			get {
				ClearFormulaireInscriptionVMStatic();
				CreateFormulaireInscriptionVMStatic();
				return _formulaireInscriptionVM;
			}
		}
		#endregion


		#region ConsultationGroupes
		private static ConsultationGroupesUCViewModel _consultationGroupesVM;

		public static void ClearConsultationGroupesVMStatic() {
			if (_consultationGroupesVM != null) {
				_consultationGroupesVM.Cleanup();
				_consultationGroupesVM = null;
			}
		}

		public static void CreateConsultationGroupesVMStatic() {
			if (_consultationGroupesVM == null) {
				_consultationGroupesVM = new ConsultationGroupesUCViewModel();
			}
		}

		public static ConsultationGroupesUCViewModel ConsultationGroupesVMStatic {
			get {
				ClearConsultationGroupesVMStatic();
				CreateConsultationGroupesVMStatic();
				return _consultationGroupesVM;
			}
		}
		#endregion

		#region FormulaireGroupe
		private static FormulaireGroupeUCViewModel _formulaireGroupeVM;

		public static void ClearFormulaireGroupeVMStatic() {
			if (_formulaireGroupeVM != null) {
				_formulaireGroupeVM.Cleanup();
				_formulaireGroupeVM = null;
			}
		}

		public static void CreateFormulaireGroupeVMStatic() {
			if (_formulaireGroupeVM == null) {
				_formulaireGroupeVM = new FormulaireGroupeUCViewModel();
			}
		}

		public static FormulaireGroupeUCViewModel FormulaireGroupeVMStatic {
			get {
				ClearFormulaireGroupeVMStatic();
				CreateFormulaireGroupeVMStatic();
				return _formulaireGroupeVM;
			}
		}
		#endregion

		#region GraphsSaisonCourante
		private static GraphsSaisonCouranteUCViewModel _graphsSaisonCouranteVM;

		public static void ClearGraphsSaisonCouranteVMStatic() {
			if (_graphsSaisonCouranteVM != null) {
				_graphsSaisonCouranteVM.Cleanup();
				_graphsSaisonCouranteVM = null;
			}
		}

		public static void CreateGraphsSaisonCouranteVMStatic() {
			if (_graphsSaisonCouranteVM == null) {
				_graphsSaisonCouranteVM = new GraphsSaisonCouranteUCViewModel();
			}
		}

		public static GraphsSaisonCouranteUCViewModel GraphsSaisonCouranteVMStatic {
			get {
				ClearGraphsSaisonCouranteVMStatic();
				CreateGraphsSaisonCouranteVMStatic();
				return _graphsSaisonCouranteVM;
			}
		}
		#endregion
	}
}
