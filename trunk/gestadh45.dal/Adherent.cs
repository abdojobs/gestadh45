using System;

namespace gestadh45.dal
{
	public partial class Adherent
	{
		/// <summary>
		/// Gets the age (donnée calculée)
		/// </summary>
		public int Age {
			get { return this.CalculerAge(); }
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
