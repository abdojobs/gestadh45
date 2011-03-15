
namespace gestadh45.Model
{
	public partial class Groupe
	{
		public override string ToString() {
			return string.Format(
				"{0} ({1}H{2} - {3}H{4})", 
				this.JourSemaine.ToString(), 
				this.HeureDebut.ToString("00"), 
				this.MinuteDebut.ToString("00"), 
				this.HeureFin.ToString("00"), 
				this.MinuteFin.ToString("00")
			);
		}
	}
}
