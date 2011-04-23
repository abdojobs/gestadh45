using gestadh45.Model;

namespace gestadh45.service.VCards
{
	public class DonneesVCardAdaptateur
	{
		public static DonneesVCard CreerDonneesVCard(Inscription pInscription) {
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
