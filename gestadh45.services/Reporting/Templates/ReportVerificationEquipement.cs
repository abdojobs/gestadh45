using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportVerificationEquipement : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderNumero")]
		public string Numero { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderCategorie")]
		public string Categorie { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderMarque")]
		public string Marque { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderModele")]
		public string Modele { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderStatutVerification")]
		public string StatutVerification { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderRaisonStatutVerification")]
		public string RaisonStatutVerification { get; set; }
	}
}
