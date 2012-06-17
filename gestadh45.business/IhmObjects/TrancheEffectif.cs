
namespace gestadh45.business.IhmObjects
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
		/// Obtient/Définit l'effectif de résidents homme pour cette tranche
		/// </summary>
		public int EffectifResidentsH { get; set; }

		/// <summary>
		/// Obtient/Définit l'effectif de résidents femme pour cette tranche
		/// </summary>
		public int EffectifResidentsF { get; set; }

		/// <summary>
		/// Obtient/Définit l'effectif d'extérieurs homme pour cettetranche
		/// </summary>
		public int EffectifExterieursH { get; set; }

		/// <summary>
		/// Obtient/Définit l'effectif d'extérieurs femme pour cettetranche
		/// </summary>
		public int EffectifExterieursF { get; set; }

		/// <summary>
		/// Obtient l'effectif total extérieurs (H + F)
		/// </summary>
		public int EffectifTotalExterieurs {
			get { return this.EffectifExterieursH + this.EffectifExterieursF; }
		}

		/// <summary>
		/// Obtient l'effectif total résidents (H + F)
		/// </summary>
		public int EffectifTotalResidents {
			get { return this.EffectifResidentsH + this.EffectifResidentsF; }
		}

		/// <summary>
		/// Obtient l'effectif total (résidents + extérieurs)
		/// </summary>
		public int EffectifTotal {
			get { return this.EffectifTotalResidents + this.EffectifTotalExterieurs; }
		}

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
