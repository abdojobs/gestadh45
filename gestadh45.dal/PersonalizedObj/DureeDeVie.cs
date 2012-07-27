
namespace gestadh45.dal
{
	public partial class DureeDeVie
	{
		public override string ToString() {
			return string.Format("{0} ({1} ans, {2} mois)", this.Libelle, this.NbAnnees, this.NbMois);
		}
	}
}
