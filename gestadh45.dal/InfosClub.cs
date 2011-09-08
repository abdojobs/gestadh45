
namespace gestadh45.dal
{
	public partial class InfosClub
	{
		/// <summary>
		/// Gets the siret. (SIREN - NIC)
		/// </summary>
		public string Siret {
			get {
				return string.Format("{0} - {1}", this.Siren, this.NIC);
			}
		}
	}
}
