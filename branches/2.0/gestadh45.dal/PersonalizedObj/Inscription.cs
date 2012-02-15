
namespace gestadh45.dal
{
	public partial class Inscription
	{
		/// <summary>
		/// Obtient la description de l'inscription
		/// </summary>
		/// <returns>Adherent - LibelleGroupe</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.Adherent, this.Groupe.Libelle);
		}
	}
}
