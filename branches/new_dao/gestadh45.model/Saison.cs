﻿
namespace gestadh45.model
{
	public class Saison : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit un booléen indiquant si il s'agit de la saison courante
		/// </summary>
		public bool EstSaisonCourante { get; set; }

		/// <summary>
		/// Obtient/Définit l'année de début
		/// </summary>
		public int AnneeDebut { get; set; }

		/// <summary>
		/// Obtient/Définit l'année de fin
		/// </summary>
		public int AnneeFin { get; set; }
		#endregion

		/// <summary>
		/// Renvoit année début - année fin
		/// </summary>
		/// <returns>Année début - Année fin</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}

		/// <summary>
		/// Renvoit année début - année fin plus l'information si la saison est la saison courante
		/// </summary>
		/// <returns>Année début - Année fin [(courante)]</returns>
		public string ToLongString() {
			if (this.EstSaisonCourante) {
				return string.Format("{0} (courante)", this.ToString());
			}
			else {
				return this.ToString();
			}
		}
	}
}
