using System.Collections.Generic;
using System.Linq;
using gestadh45.dao;
using gestadh45.dal;

namespace gestadh45.service.Graphs
{
	public static class GenerateurGraph
	{
		/// <summary>
		/// Créé le graph de remplissage des groupes
		/// </summary>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRemplissageGroupe() {
			IInscriptionDao lDaoInscription = new InscriptionDao();
			IGroupeDao lDaoGroupe = new GroupeDao();

		    Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RemplissageGroupes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();
			
		    List<Groupe> lGroupes = lDaoGroupe.ListSaisonCourante();
			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();
		    
			foreach(Groupe lGroupe in lGroupes) {
				var q = from Inscription i in lInscriptions
						where i.Groupe.ID == lGroupe.ID
						select i;

				lGraph.Donnees.Add(lGroupe.Libelle, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par sexe
		/// </summary>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionSexe() {
			ISexeDao lDaoSexe = new SexeDao();
			IInscriptionDao lDaoInscription = new InscriptionDao();
			
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionSexes;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Sexe> lSexes = lDaoSexe.List();
			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();

			foreach (Sexe lSexe in lSexes) {
				var q = from Inscription i in lInscriptions
						where i.Adherent.Sexe.ID == lSexe.ID
						select i;

				lGraph.Donnees.Add(lSexe.LibelleCourt, q.LongCount());
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par âge
		/// </summary>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionAge() {
			IInscriptionDao lDaoInscription = new InscriptionDao();

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
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionMajeursMineurs() {
			IInscriptionDao lDaoInscription = new InscriptionDao();
			
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
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionResidentsExterieurs() {
			IInscriptionDao lDaoInscription = new InscriptionDao();
			IInfosClubDao lDaoInfosClub = new InfosClubDao();
			
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionResidentsExterieurs;
			lGraph.NomDonnees = ResGraphs.Libelle_NbAdherents;
			lGraph.Donnees = new Dictionary<string, long>();

			List<Inscription> lInscriptions = lDaoInscription.ListSaisonCourante();
			InfosClub lInfosClub = lDaoInfosClub.Read();

			// Nombre de résidents
			var qResidents = from Inscription i in lInscriptions
						   where i.Adherent.ID_Ville == lInfosClub.ID_Ville
						   select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Residents, qResidents.LongCount());

			// Nombre d'extérieurs
			var qExterieurs = from Inscription i in lInscriptions
							 where i.Adherent.ID_Ville != lInfosClub.ID_Ville
							 select i;

			lGraph.Donnees.Add(ResGraphs.Libelle_Exterieurs, qExterieurs.LongCount());

			return lGraph;
		}
	}
}
