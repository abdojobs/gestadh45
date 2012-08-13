using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.services.Documents.Templates
{
	public class InscriptionDocument : GeneriqueDocument
	{
		public InscriptionDocument(Document document, DonneesDocument donnees)
			: base(document, donnees) {
		}

		private void CreerZoneTitre() {
			var par = this._page.AddParagraph();

			par.AddFormattedText(
				string.Format(ResDocuments.LibelleTitreFicheInscription, this._donnees.Saison, this._donnees.NomClub),
				TextFormat.Bold
			);
			par.Format.Alignment = ParagraphAlignment.Center;
		}

		private void CreerZoneCoordonneesAdherent() {
			var parNomPrenomAdherent = this._page.AddParagraph();
			parNomPrenomAdherent.AddFormattedText(
				string.Format(ResDocuments.LibelleNomAdherent, this._donnees.NomAdherent, this._donnees.PrenomAdherent)
			);

			var parDateNaissance = this._page.AddParagraph();
			parDateNaissance.AddFormattedText(
				string.Format(ResDocuments.LibelleNeLe, this._donnees.DateNaissanceAdherent)
			);

			var parAdresse = this._page.AddParagraph();
			parAdresse.AddFormattedText(
				string.Format(ResDocuments.LibelleAdresse, this._donnees.AdresseAdherent, this._donnees.CodePostalAdherent, this._donnees.VilleAdherent)
			);

			var parTelMail1 = this._page.AddParagraph();
			parTelMail1.AddFormattedText(
				string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone1Adherent, this._donnees.Mail1Adherent)
			);

			var parTelMail2 = this._page.AddParagraph();
			parTelMail2.AddFormattedText(
				string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone2Adherent, this._donnees.Mail2Adherent)
			);

			var parTelMail3 = this._page.AddParagraph();
			parTelMail3.AddFormattedText(
				string.Format(ResDocuments.LibelleTelMail, this._donnees.Telephone3Adherent, this._donnees.Mail3Adherent)
			);
		}

		private void CreerZoneInfosInscription() {
			var parInfosCotisation = this._page.AddParagraph();
			parInfosCotisation.AddFormattedText(
				string.Format(ResDocuments.LibelleCotisation, this._donnees.CotisationInscription)
			);

			var parInfosGroupe = this._page.AddParagraph();
			parInfosGroupe.AddFormattedText(
				string.Format(ResDocuments.LibelleGroupe, this._donnees.GroupeInscription)
			);
		}

		private void CreerZoneSignature() {
			var parSignature = this._page.AddParagraph();
			parSignature.AddFormattedText(
				ResDocuments.LibelleSignatureAdherent
			);
		}

		private void CreerZoneAutorisation() {
			var parTitreAutorisation = this._page.AddParagraph();
			parTitreAutorisation.AddFormattedText(
				ResDocuments.LibelleAutorisationParentale,
				TextFormat.Bold
			);

			var parTexteAutorisation = this._page.AddParagraph();
			parTexteAutorisation.AddFormattedText(
				ResDocuments.TexteAutorisationParentale
			);
		}

		private void CreerZoneSignatureParents() {
			var parSignature = this._page.AddParagraph();
			parSignature.AddFormattedText(
				ResDocuments.LibelleSignatureParents
			);
		}
		

		public void GenererContenuDocument() {
			this.CreerEntete();
			this.CreerRetourLigne();

			this.CreerSeparateur();
			this.CreerRetourLigne();

			this.CreerZoneTitre();
			this.CreerRetourLigne();

			this.CreerZoneCoordonneesAdherent();
			this.CreerRetourLigne();

			this.CreerZoneInfosInscription();
			this.CreerRetourLigne();

			this.CreerRetourLigne();
			this.CreerZoneSignature();
			this.CreerRetourLigne();

			this.CreerSeparateur();
			this.CreerRetourLigne();

			this.CreerZoneAutorisation();
			this.CreerRetourLigne();

			this.CreerZoneSignatureParents();
		}
	}
}
