using gestadh45.dal;
using gestadh45.service.Documents;

namespace gestadh45.Ihm.ServiceAdaptateurs
{
	public static class ServiceDocumentAdaptateur
	{
		/// <summary>
		/// Créé un objet document
		/// </summary>
		/// <param name="pInfosClub">Infos du club</param>
		/// <param name="pInscription">Inscription</param>
		/// <returns>Données du document</returns>
		public static DonneesDocument InscriptionToDonneesDocument(InfosClub pInfosClub, Inscription pInscription) {
			DonneesDocument lDonnees = new DonneesDocument()
			{
				AdresseAdherent = pInscription.Adherent.Adresse,
				AdresseClub = pInfosClub.Adresse,
				CodePostalAdherent = pInscription.Adherent.Ville.CodePostal,
				CodePostalClub = pInfosClub.Ville.CodePostal,
				CotisationInscription = pInscription.Cotisation.ToString(),
				DateNaissanceAdherent = pInscription.Adherent.DateNaissance.ToShortDateString(),
				GroupeInscription = pInscription.Groupe.ToString(),
				Mail1Adherent = pInscription.Adherent.Mail1,
				Mail2Adherent = pInscription.Adherent.Mail2,
				Mail3Adherent = pInscription.Adherent.Mail3,
				MailClub = pInfosClub.Mail,
				NomAdherent = pInscription.Adherent.Nom,
				NomClub = pInfosClub.Nom,
				NumeroClub = pInfosClub.Numero,
				PrenomAdherent = pInscription.Adherent.Prenom,
				Saison = pInscription.Groupe.Saison.ToShortString(),
				SiretClub = pInfosClub.Siret,
				SiteWebClub = pInfosClub.SiteWeb,
				Telephone1Adherent = pInscription.Adherent.Telephone1,
				Telephone2Adherent = pInscription.Adherent.Telephone2,
				Telephone3Adherent = pInscription.Adherent.Telephone3,
				TelephoneCLub = pInfosClub.Telephone,
				VilleAdherent = pInscription.Adherent.Ville.Libelle,
				VilleClub = pInfosClub.Ville.Libelle
			};
			return lDonnees;
		}
	}
}
