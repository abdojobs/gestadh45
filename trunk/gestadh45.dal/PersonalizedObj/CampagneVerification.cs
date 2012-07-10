
namespace gestadh45.dal
{
	public partial class CampagneVerification
	{
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.Date.ToShortDateString(), this.Libelle);
		}

		/// <summary>
		/// Obtient le nombre d'équipements (vérificatioons) inclus dans cette campagne
		/// </summary>
		public int NbEquipements {
			get { return this.Verifications.Count; }
		}

		/// <summary>
		/// Obtient un libellé définissant le statut (Ouverte/Fermée)
		/// </summary>
		public string LibelleStatut {
			get { return this.EstFermee ? "Fermée" : "Ouverte"; }
		}
	}
}
