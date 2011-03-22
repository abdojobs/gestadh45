
namespace gestadh45.service.Documents
{
	public abstract class GenerateurDocumentBase
	{
		public const string CodeInscriptionPdf = "FicheInscriptionPDF";
		public const string CodeAttestationPdf = "AttestationPDF";
		
		protected DonneesDocument mDonnees;
		protected string mSavePath;

		public GenerateurDocumentBase(DonneesDocument pDonnees, string pSavePath) {
			this.mDonnees = pDonnees;
			this.mSavePath = pSavePath;
		}

		public abstract void CreerDocumentAttestation();
		public abstract void CreerDocumentInscription();
	}
}
