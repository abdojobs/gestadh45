
using System.ComponentModel.DataAnnotations;
namespace gestadh45.services.Reporting.Templates
{
	public class ReportInventaireEquipementSimple : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderNumero")]
		public string Numero { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderCategorie")]
		public string Categorie { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderMarque")]
		public string Marque { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderModele")]
		public string Modele { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderDateAchat")]
		public string DateAchat { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementSimple), Name = "HeaderLocalisation")]
		public string Localisation { get; set; }
	}
}
