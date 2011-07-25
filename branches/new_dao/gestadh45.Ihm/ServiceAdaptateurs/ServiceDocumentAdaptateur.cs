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
				Mail1Adherent = pInscription.Adherent.Contact.Mail1,
				Mail2Adherent = pInscription.Adherent.Contact.Mail2,
				Mail3Adherent = pInscription.Adherent.Contact.Mail3,
				MailClub = pInfosClub.Contact.Mail1,
				NomAdherent = pInscription.Adherent.Nom,
				NomClub = pInfosClub.Nom,
				NumeroClub = pInfosClub.Numero,
				PrenomAdherent = pInscription.Adherent.Prenom,
				Saison = pInscription.Groupe.Saison.ToShortString(),
				SiretClub = pInfosClub.Siret,
				SiteWebClub = pInfosClub.Contact.SiteWeb,
				Telephone1Adherent = pInscription.Adherent.Contact.Telephone1,
				Telephone2Adherent = pInscription.Adherent.Contact.Telephone2,
				Telephone3Adherent = pInscription.Adherent.Contact.Telephone3,
				TelephoneCLub = pInfosClub.Contact.Telephone1,
				VilleAdherent = pInscription.Adherent.Adresse.Ville.Libelle,
				VilleClub = pInfosClub.Adresse.Ville.Libelle
			};
			return lDonnees;
		}
	}
}
