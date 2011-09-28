
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
		/// Obtient/Définit l'effectif de résidents pour cette tranche
		/// </summary>
		public int EffectifResidents { get; set; }

		/// <summary>
		/// Obtient/Définit l'effectif d'extérieurs pour cettetranche
		/// </summary>
		public int EffectifExterieurs { get; set; }

		/// <summary>
		/// Obtient une chaîne décrivant l'objet (ToString)
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}
		#endregion

		public override string ToString() {
			return string.Format("{0} - {1} ans", this.AgeInferieur, this.AgeSuperieur);
		}
	}
}
