using gestadh45.Model;
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

				Telephone1 = pInscription.Adherent.Contact.Telephone1,
				Telephone2 = pInscription.Adherent.Contact.Telephone2,
				Telephone3 = pInscription.Adherent.Contact.Telephone3,

				Mail1 = pInscription.Adherent.Contact.Mail1,
				Mail2 = pInscription.Adherent.Contact.Mail2,
				Mail3 = pInscription.Adherent.Contact.Mail3,

				LibelleGroupe = pInscription.Groupe.ToString()
			};

			return lDonnees;
		}
	}
}
