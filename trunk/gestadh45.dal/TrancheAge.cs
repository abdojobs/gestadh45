
namespace gestadh45.dal
{
	public partial class TrancheAge
	{
		public override string ToString() {
			return string.Format("{0] - {1}", this.AgeInf, this.AgeSup);
		}
	}
}
