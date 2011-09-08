using System;
using System.Text;

namespace gestadh45.dal
{
	public partial class Adherent
	{
		private const string SeparateurAdressesMail = ",";
		
		/// <summary>
		/// Gets the age (donnée calculée)
		/// </summary>
		public int Age {
			get { return this.CalculerAge(); }
		}

		/// <summary>
		/// Obtient la liste des adresses emails sous forme d'une chaîne, avec un séparateur standard
		/// </summary>
		public string ChaineMails {
			get {
				StringBuilder lSb = new StringBuilder();

				if (!string.IsNullOrWhiteSpace(this.Mail1.ToString())) {
					lSb.Append(this.Mail1 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail2.ToString())) {
					lSb.Append(this.Mail2 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail3.ToString())) {
					lSb.Append(this.Mail3 + SeparateurAdressesMail);
				}

				return lSb.ToString();
			}
		}

		public override string ToString() {
			return string.Format("{0} {1}", this.Nom, this.Prenom);
		}

		private int CalculerAge()
		{
			int num = DateTime.Now.Year - this.DateNaissance.Year;
			DateTime time = new DateTime(DateTime.Now.Year, this.DateNaissance.Month, this.DateNaissance.Day);
			if (time > DateTime.Now) {
				num--;
			}
			return num;
		}
	}
}
