using System;

namespace gestadh45.dal
{
	public partial class Adherent : ICloneable
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
		private int CalculerAge() {
			int num = DateTime.Now.Year - this.DateNaissance.Year;
			DateTime time = new DateTime(DateTime.Now.Year, this.DateNaissance.Month, this.DateNaissance.Day);
			if (time > DateTime.Now) {
				num--;
			}
			return num;
		}

		#region ICloneable Membres

		public object Clone() {
			return new Adherent()
			{
				Nom = string.Copy(this.Nom),
				Prenom = string.Copy(this.Prenom),
				DateNaissance = this.DateNaissance,
				ID_Sexe = this.ID_Sexe,

				Adresse = string.Copy(this.Adresse),
				ID_Ville = this.ID_Ville,

				Telephone1 = string.Copy(this.Telephone1 ?? string.Empty),
				Telephone2 = string.Copy(this.Telephone2 ?? string.Empty),
				Telephone3 = string.Copy(this.Telephone3 ?? string.Empty),

				Mail1 = string.Copy(this.Mail1 ?? string.Empty),
				Mail2 = string.Copy(this.Mail2 ?? string.Empty),
				Mail3 = string.Copy(this.Mail3 ?? string.Empty),

				Commentaire = string.Copy(this.Commentaire ?? string.Empty),

				DateCreation = DateTime.Now,
				DateModification = DateTime.Now
			};
		}

		#endregion
	}
}
