using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportListeAdherents : ITemplateReport
	{
		[Display (ResourceType=typeof(ResReportListeAdherents), Name="HeaderNom")]
		public string Nom { get; set; }

		[Display(ResourceType = typeof(ResReportListeAdherents), Name = "HeaderPrenom")]
		public string Prenom { get; set; }

		[Display(ResourceType = typeof(ResReportListeAdherents), Name = "HeaderDateNaissance")]
		public string DateNaissance { get; set; }

		[Display(ResourceType = typeof(ResReportListeAdherents), Name = "HeaderTelephone")]
		public string Telephone { get; set; }

		[Display(ResourceType = typeof(ResReportListeAdherents), Name = "HeaderEmail")]
		public string Email { get; set; }

		[Display(ResourceType = typeof(ResReportListeAdherents), Name = "HeaderGroupe")]
		public string Groupe { get; set; }
	}
}
