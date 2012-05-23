
namespace gestadh45.services.Documents
{
	public abstract class GenerateurDocumentBase
	{
		public const string CodeInscriptionPdf = "FicheInscriptionPDF";
		public const string CodeAttestationPdf = "AttestationPDF";

		protected DonneesDocument _donnees;
		protected string _savePath;

		public GenerateurDocumentBase(DonneesDocument donnees, string savePath) {
			this._donnees = donnees;
			this._savePath = savePath;
		}

		public void CreerDocument(string codeDocument) {
			switch (codeDocument) {
				case CodeAttestationPdf:
					this.CreerDocumentAttestation();
					break;

				case CodeInscriptionPdf:
					this.CreerDocumentInscription();
					break;
			}
		}
		public abstract void CreerDocumentAttestation();
		public abstract void CreerDocumentInscription();
	}
}
