using gestadh45.service.Documents.Templates;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Pdf;

namespace gestadh45.services.Documents
{
	public class GenerateurDocumentPDF : GenerateurDocumentBase
	{
		const bool Unicode = false;
		const PdfFontEmbedding Embedding = PdfFontEmbedding.Always;
		
		public GenerateurDocumentPDF(DonneesDocument donnees, string savePath)
			: base(donnees, savePath) {
		}

		public override void CreerDocumentAttestation() {
			var document = new Document();
			document.UseCmykColor = true;

			var attestation = new AttestationDocument(document, this._donnees);
			attestation.GenererContenuDocument();

			var pdfRenderer = new PdfDocumentRenderer(Unicode, Embedding);
			pdfRenderer.Document = document;
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(this._savePath);
		}

		public override void CreerDocumentInscription() {
			var document = new Document();
			document.UseCmykColor = true;

			var inscription = new InscriptionDocument(document, this._donnees);
			inscription.GenererContenuDocument();

			var pdfRenderer = new PdfDocumentRenderer(Unicode, Embedding);
			pdfRenderer.Document = document;
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(this._savePath);
		}
	}
}
