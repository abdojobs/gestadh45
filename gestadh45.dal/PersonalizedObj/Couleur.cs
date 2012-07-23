
namespace gestadh45.dal
{
	public partial class Couleur
	{
		public override string ToString() {
			var value = string.Format("{0}/{1}/{2}", this.Couleur1, this.Couleur2, this.Couleur3);

			if (value.EndsWith("/")) {
				value = value.Substring(0, value.Length - 1);
			}

			return value;
		}
	}
}
