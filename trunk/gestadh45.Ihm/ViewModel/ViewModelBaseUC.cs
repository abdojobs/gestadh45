using GalaSoft.MvvmLight;

namespace gestadh45.Ihm.ViewModel
{
	public abstract class ViewModelBaseUC : ViewModelBase
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'UC est dans sa propre fenêtre (True) ou dans l'écran principal (False)
		/// </summary>
		public bool ModeFenetre { get; set; }
	}
}
