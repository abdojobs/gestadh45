
namespace gestadh45.model
{
	public class Sexe : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le libellé court du sexe
		/// </summary>
		public string LibelleCourt { get; set; }

		/// <summary>
		/// Obtient/Définit le libellé long du sexe
		/// </summary>
		public string LibelleLong { get; set; }
		#endregion

		/// <summary>
		/// Renvoit le libellé court
		/// </summary>
		/// <returns>Libellé court</returns>
		public override string ToString() {
			return this.LibelleCourt;
		}

		public override bool EstValide() {
			return !string.IsNullOrWhiteSpace(this.LibelleCourt) && !string.IsNullOrWhiteSpace(this.LibelleLong);
		}
	}
}
