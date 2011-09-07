using System.Collections.Generic;

namespace gestadh45.service.Graphs
{
	public class Graphique
	{
		public string Titre { get; set; }
		public IDictionary<string, long> Donnees { get; set; }
		public string NomDonnees { get; set; }
	}
}
