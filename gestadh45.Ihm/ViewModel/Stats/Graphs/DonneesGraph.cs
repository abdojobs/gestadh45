using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.Ihm.ViewModel.Stats.Graphs
{
	public class DonneesGraph
	{
		public InfosClub InfosClub { get; set; }
		public IList<Inscription> InscriptionsSaisonCourante { get; set; }
		public IList<Groupe> GroupesSaisonCourante { get; set; }
		public IEnumerable<Sexe> Sexes { get; set; }
	}
}
