
namespace gestadh45.dal
{
	public partial class Verification
	{
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.DateVerification.ToShortDateString(), this.Equipement.Numero);
		}
	}
}
