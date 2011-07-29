using System.Collections.Generic;
using System.Linq;
using gestadh45.model;

namespace gestadh45.service.Graphs
{
	public static class GenerateurGraph
	{
		/// <summary>
		/// Créé le graph de remplissage des groupes
		/// </summary>
		/// <param name="pDonnees">Données</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRemplissageGroupe(DonneesGraph pDonnees) {
		    Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RemplissageGroupes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();
					    
			foreach(Groupe lGroupe in pDonnees.GroupesSaisonCourante) {
				var q = from Inscription i in pDonnees.InscriptionsSaisonCourante
						where i.Groupe.Id == lGroupe.Id
						select i;

				lGraph.Donnees.Add(lGroupe.Libelle, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par sexe
		/// </summary>
		/// <param name="pDonnees">Données</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionSexe(DonneesGraph pDonnees) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionSexes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			foreach (Sexe lSexe in pDonnees.Sexes) {
				var q = from Inscription i in pDonnees.InscriptionsSaisonCourante
						where i.Adherent.Sexe.Id == lSexe.Id
						select i;

				lGraph.Donnees.Add(lSexe.LibelleCourt, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par âge
		/// </summary>
		/// <param name="pDonnees">Données</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionAge(DonneesGraph pDonnees) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionAges;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			var qAge = from Inscription i in pDonnees.InscriptionsSaisonCourante
					   orderby i.Adherent.Age ascending
					   select i.Adherent.Age;

			List<int> lAges = qAge.Distinct().ToList();

			foreach (int lAge in lAges) {
				var qNb = from Inscription i in pDonnees.InscriptionsSaisonCourante
						  where i.Adherent.Age == lAge
						  select i;

				lGraph.Donnees.Add(lAge.ToString(), qNb.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents majeurs / mineurs
		/// </summary>
		/// <param name="pDonnees">Données</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionMajeursMineurs(DonneesGraph pDonnees) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionMajeursMineurs;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			// Nombre de majeurs
			var qMajeurs = from Inscription i in pDonnees.InscriptionsSaisonCourante
						   where i.Adherent.Age >= 18
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Majeurs, qMajeurs.LongCount());

			// Nombre de mineurs
			var qMineurs = from Inscription i in pDonnees.InscriptionsSaisonCourante
						   where i.Adherent.Age < 18
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Mineurs, qMineurs.LongCount());

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents résidents / extérieurs
		/// </summary>
		/// <param name="pDonnees">Donnéess</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionResidentsExterieurs(DonneesGraph pDonnees) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionResidentsExterieurs;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			// Nombre de résidents
			var qResidents = from Inscription i in pDonnees.InscriptionsSaisonCourante
						   where i.Adherent.Adresse.Ville.Id == pDonnees.InfosClub.Adresse.Ville.Id
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Residents, qResidents.LongCount());

			// Nombre d'extérieurs
			var qExterieurs = from Inscription i in pDonnees.InscriptionsSaisonCourante
							 where i.Adherent.Adresse.Ville.Id != pDonnees.InfosClub.Adresse.Ville.Id
							 select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Exterieurs, qExterieurs.LongCount());

			return lGraph;
		}
	}
}
