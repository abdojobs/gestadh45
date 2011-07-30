
using System;
namespace gestadh45.model
{
	[Serializable]
	public class Ville : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le code postal
		/// </summary>
		public string CodePostal { get; set; }

		/// <summary>
		/// Obtient/Définit le libellé
		/// </summary>
		public string Libelle { get; set; }
		#endregion

		/// <summary>
		/// Renvoit l'ensemble code postal - ville
		/// </summary>
		/// <returns>Code postal - Villes</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.CodePostal, this.Libelle);
		}
	}
}
