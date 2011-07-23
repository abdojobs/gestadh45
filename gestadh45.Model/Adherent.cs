using System;

namespace gestadh45.model
{
	public class Adherent : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le nom
		/// </summary>
		public string Nom { get; set; }

		/// <summary>
		/// Obtient/Définit le prénom
		/// </summary>
		public string Prenom { get; set; }

		/// <summary>
		/// Obtient/Définit la date de naissance
		/// </summary>
		public DateTime DateNaissance { get; set; }

		/// <summary>
		/// Obtient/Définit la date de création
		/// </summary>
		public DateTime DateCreation { get; set; }

		/// <summary>
		/// Obtient/Définit la date de modification
		/// </summary>
		public DateTime DateModification { get; set; }

		/// <summary>
		/// Obtient/Définit le sexe
		/// </summary>
		public Sexe Sexe { get; set; }

		/// <summary>
		/// Obtient/Définit le commentaire
		/// </summary>
		public string Commentaire { get; set; }

		/// <summary>
		/// Obtient/Définit l'adresse
		/// </summary>
		public Adresse Adresse { get; set; }

		/// <summary>
		/// Obtient/Définit le contact
		/// </summary>
		public Contact Contact { get; set; }

		/// <summary>
		/// Obtient l'âge de l'adhérent (calculé à partir de la date de naissance)
		/// </summary>
		public int Age {
			get { return this.CalculerAge(); }
		}
		#endregion

		public override string ToString() {
			return string.Format("{0} {1}", this.Nom, this.Prenom);
		}

		/// <summary>
		/// Calcule l'âge de l'adhérent à partir de sa date de naissance
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
	}
}
