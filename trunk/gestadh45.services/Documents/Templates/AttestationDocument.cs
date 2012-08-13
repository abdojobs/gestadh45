using System;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using MigraDoc.DocumentObjectModel;

namespace gestadh45.services.Documents.Templates
{
	public class AttestationDocument : GeneriqueDocument
	{
		public AttestationDocument(Document document, DonneesDocument donnees)
			: base(document, donnees) {
		}

		private void CreerZoneTitre() {
			var par = this._page.AddParagraph();
			par.AddFormattedText(
				string.Format(ResDocuments.LibelleRecuPourAdhesion, this._donnees.Saison, this._donnees.NomClub),
				TextFormat.Bold
			);
			par.Format.Alignment = ParagraphAlignment.Center;
		}

		private void CreerZoneCoordonneesAdherent() {
			var parNomAdherent = this._page.AddParagraph();
			parNomAdherent.AddFormattedText(
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
		}

		private void CreerZoneInfosAttestation() {
			var par = this._page.AddParagraph();
			par.AddFormattedText(
				string.Format(ResDocuments.LibelleCotisation, this._donnees.CotisationInscription),
				TextFormat.Bold
			);
			par.Format.Alignment = ParagraphAlignment.Center;
		}

		private void CreerZoneLieuDate() {
			var par = this._page.AddParagraph();
			par.AddFormattedText(
				string.Format(ResDocuments.LibelleFaitALe, this._donnees.VilleClub, DateTime.Now.ToShortDateString())
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

			this.CreerZoneInfosAttestation();
			this.CreerRetourLigne();

			this.CreerRetourLigne();
			this.CreerZoneLieuDate();
		}
	}
}
