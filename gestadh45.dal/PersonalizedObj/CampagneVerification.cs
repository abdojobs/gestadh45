
namespace gestadh45.dal
{
	public partial class CampagneVerification
	{
		public override string ToString() {
			return string.Format("{0} - {1}", this.Date.ToShortDateString(), this.Responsable);
		}
	}
}
