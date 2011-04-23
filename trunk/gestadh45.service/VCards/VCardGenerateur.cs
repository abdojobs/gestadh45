
using System.IO;
namespace gestadh45.service.VCards
{
	public class VCardGenerateur : VCard
	{
		private string mSaveFilePath;
		private DonneesVCard mDonnees;
		
		public VCardGenerateur(DonneesVCard pDonnees, string pSaveFilePath) {
			this.mSaveFilePath = pSaveFilePath;
			this.mDonnees = pDonnees;

		}

		public void CreerVCard() {
			base.LastName = this.mDonnees.Nom;
			base.FirstName = this.mDonnees.Prenom;

			base.DirectDial = this.mDonnees.Telephone1;
			base.Email = this.mDonnees.Mail1;
			base.Organization = this.mDonnees.LibelleGroupe;

			// ecriture du fichier
			StreamWriter lWriter = new StreamWriter(this.mSaveFilePath);
			lWriter.Write(this.GetVCard());
			lWriter.Close();
		}
	}
}
