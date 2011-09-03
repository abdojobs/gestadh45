using gestadh45.Ihm.ViewModel.Consultation;
using gestadh45.Ihm.ViewModel.Formulaire;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelLocator
	{
		private static MainViewModel _main;
		private static ConsultationAdherentsUCViewModel mConsultationAdherentsVM;
		private static ConsultationGroupesUCViewModel mConsultationGroupesVM;
		private static ConsultationInfosClubUCViewModel mConsultationInfosClubVM;
		private static ConsultationInscriptionsUCViewModel mConsultationInscriptionsVM;
		private static ConsultationSaisonsUCViewModel mConsultationSaisonsVM;
		private static ConsultationVillesUCViewModel mConsultationVillesVM;
		private static ConsultationExtractionsUCViewModel mConsultationExtractionsVM;
		private static FormulaireAdherentUCViewModel mFormulaireAdherentVM;
		private static FormulaireGroupeUCViewModel mFormulaireGroupeVM;
		private static FormulaireInfosClubUCViewModel mFormulaireInfosClubVM;
		private static FormulaireInscriptionUCViewModel mFormulaireInscriptionVM;
		private static FormulaireSaisonUCViewModel mFormulaireSaisonVM;
		private static FormulaireVilleUCViewModel mFormulaireVilleVM;
		private static GraphsSaisonCouranteUCViewModel mGraphsSaisonCouranteVM;

		public ViewModelLocator() {
			CreateMain();
		}

		public static void Cleanup() {
			ClearMain();
		}

		public static void ClearConsultationAdherentsVMStatic() {
			if (mConsultationAdherentsVM != null) {
				mConsultationAdherentsVM.Cleanup();
				mConsultationAdherentsVM = null;
			}
		}

		public static void ClearConsultationGroupesVMStatic() {
			if (mConsultationGroupesVM != null) {
				mConsultationGroupesVM.Cleanup();
				mConsultationGroupesVM = null;
			}
		}

		public static void ClearConsultationInfosClubVMStatic() {
			if (mConsultationInfosClubVM != null) {
				mConsultationInfosClubVM.Cleanup();
				mConsultationInfosClubVM = null;
			}
		}

		public static void ClearConsultationInscriptionsVMStatic() {
			if (mConsultationInscriptionsVM != null) {
				mConsultationInscriptionsVM.Cleanup();
				mConsultationInscriptionsVM = null;
			}
		}

		public static void ClearConsultationSaisonsVMStatic() {
			if (mConsultationSaisonsVM != null) {
				mConsultationSaisonsVM.Cleanup();
				mConsultationSaisonsVM = null;
			}
		}

		public static void ClearConsultationVillesVMStatic() {
			if (mConsultationVillesVM != null) {
				mConsultationVillesVM.Cleanup();
				mConsultationVillesVM = null;
			}
		}

		public static void ClearConsultationExtractionsVMStatic() {
			if (mConsultationExtractionsVM != null) {
				mConsultationExtractionsVM.Cleanup();
				mConsultationExtractionsVM = null;
			}
		}

		public static void ClearFormulaireAdherentVMStatic() {
			if (mFormulaireAdherentVM != null) {
				mFormulaireAdherentVM.Cleanup();
				mFormulaireAdherentVM = null;
			}
		}

		public static void ClearFormulaireGroupeVMStatic() {
			if (mFormulaireGroupeVM != null) {
				mFormulaireGroupeVM.Cleanup();
				mFormulaireGroupeVM = null;
			}
		}

		public static void ClearFormulaireInfosClubVMStatic() {
			if (mFormulaireInfosClubVM != null) {
				mFormulaireInfosClubVM.Cleanup();
				mFormulaireInfosClubVM = null;
			}
		}

		public static void ClearFormulaireInscriptionVMStatic() {
			if (mFormulaireInscriptionVM != null) {
				mFormulaireInscriptionVM.Cleanup();
				mFormulaireInscriptionVM = null;
			}
		}

		public static void ClearFormulaireSaisonVMStatic() {
			if (mFormulaireSaisonVM != null) {
				mFormulaireSaisonVM.Cleanup();
				mFormulaireSaisonVM = null;
			}
		}

		public static void ClearFormulaireVilleVMStatic() {
			if (mFormulaireVilleVM != null) {
				mFormulaireVilleVM.Cleanup();
				mFormulaireVilleVM = null;
			}
		}

		public static void ClearGraphsSaisonCouranteVMStatic() {
			if (mGraphsSaisonCouranteVM != null) {
				mGraphsSaisonCouranteVM.Cleanup();
				mGraphsSaisonCouranteVM = null;
			}
		}

		public static void ClearMain() {
			_main.Cleanup();
			_main = null;
		}

		public static void CreateConsultationAdherentsVMStatic() {
			if (mConsultationAdherentsVM == null) {
				mConsultationAdherentsVM = new ConsultationAdherentsUCViewModel();
			}
		}

		public static void CreateConsultationGroupesVMStatic() {
			if (mConsultationGroupesVM == null) {
				mConsultationGroupesVM = new ConsultationGroupesUCViewModel();
			}
		}

		public static void CreateConsultationInfosClubVMStatic() {
			if (mConsultationInfosClubVM == null) {
				mConsultationInfosClubVM = new ConsultationInfosClubUCViewModel();
			}
		}

		public static void CreateConsultationInscriptionsVMStatic() {
			if (mConsultationInscriptionsVM == null) {
				mConsultationInscriptionsVM = new ConsultationInscriptionsUCViewModel();
			}
		}

		public static void CreateConsultationSaisonsVMStatic() {
			if (mConsultationSaisonsVM == null) {
				mConsultationSaisonsVM = new ConsultationSaisonsUCViewModel();
			}
		}

		public static void CreateConsultationVillesVMStatic() {
			if (mConsultationVillesVM == null) {
				mConsultationVillesVM = new ConsultationVillesUCViewModel();
			}
		}

		public static void CreateConsultationExtractionsVMStatic() {
			if (mConsultationExtractionsVM == null) {
				mConsultationExtractionsVM = new ConsultationExtractionsUCViewModel();
			}
		}

		public static void CreateFormulaireAdherentVMStatic() {
			if (mFormulaireAdherentVM == null) {
				mFormulaireAdherentVM = new FormulaireAdherentUCViewModel();
			}
		}

		public static void CreateFormulaireGroupeVMStatic() {
			if (mFormulaireGroupeVM == null) {
				mFormulaireGroupeVM = new FormulaireGroupeUCViewModel();
			}
		}

		public static void CreateFormulaireInfosClubVMStatic() {
			if (mFormulaireInfosClubVM == null) {
				mFormulaireInfosClubVM = new FormulaireInfosClubUCViewModel();
			}
		}

		public static void CreateFormulaireInscriptionVMStatic() {
			if (mFormulaireInscriptionVM == null) {
				mFormulaireInscriptionVM = new FormulaireInscriptionUCViewModel();
			}
		}

		public static void CreateFormulaireSaisonVMStatic() {
			if (mFormulaireSaisonVM == null) {
				mFormulaireSaisonVM = new FormulaireSaisonUCViewModel();
			}
		}

		public static void CreateFormulaireVilleVMStatic() {
			if (mFormulaireVilleVM == null) {
				mFormulaireVilleVM = new FormulaireVilleUCViewModel();
			}
		}

		public static void CreateGraphsSaisonCouranteVMStatic() {
			if (mGraphsSaisonCouranteVM == null) {
				mGraphsSaisonCouranteVM = new GraphsSaisonCouranteUCViewModel();
			}
		}

		public static void CreateMain() {
			if (_main == null) {
				_main = new MainViewModel();
			}
		}

		public static ConsultationAdherentsUCViewModel ConsultationAdherentsVMStatic {
			get {
				ClearConsultationAdherentsVMStatic();
				CreateConsultationAdherentsVMStatic();
				return mConsultationAdherentsVM;
			}
		}

		public static ConsultationGroupesUCViewModel ConsultationGroupesVMStatic {
			get {
				ClearConsultationGroupesVMStatic();
				CreateConsultationGroupesVMStatic();
				return mConsultationGroupesVM;
			}
		}

		public static ConsultationInfosClubUCViewModel ConsultationInfosClubVMStatic {
			get {
				ClearConsultationInfosClubVMStatic();
				CreateConsultationInfosClubVMStatic();
				return mConsultationInfosClubVM;
			}
		}

		public static ConsultationInscriptionsUCViewModel ConsultationInscriptionsVMStatic {
			get {
				ClearConsultationInscriptionsVMStatic();
				CreateConsultationInscriptionsVMStatic();
				return mConsultationInscriptionsVM;
			}
		}

		public static ConsultationSaisonsUCViewModel ConsultationSaisonsVMStatic {
			get {
				ClearConsultationSaisonsVMStatic();
				CreateConsultationSaisonsVMStatic();
				return mConsultationSaisonsVM;
			}
		}

		public static ConsultationVillesUCViewModel ConsultationVillesVMStatic {
			get {
				ClearConsultationVillesVMStatic();
				CreateConsultationVillesVMStatic();
				return mConsultationVillesVM;
			}
		}

		public static ConsultationExtractionsUCViewModel ConsultationExtractionsVMStatic {
			get {
				ClearConsultationExtractionsVMStatic();
				CreateConsultationExtractionsVMStatic();
				return mConsultationExtractionsVM;
			}
		}

		public static FormulaireAdherentUCViewModel FormulaireAdherentVMStatic {
			get {
				ClearFormulaireAdherentVMStatic();
				CreateFormulaireAdherentVMStatic();
				return mFormulaireAdherentVM;
			}
		}

		public static FormulaireGroupeUCViewModel FormulaireGroupeVMStatic {
			get {
				ClearFormulaireGroupeVMStatic();
				CreateFormulaireGroupeVMStatic();
				return mFormulaireGroupeVM;
			}
		}

		public static FormulaireInfosClubUCViewModel FormulaireInfosClubVMStatic {
			get {
				ClearFormulaireInfosClubVMStatic();
				CreateFormulaireInfosClubVMStatic();
				return mFormulaireInfosClubVM;
			}
		}

		public static FormulaireInscriptionUCViewModel FormulaireInscriptionVMStatic {
			get {
				ClearFormulaireInscriptionVMStatic();
				CreateFormulaireInscriptionVMStatic();
				return mFormulaireInscriptionVM;
			}
		}

		public static FormulaireSaisonUCViewModel FormulaireSaisonVMStatic {
			get {
				ClearFormulaireSaisonVMStatic();
				CreateFormulaireSaisonVMStatic();
				return mFormulaireSaisonVM;
			}
		}

		public static FormulaireVilleUCViewModel FormulaireVilleVMStatic {
			get {
				ClearFormulaireVilleVMStatic();
				CreateFormulaireVilleVMStatic();
				return mFormulaireVilleVM;
			}
		}

		public static GraphsSaisonCouranteUCViewModel GraphsSaisonCouranteVMStatic {
			get {
				ClearGraphsSaisonCouranteVMStatic();
				CreateGraphsSaisonCouranteVMStatic();
				return mGraphsSaisonCouranteVM;
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
	}
}
