using gestadh45.model;
using gestadh45.service.VCards;

namespace gestadh45.Ihm.ServiceAdaptateurs
{
	public static class ServiceVCardAdaptateur
	{
		public static DonneesVCard InscriptionToDonneesVCard(Inscription pInscription) {
			DonneesVCard lDonnees = new DonneesVCard()
			{
				Nom = pInscription.Adherent.Nom,
				Prenom = pInscription.Adherent.Prenom,

				Telephone1 = pInscription.Adherent.Contact.Telephone1.ToString(),
				Telephone2 = pInscription.Adherent.Contact.Telephone2.ToString(),
				Telephone3 = pInscription.Adherent.Contact.Telephone3.ToString(),

				Mail1 = pInscription.Adherent.Contact.Mail1.ToString(),
				Mail2 = pInscription.Adherent.Contact.Mail2.ToString(),
				Mail3 = pInscription.Adherent.Contact.Mail3.ToString(),

				LibelleGroupe = pInscription.Groupe.ToString()
			};

			return lDonnees;
		}
	}
}
