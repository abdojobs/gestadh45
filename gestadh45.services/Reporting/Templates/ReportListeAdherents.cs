using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportListeAdherents : ITemplateReport
	{
		[Display (ResourceType=typeof(ResReports), Name="HeaderNom")]
		public string Nom { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderPrenom")]
		public string Prenom { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderDateNaissance")]
		public string DateNaissance { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTelephone")]
		public string Telephone { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderEmail")]
		public string Email { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderGroupe")]
		public string Groupe { get; set; }
	}
}
