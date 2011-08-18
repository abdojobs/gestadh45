using System.Text.RegularExpressions;

namespace gestadh45.model.bo
{
	public class NumeroTelephone
	{
		/// <summary>
		/// Obtient/Définit lenuméro de téléphone
		/// </summary>
		public string Numero { get; set; }
		
		/// <summary>
		/// Obtient un booléen indiquant si le numéro de téléphone est valide
		/// </summary>
		public bool EstValide {
			get { return this.VerifierValidite(); }
		}

		/// <summary>
		/// Retourne le numéro de téléphones
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return this.Numero;
		}

		/// <summary>
		/// Vérifie que le numéro de téléphone respecte les règles suivantes : <br />
		/// <ul>
		/// <li>Numéro non null</li>
		/// <li>10 caractères</li>
		/// <li>commence par un 0</li>
		/// <li>composé uniquement de chiffres</li>
		/// </ul>
		/// </summary>
		/// <returns>True si le numéro respecte les règles de validité, False sinon</returns>
		private bool VerifierValidite() {
			return !string.IsNullOrWhiteSpace(this.Numero) && Regex.IsMatch(this.Numero, "^0[0-9]{9}$");
		}
	}
}
