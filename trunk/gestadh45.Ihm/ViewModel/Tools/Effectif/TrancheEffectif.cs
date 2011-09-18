
namespace gestadh45.Ihm.ViewModel.Tools.Effectif
{
	/// <summary>
	/// Classe représentant une tranche d'effectif
	/// </summary>
	public class TrancheEffectif
	{
		#region properties
		/// <summary>
		/// Obtient/Définit l'âge inférieur de la tranche (inclus)
		/// </summary>
		public int AgeInferieur { get; set; }

		/// <summary>
		/// Obtient/Définit l'âge supérieur de la tranche (inclus)
		/// </summary>
		public int AgeSuperieur { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si la tranche concerne les résidents oul es extérieurs
		/// </summary>
		public bool EstResident { get; set; }

		/// <summary>
		/// Obtient/Définit l'effectif de la tranche
		/// </summary>
		public int Effectif { get; set; }

		/// <summary>
		/// Obtient le libellé de la tranche
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}
		#endregion

		public override string ToString() {
			var statut = "extérieur";
			
			if (this.EstResident) {
				statut = "résident";
			}

			return string.Format("{0} - {1} ({2})", this.AgeInferieur, this.AgeSuperieur, statut);
		}
	}
}
