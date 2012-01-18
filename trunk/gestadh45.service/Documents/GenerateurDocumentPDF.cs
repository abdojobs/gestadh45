using System.IO;
using gestadh45.service.Documents.Templates;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace gestadh45.service.Documents
{
	public class GenerateurDocumentPDF : GenerateurDocumentBase
	{
		public GenerateurDocumentPDF(DonneesDocument pDonnees, string pSavePath)
			: base(pDonnees, pSavePath) {
		}

		public override void CreerDocumentAttestation() {
			Document document = new Document();

			using (var os = new FileStream(base.mSavePath, FileMode.Create)) {
				PdfWriter.GetInstance(document, os);
				document.Open();
				document.Add(new AttestationDocument(base.mDonnees).GenererContenuDocument());
			}
		}

		public override void CreerDocumentInscription() {
			Document document = new Document();

			using (var os = new FileStream(base.mSavePath, FileMode.Create)) {
				PdfWriter.GetInstance(document, os);
				document.Open();
				document.Add(new InscriptionDocument(base.mDonnees).GenererContenuDocument());
			}
		}
	}
}
