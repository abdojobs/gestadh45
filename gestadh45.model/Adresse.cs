
using System;
namespace gestadh45.model
{
	[Serializable]
	public class Adresse : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le libellé de l'adresse
		/// </summary>
		public string Libelle { get; set; }

		/// <summary>
		/// Obtient/Définit la ville
		/// </summary>
		public Ville Ville { get; set; }
		#endregion

		/// <summary>
		/// Renvoit adresse ville
		/// </summary>
		/// <returns>adresse ville</returns>
		public override string ToString() {
			return string.Format("{0} {1}", this.Libelle, this.Ville);
		}
	}
}
