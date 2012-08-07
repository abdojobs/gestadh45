using System.Collections.Generic;
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
	}
}
