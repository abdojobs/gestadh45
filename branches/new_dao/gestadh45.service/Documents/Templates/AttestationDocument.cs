using System;
using iTextSharp.text;

namespace gestadh45.service.Documents.Templates
{
	public class AttestationDocument : GeneriqueDocument
	{
		public AttestationDocument(DonneesDocument pDonnees)
			: base(pDonnees) {
		}

		private void CreerZoneCoordonneesAdherent() {
			Paragraph element = new Paragraph(
				ResDocuments.LibelleRecuPourLAdhesion + " " 
				+ base.Donnees.Saison + " " 
				+ ResDocuments.LibelleAuClub + " " 
				+ base.Donnees.NomClub, 
				GeneriqueDocument.FontTitre)
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
			base.ContenuDocument.AddCell(new Cell(table));
		}

		private void CreerZoneInfosAttestation() {
			Paragraph element = new Paragraph();
			element.Add(new Chunk(ResDocuments.LibelleCotisation + " : ", GeneriqueDocument.FontLabel));
			element.Add(new Chunk(base.Donnees.CotisationInscription + ResDocuments.Devise, GeneriqueDocument.FontValue));
			Table table = new Table(1, 1)
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
			base.ContenuDocument.AddCell(new Cell(table));
		}

		private void CreerZoneLieuDate() {
			Paragraph element = new Paragraph
			{
				Alignment = 2
			};
			element.Add(new Chunk(ResDocuments.LibelleFaitA + " ", GeneriqueDocument.FontLabel));
			element.Add(new Chunk(base.Donnees.VilleClub + " ", GeneriqueDocument.FontLabel));
			element.Add(new Chunk(ResDocuments.LibelleLe + " ", GeneriqueDocument.FontLabel));
			element.Add(new Chunk(DateTime.Now.ToShortDateString(), GeneriqueDocument.FontLabel));
			Table table = new Table(1, 1)
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
			aCell.SetHorizontalAlignment("right");
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
			this.CreerZoneInfosAttestation();
			this.CreerZoneLieuDate();
			return base.ContenuDocument;
		}
	}
}
