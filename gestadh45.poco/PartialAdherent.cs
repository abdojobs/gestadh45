using System;

namespace gestadh45.poco
{
	public partial class Adherent
	{
		/// <summary>
		/// Obtient l(âge de l'adhérent (calculé à partir de sa date de naissance)
		/// </summary>
		public int Age {
			get { return this.CalculerAge(); }
		}

		public override string ToString() {
			return string.Format("{0} {1}", this.Nom, this.Prenom);
		}

		private int CalculerAge() {
			int num = DateTime.Now.Year - this.DateNaissance.Year;
			DateTime time = new DateTime(DateTime.Now.Year, this.DateNaissance.Month, this.DateNaissance.Day);
			if (time > DateTime.Now) {
				num--;
			}
			return num;
		}
	}
}
