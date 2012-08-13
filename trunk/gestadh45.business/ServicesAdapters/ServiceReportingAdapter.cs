﻿using System.Collections.Generic;
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
	}
}