
using System;
namespace gestadh45.model
{
	[Serializable]
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

		/// <summary>
		/// Obtient le code SIRET (composé du SIREN et du NIC)
		/// </summary>
		public string Siret {
			get {
				return string.Format("{0} - {1}", this.Siren, this.NIC);
			}
		}
		#endregion

		public InfosClub() {
			this.Adresse = new Adresse();
			this.Contact = new Contact();
		}
	}
}
