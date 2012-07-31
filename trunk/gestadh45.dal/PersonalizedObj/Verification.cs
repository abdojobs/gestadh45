
namespace gestadh45.dal
{
	public partial class Verification
	{
		public override string ToString() {
			return string.Format("{0} : {1}", this.CampagneVerification.Date.ToShortDateString(), this.Equipement.ToString());
		} 
	}
}
