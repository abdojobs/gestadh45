using gestadh45.services.Documents;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using gestadh45.services.Documents.Templates;

namespace gestadh45.service.Documents.Templates
{
	public class InscriptionDocumentPDF : GeneriqueDocumentPDF
	{
		public InscriptionDocumentPDF(PdfPage page, DonneesDocument donnees)
			: base(page, donnees) {
		}

		private void CreerZoneTitre() {
			using (var gfxTitreInscription = XGraphics.FromPdfPage(this._page)) {
				gfxTitreInscription.DrawString(
					string.Format(ResDocuments.LibelleTitreFicheInscription, this._donnees.Saison, this._donnees.NomClub),
					this.fontTitre,
					XBrushes.Black,
					new XRect(40, 200, this._page.Width - 80, 0),
					XStringFormats.Center
				);
			}
		}

		private void CreerZoneCoordonneesAdherent() {			
			using (var gfxNomPrenomAdherent = XGraphics.FromPdfPage(this._page)) {
				gfxNomPrenomAdherent.DrawString(
					string.Format(ResDocuments.LibelleNomAdherent, this._donnees.NomAdherent, this._donnees.PrenomAdherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 235, this._page.Width - 80, 0)
				);
			}

			using (var gfxNaissance = XGraphics.FromPdfPage(this._page)) {
				gfxNaissance.DrawString(
					string.Format(ResDocuments.LibelleNeLe, this._donnees.DateNaissanceAdherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 250, this._page.Width - 80, 0)
				);
			}

			using (var gfxAdresse = XGraphics.FromPdfPage(this._page)) {
				gfxAdresse.DrawString(
					string.Format(ResDocuments.LibelleAdresse, this._donnees.AdresseAdherent, this._donnees.CodePostalAdherent, this._donnees.VilleAdherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 265, this._page.Width - 80, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 285, this._page.Width - 40, 285);
			}

			using (var gfxTelMail1 = XGraphics.FromPdfPage(this._page)) {
				gfxTelMail1.DrawString(
					string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone1Adherent, this._donnees.Mail1Adherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 305, this._page.Width - 80, 0)
				);
			}

			using (var gfxTelMail2 = XGraphics.FromPdfPage(this._page)) {
				gfxTelMail2.DrawString(
					string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone2Adherent, this._donnees.Mail2Adherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 320, this._page.Width - 80, 0)
				);
			}

			using (var gfxTelMail3 = XGraphics.FromPdfPage(this._page)) {
				gfxTelMail3.DrawString(
					string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone3Adherent, this._donnees.Mail3Adherent),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 335, this._page.Width - 80, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 355, this._page.Width - 40, 355);
			}
		}

		private void CreerZoneInfosInscription() {
			using (var gfxInfosCotisation = XGraphics.FromPdfPage(this._page)) {
				gfxInfosCotisation.DrawString(
					string.Format(ResDocuments.LibelleCotisation, this._donnees.CotisationInscription),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 375, this._page.Width - 80, 0)
				);
			}

			using (var gfxInfoGroupe = XGraphics.FromPdfPage(this._page)) {
				gfxInfoGroupe.DrawString(
					string.Format(ResDocuments.LibelleGroupe, this._donnees.GroupeInscription),
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 395, this._page.Width - 80, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 415, this._page.Width - 40, 415);
			}
		}

		private void CreerZoneSignature() {
			using (var gfxSignature = XGraphics.FromPdfPage(this._page)) {
				gfxSignature.DrawString(
					ResDocuments.LibelleSignatureAdherent,
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 450, this._page.Width - 80, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 470, this._page.Width - 40, 470);
			}
		}

		private void CreerZoneAutorisation() {
			using (var gfxTitreAutorisation = XGraphics.FromPdfPage(this._page)) {
				gfxTitreAutorisation.DrawString(
					ResDocuments.LibelleAutorisationParentale,
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 490, this._page.Width - 80, 0),
					XStringFormats.Center
				);
			}

			using (var gfxTexteAutorisation = XGraphics.FromPdfPage(this._page)) {
				gfxTexteAutorisation.DrawString(
					ResDocuments.TexteAutorisationParentale,
					this.fontValue,
					XBrushes.Black,
					new XRect(40, 510, this._page.Width - 80, 0)
				);
			}

			using (var gfxSignatureParents = XGraphics.FromPdfPage(this._page)) {
				gfxSignatureParents.DrawString(
					ResDocuments.LibelleSignatureParents,
					this.fontLabel,
					XBrushes.Black,
					new XRect(40, 550, this._page.Width - 80, 0)
				);
			}
		}

		public void GenererContenuDocument() {
			base.CreerEntete();
			this.CreerZoneTitre();
			this.CreerZoneCoordonneesAdherent();
			this.CreerZoneInfosInscription();
			this.CreerZoneSignature();
			this.CreerZoneAutorisation();
		}
	}
}
