
namespace gestadh45.services.Reporting.Templates
{
	public class ReportInventaireEquipementSimple : ITemplateReport
	{
		public string Numero { get; set; }
		public string Categorie { get; set; }
		public string Marque { get; set; }
		public string Modele { get; set; }
		public string DateAchat { get; set; }
		public string Localisation { get; set; }
	}
}
