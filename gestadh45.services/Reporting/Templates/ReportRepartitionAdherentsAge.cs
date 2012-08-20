using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportRepartitionAdherentsAge : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderLibelle")]
		public string Libelle { get; set; }

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderHommesResident")]
		public int NbHommesResident { get; set; }

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderFemmesResident")]
		public int NbFemmesResident { get; set; }

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderTotalresidents")]
		public int NbResidents {
			get { return this.NbHommesResident + this.NbFemmesResident; }
		}

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderHommesExterieur")]
		public int NbHommesExterieur { get; set; }

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderFemmesExterieur")]
		public int NbFemmesExterieur { get; set; }

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderTotalExterieurs")]
		public int NbExterieurs {
			get { return this.NbHommesExterieur + this.NbFemmesExterieur; }
		}

		[Display(ResourceType = typeof(ResReportRepartitionAdherentsAge), Name = "HeaderTotal")]
		public int NbTotal {
			get {
				return this.NbResidents + this.NbExterieurs;
			}
		}
	}
}
