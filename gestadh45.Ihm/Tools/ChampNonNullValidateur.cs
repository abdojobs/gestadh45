using System.Windows.Controls;
using gestadh45.Ihm.ViewModel.Formulaire;

namespace gestadh45.Ihm.Tools
{
	public class ChampNonNullValidateur : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
			if (value == null) {
				return new ValidationResult(false, ResErreurs.ChampObligatoire);
			}
			else {
				return new ValidationResult(true, null);
			}
		}
	}
}
