using System;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace gestadh45.service.Documents.Templates
{
	public class AttestationDocumentPDF : GeneriqueDocumentPDF
	{
		public AttestationDocumentPDF(PdfPage page, DonneesDocument donnees)
			: base(page, donnees) {
		}

		private void CreerZoneTitre() {
			using (var gfxRecuPourAdhesion = XGraphics.FromPdfPage(this._page)) {
				gfxRecuPourAdhesion.DrawString(
					string.Format(ResDocuments.LibelleRecuPourAdhesion, this._donnees.Saison, this._donnees.NomClub),
					this.fontTitre,
					XBrushes.Black,
					new XRect(40, 200, this._page.Width - 80, 0),
					XStringFormats.Center
				);
			}
		}

		private void CreerZoneCoordonneesAdherent() {			
			using (var gfxNomAdherent = XGraphics.FromPdfPage(this._page)) {
				gfxNomAdherent.DrawString(
					string.Format(ResDocuments.LibelleNomAdherent, this._donnees.NomAdherent, this._donnees.PrenomAdherent),
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 235, this._page.Width - 80, 0)
				);
			}

			using (var gfxNaissance = XGraphics.FromPdfPage(this._page)) {
				gfxNaissance.DrawString(
					string.Format(ResDocuments.LibelleNeLe, this._donnees.DateNaissanceAdherent),
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 250, this._page.Width - 80, 0)
				);
			}

			using (var gfxAdresse = XGraphics.FromPdfPage(this._page)) {
				gfxAdresse.DrawString(
					string.Format(ResDocuments.LibelleAdresse, this._donnees.AdresseAdherent, this._donnees.CodePostalAdherent, this._donnees.VilleAdherent),
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 265, this._page.Width - 80, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 285, this._page.Width - 40, 285);
			}
		}

		private void CreerZoneInfosAttestation() {
			using (var gfxInfosCotisation = XGraphics.FromPdfPage(this._page)) {
				gfxInfosCotisation.DrawString(
					string.Format(ResDocuments.LibelleCotisation,this._donnees.CotisationInscription),
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 305, this._page.Width - 80, 0)
				);
			}
		}

		private void CreerZoneLieuDate() {
			using (var gfxFaitALe = XGraphics.FromPdfPage(this._page)) {
				gfxFaitALe.DrawString(
					string.Format(ResDocuments.LibelleFaitALe, this._donnees.VilleClub, DateTime.Now.ToShortDateString()),
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 400, this._page.Width - 80, 0)
				);
			}
		}

		public void GenererContenuDocument() {
			base.CreerEntete();
			this.CreerZoneTitre();
			this.CreerZoneCoordonneesAdherent();
			this.CreerZoneInfosAttestation();
			this.CreerZoneLieuDate();
		}
	}
}
