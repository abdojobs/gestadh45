using System.Text.RegularExpressions;

namespace gestadh45.model.bo
{
	public class AdresseEmail
	{
		private string _adresse;
		
		/// <summary>
		/// Obtient/Définit l'adresse email
		/// </summary>
		public string Adresse {
			get { return this._adresse; }
			set {
				this._adresse = value.ToLowerInvariant();
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si l'adresse est valide
		/// </summary>
		public bool EstValide {
			get { return this.VerifierValidite(); }
		}

		/// <summary>
		/// Obtient le domaine de l'adresse email (après le @)
		/// </summary>
		public string Domaine {
			get { return this._adresse.Split('@')[1]; }
		}

		/// <summary>
		/// Retourne l'adresse email
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return this._adresse;
		}

		/// <summary>
		/// Vérifie que l'email est valide syntaxiquement<br />
		/// </summary>
		/// <returns>True si l'adresse est valide, False sinon</returns>
		private bool VerifierValidite() {
			return !string.IsNullOrWhiteSpace(this.Adresse) && Regex.IsMatch(this.Adresse,
			  @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
			  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"); 
		}
	}
}
