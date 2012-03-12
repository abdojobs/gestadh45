
namespace gestadh45.dal
{
	public partial class Inscription
	{
		/// <summary>
		/// Obtient la description de l'inscription
		/// </summary>
		/// <returns>LibelleGroupe - Adherent</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.Groupe.Libelle, this.Adherent);
		}

		/// <summary>
		/// Obtient le libellé de l'inscription (encapsulation de ToString())
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}
	}
}
