
namespace gestadh45.dal
{
	public partial class JourSemaine
	{
		/// <summary>
		/// Obtient la description du jour de la semaine
		/// </summary>
		/// <returns>LibelleJour</returns>
		public override string ToString() {
			return string.Format("{0}", this.Libelle);
		}
	}
}
