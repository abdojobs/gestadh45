
namespace gestadh45.model
{
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

		public Ville() {
			this.CodePostal = string.Empty;
			this.Libelle = string.Empty;
		}

		/// <summary>
		/// Renvoit l'ensemble code postal - ville
		/// </summary>
		/// <returns>Code postal - Villes</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.CodePostal, this.Libelle);
		}

		public override bool EstValide() {
			return !string.IsNullOrWhiteSpace(this.Libelle) && !string.IsNullOrWhiteSpace(this.CodePostal);
		}
	}
}
