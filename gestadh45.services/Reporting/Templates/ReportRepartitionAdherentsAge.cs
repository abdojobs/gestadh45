using System.ComponentModel.DataAnnotations;

namespace gestadh45.services.Reporting.Templates
{
	public class ReportRepartitionAdherentsAge : ITemplateReport
	{
		[Display(ResourceType = typeof(ResReports), Name = "HeaderLibelle")]
		public string Libelle { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderHommesResident")]
		public int NbHommesResident { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderFemmesResident")]
		public int NbFemmesResident { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotalresidents")]
		public int NbResidents {
			get { return this.NbHommesResident + this.NbFemmesResident; }
		}

		[Display(ResourceType = typeof(ResReports), Name = "HeaderHommesExterieur")]
		public int NbHommesExterieur { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderFemmesExterieur")]
		public int NbFemmesExterieur { get; set; }

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotalExterieurs")]
		public int NbExterieurs {
			get { return this.NbHommesExterieur + this.NbFemmesExterieur; }
		}

		[Display(ResourceType = typeof(ResReports), Name = "HeaderTotal")]
		public int NbTotal {
			get {
				return this.NbResidents + this.NbExterieurs;
			}
		}
	}
}
