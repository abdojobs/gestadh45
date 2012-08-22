
using System.ComponentModel.DataAnnotations;
namespace gestadh45.services.Reporting.Templates
{
	public class ReportInventaireEquipementSimple : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderNumero")]
		public string Numero { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderCategorie")]
		public string Categorie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderMarque")]
		public string Marque { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderModele")]
		public string Modele { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderDateAchat")]
		public string DateAchat { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderLocalisation")]
		public string Localisation { get; set; }
	}
}
