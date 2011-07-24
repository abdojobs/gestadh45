using System.Collections.Generic;
using System.Linq;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.service.Graphs
{
	public static class GenerateurGraph
	{
		/// <summary>
		/// Créé le graph de remplissage des groupes
		/// </summary>
		/// <param name="pDataSource">DataSource</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRemplissageGroupe(string pDataSource) {
			InscriptionDao lDaoInscription = new InscriptionDao(pDataSource);
			GroupeDao lDaoGroupe = new GroupeDao(pDataSource);

		    Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RemplissageGroupes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();
			
		    List<Groupe> lGroupes = lDaoGroupe.ListSaisonCourante();
			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();
		    
			foreach(Groupe lGroupe in lGroupes) {
				var q = from Inscription i in lInscriptions
						where i.Groupe.Id == lGroupe.Id
						select i;

				lGraph.Donnees.Add(lGroupe.Libelle, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par sexe
		/// </summary>
		/// <param name="pDataSource">DataSource</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionSexe(string pDataSource) {
			SexeDao lDaoSexe = new SexeDao(pDataSource);
			InscriptionDao lDaoInscription = new InscriptionDao(pDataSource);
			
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionSexes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Sexe> lSexes = lDaoSexe.List();
			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();

			foreach (Sexe lSexe in lSexes) {
				var q = from Inscription i in lInscriptions
						where i.Adherent.Sexe.Id == lSexe.Id
						select i;

				lGraph.Donnees.Add(lSexe.LibelleCourt, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par âge
		/// </summary>
		/// <param name="pDataSource">DataSource</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionAge(string pDataSource) {
			InscriptionDao lDaoInscription = new InscriptionDao(pDataSource);

			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionAges;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();

			var qAge = from Inscription i in lInscriptions
					   orderby i.Adherent.Age ascending
					   select i.Adherent.Age;

			List<int> lAges = qAge.Distinct().ToList();

			foreach (int lAge in lAges) {
				var qNb = from Inscription i in lInscriptions
						  where i.Adherent.Age == lAge
						  select i;

				lGraph.Donnees.Add(lAge.ToString(), qNb.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents majeurs / mineurs
		/// </summary>
		/// <param name="pDataSource">DataSource</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionMajeursMineurs(string pDataSource) {
			InscriptionDao lDaoInscription = new InscriptionDao(pDataSource);
			
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionMajeursMineurs;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();

			// Nombre de majeurs
			var qMajeurs = from Inscription i in lInscriptions
						   where i.Adherent.Age >= 18
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Majeurs, qMajeurs.LongCount());

			// Nombre de mineurs
			var qMineurs = from Inscription i in lInscriptions
						   where i.Adherent.Age < 18
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Mineurs, qMineurs.LongCount());

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents résidents / extérieurs
		/// </summary>
		/// <param name="pDataSource">DataSource</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionResidentsExterieurs(string pDataSource) {
			InscriptionDao lDaoInscription = new InscriptionDao(pDataSource);
			InfosClubDao lDaoInfosClub = new InfosClubDao(pDataSource);
			
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionResidentsExterieurs;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();
			InfosClub lInfosClub = lDaoInfosClub.Read(0);

			// Nombre de résidents
			var qResidents = from Inscription i in lInscriptions
						   where i.Adherent.Adresse.Ville.Id == lInfosClub.Adresse.Ville.Id
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Residents, qResidents.LongCount());

			// Nombre d'extérieurs
			var qExterieurs = from Inscription i in lInscriptions
							 where i.Adherent.Adresse.Ville.Id != lInfosClub.Adresse.Ville.Id
							 select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Exterieurs, qExterieurs.LongCount());

			return lGraph;
		}
	}
}
