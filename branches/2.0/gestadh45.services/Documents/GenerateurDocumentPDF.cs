using gestadh45.service.Documents.Templates;
using PdfSharp.Pdf;

namespace gestadh45.services.Documents
{
	public class GenerateurDocumentPDF : GenerateurDocumentBase
	{
		public GenerateurDocumentPDF(DonneesDocument donnees, string savePath)
			: base(donnees, savePath) {
		}

		public override void CreerDocumentAttestation() {
			var document = new PdfDocument();
			var page = document.AddPage();

			var attestation = new AttestationDocumentPDF(page, this._donnees);
			attestation.GenererContenuDocument();
			document.Save(this._savePath);
		}

		public override void CreerDocumentInscription() {
			var document = new PdfDocument();
			var page = document.AddPage();

			var attestation = new InscriptionDocumentPDF(page, this._donnees);
			attestation.GenererContenuDocument();
			document.Save(this._savePath);
		}
	}
}
