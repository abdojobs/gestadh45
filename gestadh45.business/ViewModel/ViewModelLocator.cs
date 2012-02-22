/*
  In App.xaml:
  <Application.Resources>
	  <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:gestadh45.business"
								   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=[StaticVMProperty]}"
*/

using GalaSoft.MvvmLight;
using gestadh45.business.ViewModel.InfosClubVM;
using gestadh45.business.ViewModel.VillesVM;
using gestadh45.business.ViewModel.SaisonsVM;

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
		#endregion
	}
}