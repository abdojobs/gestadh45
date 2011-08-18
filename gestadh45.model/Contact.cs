
using System;
using System.Text;
using gestadh45.model.bo;
namespace gestadh45.model
{
	[Serializable]
	public class Contact : BaseClass
	{
		private const string SeparateurAdressesMail = ",";
		
		#region properties
		/// <summary>
		/// Obtient/Définit le téléphone1
		/// </summary>
		public NumeroTelephone Telephone1 { get; set; }

		/// <summary>
		/// Obtient/Définit le téléphone3
		/// </summary>
		public NumeroTelephone Telephone2 { get; set; }

		/// <summary>
		/// Obtient/Définit le téléphone3
		/// </summary>
		public NumeroTelephone Telephone3 { get; set; }

		/// <summary>
		/// Obtient/Définit le mail1
		/// </summary>
		public AdresseEmail Mail1 { get; set; }

		/// <summary>
		/// Obtient/Définit le mail2
		/// </summary>
		public AdresseEmail Mail2 { get; set; }

		/// <summary>
		/// Obtient/Définit le mail3
		/// </summary>
		public AdresseEmail Mail3 { get; set; }

		/// <summary>
		/// Obtient/Définit le site web
		/// </summary>
		public string SiteWeb { get; set; }

		/// <summary>
		/// Obtient la liste des adresses emails sous forme d'une chaîne, avec un séparateur standard
		/// </summary>
		public string ChaineMails {
			get {
				StringBuilder lSb = new StringBuilder();

				if (!string.IsNullOrWhiteSpace(this.Mail1.ToString())) {
					lSb.Append(this.Mail1 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail2.ToString())) {
					lSb.Append(this.Mail2 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail3.ToString())) {
					lSb.Append(this.Mail3 + SeparateurAdressesMail);
				}

				return lSb.ToString();
			}
		}
		#endregion
	}
}
