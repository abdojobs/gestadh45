using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.service.Documents.Templates
{
	public abstract class GeneriqueDocumentPDF
	{
		private Document _document;
		protected Section _page;
		protected DonneesDocument _donnees;

		protected GeneriqueDocumentPDF(Document document, DonneesDocument donnees) {
			this._document = document;
			this._page = this._document.AddSection();
			this._donnees = donnees;
		}

		protected void CreerEntete() {
			var parNomClubSaison = this._page.AddParagraph();
			parNomClubSaison.AddFormattedText(
				string.Format(ResDocuments.LibelleNomClubSaison, this._donnees.NomClub, this._donnees.Saison),
				TextFormat.Bold
			);

			var parAdresseClub = this._page.AddParagraph();
			parAdresseClub.AddFormattedText(
				string.Format(ResDocuments.LibelleNomClubSaison, this._donnees.NomClub, this._donnees.Saison)
			);

			var parTelClub = this._page.AddParagraph();
			parTelClub.AddFormattedText(
				string.Format(ResDocuments.LibelleTelephone, this._donnees.TelephoneCLub)
			);

			var parMailClub = this._page.AddParagraph();
			parTelClub.AddFormattedText(
				string.Format(ResDocuments.LibelleMailClub, this._donnees.MailClub)
			);

			var parSiteWebClub = this._page.AddParagraph();
			parSiteWebClub.AddFormattedText(
				string.Format(ResDocuments.LibelleSiteWebClub, this._donnees.SiteWebClub)
			);

			var parNumeroClub = this._page.AddParagraph();
			parNumeroClub.AddFormattedText(
				string.Format(ResDocuments.LibelleNumeroClub, this._donnees.NumeroClub)
			);

			var parSiretClub = this._page.AddParagraph();
			parSiretClub.AddFormattedText(
				string.Format(ResDocuments.LibelleSiret, this._donnees.SiretClub)
			);
		}
	}
}
