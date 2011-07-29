using System;

namespace gestadh45.model
{
	public class Groupe : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le libellé
		/// </summary>
		public string Libelle { get; set; }

		/// <summary>
		/// Obtient/Définit le jour
		/// </summary>
		public JourSemaine JourSemaine { get; set; }

		/// <summary>
		/// Obtient/Définit le nombre de places
		/// </summary>
		public int NbPlaces { get; set; }

		/// <summary>
		/// Obtient/Définit le commentaire
		/// </summary>
		public string Commentaire { get; set; }

		/// <summary>
		/// Obtient/Définit la saison
		/// </summary>
		public Saison Saison { get; set; }

		/// <summary>
		/// Obtient/Définit l'heure de début
		/// </summary>
		public DateTime HeureDebut { get; set; }

		/// <summary>
		/// Obtient/Définit l'heure de fin
		/// </summary>
		public DateTime HeureFin { get; set; }
		#endregion

		/// <summary>
		/// Renvoit le jour et le créneau du groupe
		/// </summary>
		/// <returns>Jour Heure début - Heure fin</returns>
		public override string ToString() {
			return string.Format("{0} {1} - {2}", this.JourSemaine.ToString(), this.HeureDebut.ToShortTimeString(), this.HeureFin.ToShortTimeString());
		}
	}
}
