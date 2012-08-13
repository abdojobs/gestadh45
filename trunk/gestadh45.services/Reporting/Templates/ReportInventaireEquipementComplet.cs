
using System.ComponentModel.DataAnnotations;
namespace gestadh45.services.Reporting.Templates
{
	public class ReportInventaireEquipementComplet : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderNumero")]
		public string Numero { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderCategorie")]
		public string Categorie { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderMarque")]
		public string Marque { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderModele")]
		public string Modele { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderDateAchat")]
		public string DateAchat { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderLocalisation")]
		public string Localisation { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderDateDerniereVerification")]
		public string DateDerniereVerification { get; set; }

		[Display(ResourceType = typeof(ResReportInventaireEquipementComplet), Name = "HeaderStatutDerniereVerification")]
		public string StatutDerniereVerification { get; set; }
	}
}
