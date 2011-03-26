
namespace gestadh45.Model
{
	public partial class InfosClub
	{
		/// <summary>
		/// Obtient le code SIRET (composé du SIREN et du NIC)
		/// </summary>
		public string Siret {
			get {
				return string.Format("{0} {1}", this.Siren, this.NIC);
			}
		}
	}
}
