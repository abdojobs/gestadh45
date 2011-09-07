using iTextSharp.text;

namespace gestadh45.service.Documents.Templates
{
	public class InscriptionDocument : GeneriqueDocument
	{
		public InscriptionDocument(DonneesDocument pDonnees)
			: base(pDonnees) {
		}

		private void CreerZoneAutorisation() {
			Paragraph element = new Paragraph();
			element.Add(new Chunk(ResDocuments.LibelleAutorisationParentale, GeneriqueDocument.FontLabel));
			Paragraph paragraph2 = new Paragraph();
			paragraph2.Add(new Chunk(ResDocuments.TexteAutorisationParentale, GeneriqueDocument.FontValue));
			Paragraph paragraph3 = new Paragraph();
			paragraph3.Add(new Chunk(ResDocuments.LibelleFaitALe, GeneriqueDocument.FontLabel));
			Paragraph paragraph4 = new Paragraph();
			paragraph4.Add(new Chunk(ResDocuments.LibelleSignatureParent, GeneriqueDocument.FontLabel));
			Table table = new Table(1, 4)
			{
				Locked = false,
				Width = 95f,
				Border = 0
			};
			Cell aCell = new Cell(element)
			{
				Border = 0
			};
			table.AddCell(aCell, 0, 0);
			Cell cell2 = new Cell(paragraph2)
			{
				Border = 0
			};
			table.AddCell(cell2, 1, 0);
			Cell cell3 = new Cell(paragraph3)
			{
				Border = 0
			};
			table.AddCell(cell3, 2, 0);
			Cell cell4 = new Cell(paragraph4)
			{
				Border = 0
			};
			table.AddCell(cell4, 3, 0);
			base.ContenuDocument.AddCell(new Cell(table));
		}

		private void CreerZoneCoordonneesAdherent() {
			Paragraph element = new Paragraph(ResDocuments.TitreFicheInscription, GeneriqueDocument.FontTitre)
			{
				Alignment = 1
			};
			Paragraph paragraph2 = new Paragraph();
			paragraph2.Add(new Chunk(ResDocuments.LibelleNom + " : ", GeneriqueDocument.FontLabel));
			paragraph2.Add(new Chunk(base.Donnees.NomAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph3 = new Paragraph();
			paragraph3.Add(new Chunk(ResDocuments.LibellePrenom + " : ", GeneriqueDocument.FontLabel));
			paragraph3.Add(new Chunk(base.Donnees.PrenomAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph4 = new Paragraph();
			paragraph4.Add(new Chunk(ResDocuments.LibelleDateNaissance + " : ", GeneriqueDocument.FontLabel));
			paragraph4.Add(new Chunk(base.Donnees.DateNaissanceAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph5 = new Paragraph();
			paragraph5.Add(new Chunk(ResDocuments.LibelleAdresse + " : ", GeneriqueDocument.FontLabel));
			paragraph5.Add(new Chunk(base.Donnees.AdresseAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph6 = new Paragraph();
			paragraph6.Add(new Chunk(ResDocuments.LibelleCodePostal + " : ", GeneriqueDocument.FontLabel));
			paragraph6.Add(new Chunk(base.Donnees.CodePostalAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph7 = new Paragraph();
			paragraph7.Add(new Chunk(ResDocuments.LibelleVille + " : ", GeneriqueDocument.FontLabel));
			paragraph7.Add(new Chunk(base.Donnees.VilleAdherent, GeneriqueDocument.FontValue));
			Paragraph paragraph8 = new Paragraph();
			paragraph8.Add(new Chunk(ResDocuments.LibelleTelephone1 + " : ", GeneriqueDocument.FontLabel));
			paragraph8.Add(new Chunk(base.Donnees.Telephone1Adherent, GeneriqueDocument.FontValue));
			Paragraph paragraph9 = new Paragraph();
			paragraph9.Add(new Chunk(ResDocuments.LibelleTelephone2 + " : ", GeneriqueDocument.FontLabel));
			paragraph9.Add(new Chunk(base.Donnees.Telephone2Adherent, GeneriqueDocument.FontValue));
			Paragraph paragraph10 = new Paragraph();
			paragraph10.Add(new Chunk(ResDocuments.LibelleTelephone3 + " : ", GeneriqueDocument.FontLabel));
			paragraph10.Add(new Chunk(base.Donnees.Telephone3Adherent, GeneriqueDocument.FontValue));
			Paragraph paragraph11 = new Paragraph();
			paragraph11.Add(new Chunk(ResDocuments.LibelleMail1 + " : ", GeneriqueDocument.FontLabel));
			paragraph11.Add(new Chunk(base.Donnees.Mail1Adherent, GeneriqueDocument.FontValue));
			Paragraph paragraph12 = new Paragraph();
			paragraph12.Add(new Chunk(ResDocuments.LibelleMail2 + " : ", GeneriqueDocument.FontLabel));
			paragraph12.Add(new Chunk(base.Donnees.Mail2Adherent, GeneriqueDocument.FontValue));
			Paragraph paragraph13 = new Paragraph();
			paragraph13.Add(new Chunk(ResDocuments.LibelleMail3 + " : ", GeneriqueDocument.FontLabel));
			paragraph13.Add(new Chunk(base.Donnees.Mail3Adherent, GeneriqueDocument.FontValue));
			Table table = new Table(2, 6)
			{
				Locked = false,
				Width = 95f,
				Border = 0
			};
			Cell aCell = new Cell(element)
			{
				Colspan = 2,
				Border = 0
			};
			table.AddCell(aCell, 0, 0);
			Cell cell2 = new Cell(paragraph2)
			{
				Border = 0
			};
			table.AddCell(cell2, 1, 0);
			Cell cell3 = new Cell(paragraph3)
			{
				Border = 0
			};
			table.AddCell(cell3, 1, 1);
			Cell cell4 = new Cell(paragraph4)
			{
				Colspan = 2,
				Border = 0
			};
			table.AddCell(cell4, 2, 0);
			Cell cell5 = new Cell(paragraph5)
			{
				Colspan = 2,
				Border = 0
			};
			table.AddCell(cell5, 3, 0);
			Cell cell6 = new Cell(paragraph6)
			{
				Border = 0
			};
			table.AddCell(cell6, 4, 0);
			Cell cell7 = new Cell(paragraph7)
			{
				Border = 0
			};
			table.AddCell(cell7, 4, 1);
			Cell cell8 = new Cell
			{
				Border = 0
			};
			cell8.AddElement(paragraph8);
			cell8.AddElement(paragraph9);
			cell8.AddElement(paragraph10);
			table.AddCell(cell8, 5, 0);
			Cell cell9 = new Cell
			{
				Border = 0
			};
			cell9.AddElement(paragraph11);
			cell9.AddElement(paragraph12);
			cell9.AddElement(paragraph13);
			table.AddCell(cell9, 5, 1);
			base.ContenuDocument.AddCell(new Cell(table));
		}

		private void CreerZoneInfosInscription() {
			Paragraph element = new Paragraph();
			element.Add(new Chunk(ResDocuments.LibelleCotisation, GeneriqueDocument.FontLabel));
			element.Add(new Chunk(base.Donnees.CotisationInscription + ResDocuments.Devise, GeneriqueDocument.FontValue));
			Paragraph paragraph2 = new Paragraph();
			paragraph2.Add(new Chunk(ResDocuments.LibelleGroupe + " : ", GeneriqueDocument.FontLabel));
			paragraph2.Add(new Chunk(base.Donnees.GroupeInscription, GeneriqueDocument.FontValue));
			Table table = new Table(2, 1)
			{
				Locked = false,
				Width = 95f,
				Border = 0
			};
			Cell aCell = new Cell
			{
				Border = 0
			};
			aCell.AddElement(element);
			table.AddCell(aCell, 0, 0);
			Cell cell2 = new Cell
			{
				Border = 0
			};
			cell2.AddElement(paragraph2);
			table.AddCell(cell2, 0, 1);
			base.ContenuDocument.AddCell(new Cell(table));
		}

		private void CreerZoneSignature() {
			Paragraph element = new Paragraph();
			element.Add(new Chunk(ResDocuments.LibelleSignatureAdherent, GeneriqueDocument.FontLabel));
			Table table = new Table(1, 1)
			{
				Locked = false,
				Width = 95f,
				Border = 0
			};
			Cell aCell = new Cell(element)
			{
				Border = 0
			};
			table.AddCell(aCell, 0, 0);
			base.ContenuDocument.AddCell(new Cell(table));
		}

		public Table GenererContenuDocument() {
			base.ContenuDocument.Locked = false;
			base.ContenuDocument.Width = 100f;
			base.ContenuDocument.Border = 0;
			base.CreerEntete();
			base.CreerSeparation();
			this.CreerZoneCoordonneesAdherent();
			base.CreerSeparation();
			this.CreerZoneInfosInscription();
			base.CreerSeparation();
			this.CreerZoneSignature();
			base.CreerSeparation();
			this.CreerZoneAutorisation();
			return base.ContenuDocument;
		}
	}
}
