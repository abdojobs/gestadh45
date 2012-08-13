using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ServicesAdapters
{
	public static class ServiceReportingAdapter
	{
		public static ICollection<ReportInventaireEquipementSimple> EquipementToReportInventaireEquipementSimple(ICollection<Equipement> equipements) {
			var result = new List<ReportInventaireEquipementSimple>();

			foreach (var equip in equipements) {
				var item = new ReportInventaireEquipementSimple()
				{
					Numero = equip.Numero,
					Categorie = equip.Modele.Categorie.Libelle,
					Modele = equip.Modele.LibelleCourt,
					Marque = equip.Modele.Marque.Libelle,
					DateAchat = (equip.DateAchat.HasValue ? equip.DateAchat.Value.ToShortDateString() : equip.DateCreation.ToShortDateString()),
					Localisation = equip.Localisation.Libelle
				};

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportInventaireEquipementComplet> EquipementToReportInventaireEquipementComplet(ICollection<Equipement> equipements) {
			var result = new List<ReportInventaireEquipementComplet>();

			foreach (var equip in equipements) {
				var item = new ReportInventaireEquipementComplet()
				{
					Numero = equip.Numero,
					Categorie = equip.Modele.Categorie.Libelle,
					Modele = equip.Modele.LibelleCourt,
					Marque = equip.Modele.Marque.Libelle,
					DateAchat = (equip.DateAchat.HasValue ? equip.DateAchat.Value.ToShortDateString() : equip.DateCreation.ToShortDateString()),
					Localisation = equip.Localisation.Libelle
				};

				var lastVerif = equip.Verifications.OrderBy(v => v.CampagneVerification.Date).Last();

				item.DateDerniereVerification = lastVerif.CampagneVerification.Date.ToShortDateString();
				item.StatutDerniereVerification = lastVerif.StatutVerification.Libelle;

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportVerificationEquipement> CampagneVerificationToReportVerificationEquipement(CampagneVerification campagne) {
			var result = new List<ReportVerificationEquipement>();

			foreach (var verif in campagne.Verifications) {
				var item = new ReportVerificationEquipement()
				{
					Categorie = verif.Equipement.Modele.Categorie.Libelle,
					Marque = verif.Equipement.Modele.Marque.Libelle,
					Modele = verif.Equipement.Modele.LibelleCourt,
					Numero = verif.Equipement.Numero,
					StatutVerification = verif.StatutVerification.Libelle
				};

				result.Add(item);
			}

			return result;
		}
	}
}
