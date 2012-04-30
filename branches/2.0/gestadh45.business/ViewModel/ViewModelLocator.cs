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
using gestadh45.business.ViewModel.GroupesVM;
using gestadh45.business.ViewModel.InfosClubVM;
using gestadh45.business.ViewModel.InscriptionsVM;
using gestadh45.business.ViewModel.SaisonsVM;
using gestadh45.business.ViewModel.Statistiques;
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

		#region StatistiquesVM
		public static EcranStatistiquesVM EcranStatistiquesVM {
			get {
				return new EcranStatistiquesVM();
			}
		}
		#endregion
	}
}