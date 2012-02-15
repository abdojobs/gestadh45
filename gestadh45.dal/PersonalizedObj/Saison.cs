
namespace gestadh45.dal
{
	public partial class Saison
	{
		/// <summary>
		/// Obtient une description courte de la saison
		/// </summary>
		/// <returns>AnneeDebut - AnneeFin</returns>
		public string ToShortString() {
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}

		/// <summary>
		/// Obtient une description complète de la saison
		/// </summary>
		/// <returns>AnneeDebut - AnneeFin [(courante)]</returns>
		public override string ToString() {
			if (this.EstSaisonCourante) {
				return string.Format("{0} - {1} (courante)", this.AnneeDebut, this.AnneeFin);
			}
			return string.Format("{0} - {1}", this.AnneeDebut, this.AnneeFin);
		}
	}
}
