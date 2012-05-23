
namespace gestadh45.dal
{
	public partial class StatutInscription
	{
		/// <summary>
		/// Obtient une description du statut
		/// </summary>
		/// <returns>Libelle</returns>
		public override string ToString() {
			return string.Format("{0}", this.Libelle);
		}
	}
}
