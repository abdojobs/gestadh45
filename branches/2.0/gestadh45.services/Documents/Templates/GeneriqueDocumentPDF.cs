using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace gestadh45.service.Documents.Templates
{
	public abstract class GeneriqueDocumentPDF
	{
		// polices
		protected XFont fontCoordonneesClub = new XFont("Times-Roman", 10f, XFontStyle.Regular);
		protected XFont fontLabel = new XFont("Helvetica-Bold", 10f, XFontStyle.Bold);
		protected XFont fontValue = new XFont("Helvetica", 10f, XFontStyle.Regular);
		protected XFont fontNomClubSaison = new XFont("Helvetica-Bold", 16f, XFontStyle.Bold);
		protected XFont fontTitre = new XFont("Helvetica-Bold", 12f, XFontStyle.Bold);


		protected PdfPage _page;
		protected DonneesDocument _donnees;

		protected GeneriqueDocumentPDF(PdfPage page, DonneesDocument donnees) {
			this._page = page;
			this._donnees = donnees;
		}

		protected void CreerEntete() {
			using (var gfxNomClubSaison = XGraphics.FromPdfPage(this._page)) {
				gfxNomClubSaison.DrawString(
					string.Format(ResDocuments.LibelleNomClubSaison, this._donnees.NomClub, this._donnees.Saison),
					this.fontNomClubSaison,
					XBrushes.Black,
					new XRect(40, 40, 0, 0)
				);
			}

			using (var gfxAdresseClub = XGraphics.FromPdfPage(this._page)) {
				gfxAdresseClub.DrawString(
					string.Format(ResDocuments.LibelleAdresse, this._donnees.AdresseClub, this._donnees.CodePostalClub, this._donnees.VilleClub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 70, 0, 0)
				);
			}

			using (var gfxTelephoneClub = XGraphics.FromPdfPage(this._page)) {
				gfxTelephoneClub.DrawString(
					string.Format(ResDocuments.LibelleTelephone, this._donnees.TelephoneCLub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 85, 0, 0)
				);
			}

			using (var gfxMailClub = XGraphics.FromPdfPage(this._page)) {
				gfxMailClub.DrawString(
					string.Format(ResDocuments.LibelleMailClub, this._donnees.TelephoneCLub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 100, 0, 0)
				);
			}

			using (var gfxSiteWebClub = XGraphics.FromPdfPage(this._page)) {
				gfxSiteWebClub.DrawString(
					string.Format(ResDocuments.LibelleSiteWebClub, this._donnees.MailClub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 115, 0, 0)
				);
			}

			using (var gfxNumeroClub = XGraphics.FromPdfPage(this._page)) {
				gfxNumeroClub.DrawString(
					string.Format(ResDocuments.LibelleNumeroClub, this._donnees.NumeroClub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 130, 0, 0)
				);
			}

			using (var gfxSiretClub = XGraphics.FromPdfPage(this._page)) {
				gfxSiretClub.DrawString(
					string.Format(ResDocuments.LibelleSiret, this._donnees.SiretClub),
					this.fontCoordonneesClub,
					XBrushes.Black,
					new XRect(40, 145, 0, 0)
				);
			}

			using (var gfxSeparator = XGraphics.FromPdfPage(this._page)) {
				gfxSeparator.DrawLine(XPens.Black, 40, 175, this._page.Width - 40, 175);
			}
		}
	}
}
