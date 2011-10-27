
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
namespace gestadh45.service.VCards
{
	public class VCardGenerateur
	{
		private string _SaveFilePath;
		private List<DonneesVCard> _Donnees;
		
		public VCardGenerateur(List<DonneesVCard> pDonnees, string pSaveFilePath) {
			this._SaveFilePath = pSaveFilePath;
			this._Donnees = pDonnees;
		}

		public VCardGenerateur(DonneesVCard pDonnees, string pSaveFilePath) {
			this._SaveFilePath = pSaveFilePath;
			this._Donnees = new List<DonneesVCard>();
			this._Donnees.Add(pDonnees);
		}

		public void CreerVCard() {
			var chaineVcards = new StringBuilder();
			
			foreach (DonneesVCard donnee in this._Donnees) {
				var vcard = new VCard();

				vcard.LastName = donnee.Nom;
				vcard.FirstName = donnee.Prenom;

				vcard.DirectDial = donnee.Telephone1;
				vcard.Email = donnee.Mail1;
				vcard.Organization = donnee.LibelleGroupe;

				chaineVcards.Append(vcard.GetVCard());
			}			

			// ecriture du fichier en UTF-8 (sans BOM pour ne pas poser de problèmes avec l'import dans zimbra)
			// TODO voir pour le rendre paramétrable...
			StreamWriter lWriter = new StreamWriter(this._SaveFilePath, false, new System.Text.UTF8Encoding(false));
			lWriter.Write(chaineVcards.ToString());
			lWriter.Close();
		}
	}
}
