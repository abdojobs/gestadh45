
namespace gestadh45.model
{
	public class JourSemaine : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le numéro du jour (lundi=1, ..., dimanche=7)
		/// </summary>
		public int Numero { get; set; }

		/// <summary>
		/// Obtient/Définit le libellé du jour
		/// </summary>
		public string Libelle { get; set; }
		#endregion

		/// <summary>
		/// Renvoit le libellé du jour
		/// </summary>
		/// <returns>Libellé du jour</returns>
		public override string ToString() {
			return this.Libelle;
		}
	}
}
