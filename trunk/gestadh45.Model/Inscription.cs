
namespace gestadh45.Model
{
	public partial class Inscription
	{
		public override string ToString() {
			return string.Format("{0} - {1}", this.Adherent, this.Groupe.Libelle);
		}
	}
}
