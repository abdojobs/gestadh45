using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.service.Graphs
{
	public class DonneesGraph
	{
		public InfosClub InfosClub { get; set; }
		public List<Inscription> InscriptionsSaisonCourante { get; set; }
		public List<Groupe> GroupesSaisonCourante { get; set; }
		public List<Sexe> Sexes { get; set; }
	}
}
