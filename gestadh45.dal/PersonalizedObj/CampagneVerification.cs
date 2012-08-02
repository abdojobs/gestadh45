using System.Linq;

namespace gestadh45.dal
{
	public partial class CampagneVerification
	{
		public override string ToString() {
			return string.Format("{0} - {1}", this.Date.ToShortDateString(), this.Responsable);
		}

		/// <summary>
		/// Obtient une chaîne décrivant l'objet
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}

		/// <summary>
		/// Obtient le nombre d'équipements concernés par la campagne
		/// </summary>
		public int NbEquipements {
			get { return this.Verifications.Count; }
		}
	}
}
