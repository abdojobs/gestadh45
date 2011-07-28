
namespace gestadh45.model
{
	/// <summary>
	/// Classe de base de laquelle héritent toutes les autres classes
	/// </summary>
	public abstract class BaseClass
	{
		/// <summary>
		/// Obtient/Définit l'identifiant
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Obtient une chaîne représentant l'objet
		/// </summary>
		public string DisplayMemberPath {
			get { return this.ToString(); }
		}

		public abstract bool EstValide();
	}
}
