using gestadh45.model;
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
				AdresseAdherent = pInscription.Adherent.Adresse.Libelle,
				AdresseClub = pInfosClub.Adresse.Libelle,
				CodePostalAdherent = pInscription.Adherent.Adresse.Ville.CodePostal,
				CodePostalClub = pInfosClub.Adresse.Ville.CodePostal,
				CotisationInscription = pInscription.Cotisation.ToString(),
				DateNaissanceAdherent = pInscription.Adherent.DateNaissance.ToShortDateString(),
				GroupeInscription = pInscription.Groupe.ToString(),
				Mail1Adherent = pInscription.Adherent.Contact.Mail1.ToString(),
				Mail2Adherent = pInscription.Adherent.Contact.Mail2.ToString(),
				Mail3Adherent = pInscription.Adherent.Contact.Mail3.ToString(),
				MailClub = pInfosClub.Contact.Mail1.ToString(),
				NomAdherent = pInscription.Adherent.Nom,
				NomClub = pInfosClub.Nom,
				NumeroClub = pInfosClub.Numero,
				PrenomAdherent = pInscription.Adherent.Prenom,
				Saison = pInscription.Groupe.Saison.ToShortString(),
				SiretClub = pInfosClub.Siret,
				SiteWebClub = pInfosClub.Contact.SiteWeb,
				Telephone1Adherent = pInscription.Adherent.Contact.Telephone1.ToString(),
				Telephone2Adherent = pInscription.Adherent.Contact.Telephone2.ToString(),
				Telephone3Adherent = pInscription.Adherent.Contact.Telephone3.ToString(),
				TelephoneCLub = pInfosClub.Contact.Telephone1.ToString(),
				VilleAdherent = pInscription.Adherent.Adresse.Ville.Libelle,
				VilleClub = pInfosClub.Adresse.Ville.Libelle
			};
			return lDonnees;
		}
	}
}
