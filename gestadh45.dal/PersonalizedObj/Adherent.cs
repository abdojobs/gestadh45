using System;

namespace gestadh45.dal
{
	public partial class Adherent
	{
		/// <summary>
		/// Obtient l'âge de l'adhérent (calculé à partir de sa date de naissance)
		/// </summary>
		public int Age {
			get { return this.CalculerAge(); }
		}

		/// <summary>
		/// Affiche la description de l'adhérent
		/// </summary>
		/// <returns>Nom Prenom</returns>
		public override string ToString() {
			return string.Format("{0} {1}", this.Nom, this.Prenom);
		}

		/// <summary>
		/// Calcule l'age de l'adherent à partir de sa date de naissance
		/// </summary>
		/// <returns>Age de l'adhérent</returns>
		[Obsolete("Utiliser la méthode du gptoolkit à la place")]
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
