using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.business.ServicesAdapters
{
	public static class ServiceReportingAdapter
	{
		public static ReportInventaireEquipementSimple EquipementToReportInventaireEquipementSimple(Equipement equipement) {
			return new ReportInventaireEquipementSimple()
			{
				Numero = equipement.Numero,
				Categorie = equipement.Modele.Categorie.Libelle,
				Modele = equipement.Modele.LibelleCourt,
				Marque = equipement.Modele.Marque.Libelle,
				DateAchat = (equipement.DateAchat.HasValue ? equipement.DateAchat.Value.ToShortDateString() : equipement.DateCreation.ToShortDateString()),
				Localisation = equipement.Localisation.Libelle
			};
		}
		
		public static ICollection<ReportInventaireEquipementSimple> EquipementsToReportInventaireEquipementSimple(ICollection<Equipement> equipements) {
			var result = new List<ReportInventaireEquipementSimple>();

			foreach (var equip in equipements) {
				result.Add(EquipementToReportInventaireEquipementSimple(equip));
			}

			return result;
		}

		public static ICollection<ReportInventaireEquipementComplet> EquipementsToReportInventaireEquipementComplet(ICollection<Equipement> equipements) {
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

				// on se base sur la dernière vérification validée
				var lastVerif = equip.Verifications.OrderByDescending(v => v.CampagneVerification.Date).FirstOrDefault(v => v.CampagneVerification.EstValidee);

				item.DateDerniereVerification = (lastVerif != null) ? lastVerif.CampagneVerification.Date.ToShortDateString() : string.Empty;
				item.StatutDerniereVerification = (lastVerif != null) ? lastVerif.StatutVerification.Libelle : string.Empty;

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
					StatutVerification = verif.StatutVerification.Libelle,
					RaisonStatutVerification = verif.Commentaire
				};

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportListeAdherents> InscriptionsToListeAdherents(ICollection<Inscription> inscriptions) {
			var result = new List<ReportListeAdherents>();

			foreach (var ins in inscriptions) {
				var item = new ReportListeAdherents()
				{
					Nom = ins.Adherent.Nom,
					Prenom = ins.Adherent.Prenom,
					DateNaissance = ins.Adherent.DateCreation.ToShortDateString(),
					Telephone = ins.Adherent.Telephone1,
					Email = ins.Adherent.Mail1,
					Groupe = ins.Groupe.Libelle
				};

				result.Add(item);
			}

			return result;
		}

		public static ICollection<ReportListeAdherents> GroupeToReportListeAdherents(Groupe groupe) {
			return InscriptionsToListeAdherents(groupe.Inscriptions);
		}

		public static ICollection<ReportRepartitionAdherentsAge> InscriptionsToReportRepartitionAdherentsAge(ICollection<TrancheAge> tranchesAge, Ville villeResident, ICollection<Inscription> inscriptions) {
			var result = new List<ReportRepartitionAdherentsAge>();

			foreach (var tranche in tranchesAge) {
				var item = new ReportRepartitionAdherentsAge()
				{
					Libelle = tranche.ToString()
				};

				item.NbHommesResident = inscriptions.Count(i =>
					i.Adherent.Sexe.LibelleCourt.Equals("M")
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbFemmesResident = inscriptions.Count(i =>
					i.Adherent.Sexe.LibelleCourt.Equals("F")
					&& i.Adherent.Ville.ID == villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbHommesExterieur = inscriptions.Count(i =>
					i.Adherent.Sexe.LibelleCourt.Equals("M")
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				item.NbFemmesExterieur = inscriptions.Count(i =>
					i.Adherent.Sexe.LibelleCourt.Equals("F")
					&& i.Adherent.Ville.ID != villeResident.ID
					&& i.Adherent.Age >= tranche.AgeInf
					&& i.Adherent.Age <= tranche.AgeSup
				);

				result.Add(item);
			}

			return result;
		}
	}
}
