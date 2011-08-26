using System.Collections.Generic;

namespace gestadh45.Ihm.ViewModel.Consultation.Stats
{
	public class Graphique
	{
		public string Titre { get; set; }
		public IDictionary<string, long> Donnees { get; set; }
		public string NomDonnees { get; set; }
	}
}
