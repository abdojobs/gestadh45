
namespace gestadh45.model
{
	public class InfosClub : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit le nom
		/// </summary>
		public string Nom { get; set; }

		/// <summary>
		/// Obtient/Définit l'adresse
		/// </summary>
		public Adresse Adresse { get; set; }

		/// <summary>
		/// Obtient/Définit le contact
		/// </summary>
		public Contact Contact { get; set; }

		/// <summary>
		/// Obtient/Définit le numéro
		/// </summary>
		public string Numero { get; set; }

		/// <summary>
		/// Obtient/Définit le Siren
		/// </summary>
		public string Siren { get; set; }

		/// <summary>
		/// Obtient/Définit le NIC
		/// </summary>
		public string NIC { get; set; }
		#endregion
	}
}
