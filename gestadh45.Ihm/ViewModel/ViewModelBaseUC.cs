
namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelBaseUC : ViewModelBaseApplication
	{
		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de cet élément
		/// </summary>
		public string CodeUCOrigine { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'élément est dans sa propre fenêtre (True) ou dans l'écran principal (False)
		/// </summary>
		public bool ModeFenetre { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		public ViewModelBaseUC() {
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;
		}
	}
}
