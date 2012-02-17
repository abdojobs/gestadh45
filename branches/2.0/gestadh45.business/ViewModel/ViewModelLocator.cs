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

namespace gestadh45.business.ViewModel
{
	public class ViewModelLocator
	{
		private static MainViewModel _main;
		
		public static MainViewModel Main
		{
			get { return _main; }
		}

		public ViewModelLocator() {
			// initialisation du VM principal
			if (_main == null) {
				_main = new MainViewModel();
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
	}
}