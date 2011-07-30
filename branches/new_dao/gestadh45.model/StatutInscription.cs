
using System;
namespace gestadh45.model
{
	[Serializable]
	public class StatutInscription : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le libellé
		/// </summary>
		public string Libelle { get; set; }

		/// <summary>
		/// Obtient/Définit le code couleur
		/// </summary>
		public string CodeCouleur { get; set; }

		/// <summary>
		/// Obtient/Définit l'ordre d'affichage
		/// </summary>
		public int Ordre { get; set; }
		#endregion

		/// <summary>
		/// Renvoit le libellé du statut
		/// </summary>
		/// <returns>Libellé du statut</returns>
		public override string ToString() {
			return this.Libelle;
		}
	}
}
