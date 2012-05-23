
namespace gestadh45.services.Documents
{
	public class DonneesDocument
	{
		// Infos d'entête
		public string NomClub { get; set; }
		public string Saison { get; set; }
		public string AdresseClub { get; set; }
		public string CodePostalClub { get; set; }
		public string VilleClub { get; set; }
		public string TelephoneCLub { get; set; }
		public string MailClub { get; set; }
		public string SiteWebClub { get; set; }
		public string NumeroClub { get; set; }
		public string SiretClub { get; set; }

		// infos adherent
		public string NomAdherent { get; set; }
		public string PrenomAdherent { get; set; }
		public string DateNaissanceAdherent { get; set; }

		public string AdresseAdherent { get; set; }
		public string CodePostalAdherent { get; set; }
		public string VilleAdherent { get; set; }

		public string Telephone1Adherent { get; set; }
		public string Telephone2Adherent { get; set; }
		public string Telephone3Adherent { get; set; }
		public string Mail1Adherent { get; set; }
		public string Mail2Adherent { get; set; }
		public string Mail3Adherent { get; set; }

		// infos inscription
		public string CotisationInscription { get; set; }
		public string GroupeInscription { get; set; }

		/// <summary>
		/// Constructeur permettant d'initialiser toutes les données à string.Empty
		/// </summary>
		public DonneesDocument() {
			// Infos d'entête
			this.NomClub = string.Empty;
			this.Saison = string.Empty;
			this.AdresseClub = string.Empty;
			this.CodePostalClub = string.Empty;
			this.VilleClub = string.Empty;
			this.TelephoneCLub = string.Empty;
			this.MailClub = string.Empty;
			this.SiteWebClub = string.Empty;
			this.NumeroClub = string.Empty;
			this.SiretClub = string.Empty;
		}
	}
}
