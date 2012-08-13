
namespace gestadh45.services.Reporting.Templates
{
	public class ReportInventaireEquipementComplet : ITemplateReport
	{
		public string Numero { get; set; }
		public string Categorie { get; set; }
		public string Marque { get; set; }
		public string Modele { get; set; }
		public string DateAchat { get; set; }
		public string Localisation { get; set; }
		public string DateDerniereVerification { get; set; }
		public string StatutDerniereVerification { get; set; }
	}
}
