
namespace gestadh45.Model
{
	public partial class Ville
	{
		public override string ToString() {
			return string.Format("{0} - {1}", this.CodePostal, this.Libelle);
		}
	}
}
