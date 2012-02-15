
namespace gestadh45.dal
{
	public partial class Sexe
	{
		/// <summary>
		/// Obtient une description du sexe
		/// </summary>
		/// <returns>LibelleLong</returns>
		public string ToLongString() {
			return this.LibelleLong;
		}

		/// <summary>
		/// Obtient une description courte du sexe
		/// </summary>
		/// <returns>LibelleCourt</returns>
		public override string ToString() {
			return this.LibelleCourt;
		}
	}
}
