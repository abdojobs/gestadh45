using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.service.Documents.Templates
{
	public abstract class GeneriqueDocument
	{
		private Document _document;
		protected Section _page;
		protected DonneesDocument _donnees;

		protected GeneriqueDocument(Document document, DonneesDocument donnees) {
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
				string.Format(ResDocuments.LibelleAdresseClub, this._donnees.AdresseClub)
			);

			var parCodePostalVilleClub = this._page.AddParagraph();
			parCodePostalVilleClub.AddFormattedText(
				string.Format(ResDocuments.LibelleCodePostalVilleClub, this._donnees.CodePostalClub, this._donnees.VilleClub)
			);

			var parTelClub = this._page.AddParagraph();
			parTelClub.AddFormattedText(
				string.Format(ResDocuments.LibelleTelephone, this._donnees.TelephoneCLub)
			);

			var parMailClub = this._page.AddParagraph();
			parMailClub.AddFormattedText(
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

		protected void CreerSeparateur() {
			var parSeparateur = this._page.AddParagraph();
			parSeparateur.AddFormattedText(ResDocuments.Separateur, TextFormat.Bold);
			parSeparateur.Format.Alignment = ParagraphAlignment.Center;
		}

		protected void CreerRetourLigne() {
			var parLigneVide = this._page.AddParagraph();
			parLigneVide.AddLineBreak();
		}
	}
}
