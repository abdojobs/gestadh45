using System;

namespace gestadh45.service.Database
{
	public class DonneesBase
	{
		public string LibelleVilleClub { get; set; }
		public string CodePostalVilleClub { get; set; }
		public string LibelleAdresseClub { get; set; }
		public string NomClub { get; set; }
		public int AnneeDebutSaison { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		public DonneesBase() {
			this.LibelleVilleClub = ResDatabase.Defaut_LibelleVilleClub;
			this.CodePostalVilleClub = ResDatabase.Defaut_CodePostalVilleClub;
			this.LibelleAdresseClub = ResDatabase.Defaut_LibelleAdresseClub;
			this.NomClub = ResDatabase.Defaut_NomClub;
			this.AnneeDebutSaison = DateTime.Now.Year;
		}
	}
}
