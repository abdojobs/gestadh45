
namespace gestadh45.Model
{
	public partial class Saison
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant si la saison est la saison courante
		/// </summary>
		public bool EstSaisonCouranteBool {
			get { return this.EstSaisonCourante == 1; }
			set { this.EstSaisonCourante =  (value) ? 1 : 0; }
		}
		
		public string ToShortString() {
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}

		public override string ToString() {
			if (this.EstSaisonCourante == 1L) {
				return string.Format("{0} - {1} (courante)", this.AnneeDebut, this.AnneeFin);
			}
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}
	}
}
