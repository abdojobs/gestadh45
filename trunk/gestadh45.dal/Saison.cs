
namespace gestadh45.dal
{
	public partial class Saison
	{
		public string ToShortString() {
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}

		public override string ToString() {
			if (this.EstSaisonCourante) {
				return string.Format("{0} - {1} (courante)", this.AnneeDebut, this.AnneeFin);
			}
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}
	}
}
