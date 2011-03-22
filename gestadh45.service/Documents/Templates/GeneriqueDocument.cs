using iTextSharp.text;
using iTextSharp.text.pdf.draw;

namespace gestadh45.service.Documents.Templates
{
	public abstract class GeneriqueDocument
	{
		protected const string AlignementCentre = "center";
		protected const string AlignementDroite = "right";
		protected const string AlignementGauche = "left";
		private static Font fontCoordonneesClub = FontFactory.GetFont("Times-Roman", 10f, 0);
		private static Font fontLabel = FontFactory.GetFont("Helvetica-Bold", 10f, 1);
		private static Font fontNomClubSaison = FontFactory.GetFont("Helvetica-Bold", 16f, 1);
		private static Font fontTitre = FontFactory.GetFont("Helvetica-Bold", 12f, 5);
		private static Font fontValue = FontFactory.GetFont("Helvetica", 10f, 0);
		private Table mContenuDocument;		
		protected const float SeparatorWidth = 0.5f;
		private DonneesDocument mDonnees;

		protected GeneriqueDocument(DonneesDocument pDonnees) {
			this.mContenuDocument = new Table(1, 4);
			this.mDonnees = pDonnees;
		}

		protected void CreerEntete() {
			Paragraph element = new Paragraph(this.mDonnees.NomClub, fontNomClubSaison)
			{
				Alignment = 0
			};
			Paragraph paragraph2 = new Paragraph(ResDocuments.LibelleSaison + " " + this.mDonnees.Saison, fontNomClubSaison)
			{
				Alignment = 2
			};
			Paragraph paragraph3 = new Paragraph(this.mDonnees.AdresseClub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph4 = new Paragraph(string.Format("{0} - {1}", this.mDonnees.CodePostalClub, this.mDonnees.VilleClub), fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph5 = new Paragraph(ResDocuments.PrefixeTelephone + " : " + this.mDonnees.TelephoneCLub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph6 = new Paragraph(ResDocuments.PrefixeMail + " : " + this.mDonnees.MailClub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph7 = new Paragraph(ResDocuments.PrefixeSiteWeb + " : " + this.mDonnees.SiteWebClub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph8 = new Paragraph(ResDocuments.PrefixeNumeroClub + " : " + this.mDonnees.NumeroClub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			Paragraph paragraph9 = new Paragraph(ResDocuments.PrefixeSiren + " : " + this.mDonnees.SirenClub, fontCoordonneesClub)
			{
				Alignment = 0
			};
			
			Table table = new Table(2, 2)
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
			cell2.SetHorizontalAlignment("right");
			table.AddCell(cell2, 0, 1);
			Cell cell3 = new Cell
			{
				Border = 0
			};
			cell3.AddElement(paragraph3);
			cell3.AddElement(paragraph4);
			cell3.AddElement(paragraph5);
			cell3.AddElement(paragraph6);
			cell3.AddElement(paragraph7);
			cell3.AddElement(paragraph8);
			table.AddCell(cell3, 1, 0);
			this.mContenuDocument.AddCell(new Cell(table));
		}

		protected void CreerSeparation() {
			LineSeparator element = new LineSeparator
			{
				LineWidth = 0.5f
			};
			Cell cell = new Cell(element)
			{
				Border = 0
			};
			this.mContenuDocument.AddCell(cell);
		}

		protected Table ContenuDocument {
			get {
				return this.mContenuDocument;
			}
		}

		protected static Font FontCoordonneesClub {
			get {
				return fontCoordonneesClub;
			}
		}

		protected static Font FontLabel {
			get {
				return fontLabel;
			}
		}

		protected static Font FontNomClubSaison {
			get {
				return fontNomClubSaison;
			}
		}

		protected static Font FontTitre {
			get {
				return fontTitre;
			}
		}

		protected static Font FontValue {
			get {
				return fontValue;
			}
		}

		protected DonneesDocument Donnees {
			get {
				return this.mDonnees;
			}
		}
	}
}
