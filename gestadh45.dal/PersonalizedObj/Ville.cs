
namespace gestadh45.dal
{
	public partial class Ville
	{
		/// <summary>
		/// Obtient une description de la ville
		/// </summary>
		/// <returns>CodePostal - Libelle</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.CodePostal, this.Libelle);
		}
	}
}
