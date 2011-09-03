using System.Windows.Controls;
using gestadh45.Ihm.ViewModel.Formulaire;

namespace gestadh45.Ihm.Tools
{
	public class ChampTexteObligatoireValidateur : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
			string lContenu = value as string;

			if (string.IsNullOrWhiteSpace(lContenu)) {
				return new ValidationResult(false, ResErreurs.ChampObligatoire);
			}
			else {
				return new ValidationResult(true, null);
			}
		}
	}
}
