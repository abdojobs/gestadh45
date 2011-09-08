
namespace gestadh45.Ihm.ViewModel.Common
{
	public class ConsultationExtractionsUCViewModel : ViewModelBaseConsultation
	{
		private string mResultatExtraction;

		/// <summary>
		/// Obtient/Définit le résultat de l'extraction à afficher
		/// </summary>
		public string ResultatExtraction {
			get {
				return this.mResultatExtraction;
			}
			set {
				if (this.mResultatExtraction != value) {
					this.mResultatExtraction = value;
					this.RaisePropertyChanged("ResultatExtraction");
				}
			}
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		public ConsultationExtractionsUCViewModel() {
		}
	}
}
