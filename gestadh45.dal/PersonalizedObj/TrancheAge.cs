
namespace gestadh45.dal
{
	public partial class TrancheAge
	{
		/// <summary>
		/// Obtient une description de la tranche d'âge
		/// </summary>
		/// <returns>AgeInf - AgeSup ans</returns>
		public override string ToString() {
			return string.Format("{0} - {1} ans", this.AgeInf, this.AgeSup);
		}
	}
}
