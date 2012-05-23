
namespace gestadh45.dal
{
	public partial class Groupe
	{
		/// <summary>
		/// Affiche la description du groupe
		/// </summary>
		/// <returns>JourSemaine (HeureDebut - HeureFin)</returns>
		public override string ToString() {
			return string.Format(
				"{0} ({1} - {2})",
				this.JourSemaine.ToString(),
				this.HeureDebut.ToString("t"),
				this.HeureFin.ToString("t")
			);
		}

		/// <summary>
		/// Gets the nb places libres.
		/// </summary>
		public int NbPlacesLibres {
			get {
				return this.NbPlaces - this.Inscriptions.Count;
			}
		}
	}
}
