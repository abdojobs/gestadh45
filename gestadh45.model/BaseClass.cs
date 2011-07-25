
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

		public int ValuePath {
			get { return this.Id; }
		}

		public string DisplayMemberPath {
			get { return this.ToString(); }
		}
	}
}
