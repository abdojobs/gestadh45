/*
  In App.xaml:
  <Application.Resources>
	  <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:gestadh45.business"
								   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=[StaticVMProperty]}"
*/

using gestadh45.business.ViewModel.AdherentsVM;
using gestadh45.business.ViewModel.CampagnesVerificationVM;
using gestadh45.business.ViewModel.CategoriesVM;
using gestadh45.business.ViewModel.DureesDeVieVM;
using gestadh45.business.ViewModel.EquipementsVM;
using gestadh45.business.ViewModel.GroupesVM;
using gestadh45.business.ViewModel.InfosClubVM;
using gestadh45.business.ViewModel.InscriptionsVM;
using gestadh45.business.ViewModel.LocalisationVM;
using gestadh45.business.ViewModel.MainScreenVM;
using gestadh45.business.ViewModel.MarquesVM;
using gestadh45.business.ViewModel.ModeleVM;
using gestadh45.business.ViewModel.OutilsVM;
using gestadh45.business.ViewModel.SaisonsVM;
using gestadh45.business.ViewModel.TranchesAgeVM;
using gestadh45.business.ViewModel.VillesVM;

namespace gestadh45.business.ViewModel
{
	public class ViewModelLocator
	{
		private static MainViewModel _mainVM;
		
		public static MainViewModel MainVM
		{
			get { return _mainVM; }
		}

		public ViewModelLocator() {
			// initialisation du VM principal
			if (_mainVM == null) {
				_mainVM = new MainViewModel();
			}
		}

		#region InfosClubVM
		public static ConsultationInfosClubVM ConsultationInfosClubVM {
			get { return new ConsultationInfosClubVM(); }
		}

		public static FormulaireInfosClubVM FormulaireInfosClubVM {
			get { return new FormulaireInfosClubVM(); }
		}
		#endregion

		#region VillesVM
		public static ConsultationVillesVM ConsultationVillesVM {
			get { return new ConsultationVillesVM(); }
		}

		public static FormulaireVilleVM FormulaireVilleVM {
			get { return new FormulaireVilleVM(); }
		}
		#endregion

		#region SaisonsVM
		public static ConsultationSaisonsVM ConsultationSaisonsVM {
			get { return new ConsultationSaisonsVM(); }
		}

		public static FormulaireSaisonVM FormulaireSaisonVM {
			get { return new FormulaireSaisonVM(); }
		}
		#endregion

		#region AdherentsVM
		public static ConsultationAdherentsVM ConsultationAdherentsVM {
			get { return new ConsultationAdherentsVM(); }
		}

		public static FormulaireAdherentVM FormulaireAdherentVM {
			get { return new FormulaireAdherentVM(); }
		}
		#endregion

		#region InscriptionsVM
		public static ConsultationInscriptionsVM ConsultationInscriptionsVM {
			get { return new ConsultationInscriptionsVM(); }
		}

		public static FormulaireInscriptionVM FormulaireInscriptionVM {
			get { return new FormulaireInscriptionVM(); }
		}
		#endregion

		#region GroupesVM
		public static ConsultationGroupesVM ConsultationGroupesVM {
			get { return new ConsultationGroupesVM(); }
		}

		public static FormulaireGroupeVM FormulaireGroupeVM {
			get { return new FormulaireGroupeVM(); }
		}
		#endregion

		#region TranchesAgeVM
		public static ConsultationTranchesAgeVM ConsultationTranchesAgeVM {
			get { return new ConsultationTranchesAgeVM(); }
		}

		public static FormulaireTrancheAgeVM FormulaireTrancheAgeVM {
			get { return new FormulaireTrancheAgeVM(); }
		}
		#endregion

		#region MarquesVM
		public static ConsultationMarquesVM ConsultationMarquesVM {
			get {
				return new ConsultationMarquesVM();
			}
		}

		public static FormulaireMarqueVM FormulaireMarqueVM {
			get {
				return new FormulaireMarqueVM();
			}
		}
		#endregion

		#region EquipementsVM
		public static ConsultationEquipementsVM ConsultationEquipementsVM {
			get {
				return new ConsultationEquipementsVM();
			}
		}

		public static FormulaireEquipementVM FormulaireEquipementVM {
			get {
				return new FormulaireEquipementVM();
			}
		}
		#endregion

		#region CategoriesVM
		public static ConsultationCategoriesVM ConsultationCategoriesVM {
			get {
				return new ConsultationCategoriesVM();
			}
		}

		public static FormulaireCategorieVM FormulaireCategorieVM {
			get {
				return new FormulaireCategorieVM();
			}
		}
		#endregion

		#region DureesDeVieVM
		public static ConsultationDureesDeVieVM ConsultationDureesDeVieVM {
			get {
				return new ConsultationDureesDeVieVM();
			}
		}

		public static FormulaireDureeDeVieVM FormulaireDureeDeVieVM {
			get {
				return new FormulaireDureeDeVieVM();
			}
		}
		#endregion

		#region MainScreenVM
		public static MainScreenCheckVM MainScreenCheckVM {
			get {
				return new MainScreenCheckVM();
			}
		}

		public static FormulaireInitialisationDatabaseVM FormulaireInitialisationDatabaseVM {
			get {
				return new FormulaireInitialisationDatabaseVM();
			}
		}
		#endregion

		#region ModelesVM
		public static ConsultationModelesVM ConsultationModelesVM {
			get { return new ConsultationModelesVM(); }
		}

		public static FormulaireModeleVM FormulaireModeleVM {
			get { return new FormulaireModeleVM(); }
		}
		#endregion

		#region LocalisationsVM
		public static ConsultationLocalisationsVM ConsultationLocalisationsVM {
			get { return new ConsultationLocalisationsVM(); }
		}

		public static FormulaireLocalisationVM FormulaireLocalisationVM {
			get { return new FormulaireLocalisationVM(); }
		}
		#endregion

		#region CampagnesVerificationVM
		public static ConsultationCampagnesVerificationVM ConsultationCampagnesVerificationVM {
			get { return new ConsultationCampagnesVerificationVM(); }
		}

		public static FormulaireCreationCampagneVerificationVM FormulaireCreationCampagneVerificationVM {
			get { return new FormulaireCreationCampagneVerificationVM(); }
		}
		#endregion

		#region OutilsVM
		public static ReportingVM ReportingVM {
			get { return new ReportingVM(); }
		}

		public static GraphiquesVM GraphiquesVM {
			get { return new GraphiquesVM(); }
		}

		public static NettoyageCNILVM NettoyageCNILVM {
			get { return new NettoyageCNILVM(); }
		}
		#endregion
	}
}