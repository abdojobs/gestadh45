using System.Text;

namespace gestadh45.Model
{
	public partial class Contact
	{
		private const string SeparateurAdressesMail = ",";
		
		/// <summary>
		/// Obtient la liste des adresses emails sous forme d'une chaîne, avec un séparateur standard
		/// </summary>
		public string ChaineMails {
			get {
				StringBuilder lSb = new StringBuilder();

				if (!string.IsNullOrWhiteSpace(this.Mail1)) {
					lSb.Append(this.Mail1 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail2)) {
					lSb.Append(this.Mail2 + SeparateurAdressesMail);
				}

				if (!string.IsNullOrWhiteSpace(this.Mail3)) {
					lSb.Append(this.Mail3 + SeparateurAdressesMail);
				}

				return lSb.ToString();
			}
		}
	}
}
