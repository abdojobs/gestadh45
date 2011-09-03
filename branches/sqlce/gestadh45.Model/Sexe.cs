
namespace gestadh45.Model
{
	public partial class Sexe
	{
		public string ToLongString() {
			return this.LibelleLong;
		}

		public override string ToString() {
			return this.LibelleCourt;
		}
	}
}
