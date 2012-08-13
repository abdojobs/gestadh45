using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportVerificationEquipement : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderNumero")]
		public string Numero { get; set; }

		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderCategorie")]
		public string Categorie { get; set; }

		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderMarque")]
		public string Marque { get; set; }

		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderModele")]
		public string Modele { get; set; }

		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderStatutVerification")]
		public string StatutVerification { get; set; }

		[Display(ResourceType = typeof(ResReportVerificationEquipement), Name = "HeaderRaisonStatutVerification")]
		public string RaisonStatutVerification { get; set; }
	}
}
