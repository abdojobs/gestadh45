using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using gestadh45.dao;
using gestadh45.Model;
using Visifire.Charts;

namespace gestadh45.service.Graphs
{
	public static class GenerateurGraph
	{
		/// <summary>
		/// Créé le graph de remplissage des groupes
		/// </summary>
		/// <param name="pContexte">Contexte de l'application</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRemplissageGroupe(Entities pContexte) {
		    Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RemplissageGroupes;
			lGraph.Type = RenderAs.Column;
			lGraph.Donnees = new ObservableCollection<DonneeGraph>();
			
		    List<Groupe> lGroupes = GroupeDao.GetInstance(pContexte).ListSaisonCourante();
		    List<Inscription> lInscriptions = InscriptionDao.GetInstance(pContexte).ListSaisonCourante();
		    
			foreach(Groupe lGroupe in lGroupes) {
				DonneeGraph lDonnee = new DonneeGraph();
				lDonnee.Label = lGroupe.Libelle;

				var q = from Inscription i in lInscriptions
						where i.Groupe.ID == lGroupe.ID
						select i;

				lDonnee.YValue = q.Count();

				lGraph.Donnees.Add(lDonnee);
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par sexe
		/// </summary>
		/// <param name="pContexte">Contexte de l'application</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionSexe(Entities pContexte) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionSexes;
			lGraph.Type = RenderAs.Column;
			lGraph.Donnees = new ObservableCollection<DonneeGraph>();

			List<Sexe> lSexes = SexeDao.GetInstance(pContexte).List();
			List<Inscription> lInscriptions = InscriptionDao.GetInstance(pContexte).ListSaisonCourante();

			foreach (Sexe lSexe in lSexes) {
				DonneeGraph lDonnee = new DonneeGraph();
				lDonnee.Label = lSexe.LibelleCourt;

				var q = from Inscription i in lInscriptions
						where i.Adherent.Sexe.ID == lSexe.ID
						select i;

				lDonnee.YValue = q.Count();
				lGraph.Donnees.Add(lDonnee);
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents par âge
		/// </summary>
		/// <param name="pContexte">Contexte de l'application</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionAge(Entities pContexte) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionAges;
			lGraph.Type = RenderAs.Column;
			lGraph.Donnees = new ObservableCollection<DonneeGraph>();

			List<Inscription> lInscriptions = InscriptionDao.GetInstance(pContexte).ListSaisonCourante();

			var qAge = from Inscription i in lInscriptions
					   orderby i.Adherent.Age ascending
					   select i.Adherent.Age;

			List<int> lAges = qAge.Distinct().ToList();

			foreach (int lAge in lAges) {
				DonneeGraph lDonnee = new DonneeGraph();
				lDonnee.Label = lAge.ToString();

				var qNb = from Inscription i in lInscriptions
						  where i.Adherent.Age == lAge
						  select i;

				lDonnee.YValue = qNb.Count();

				lGraph.Donnees.Add(lDonnee);
			}

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents majeurs / mineurs
		/// </summary>
		/// <param name="pContexte">Contexte de l'application</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionMajeursMineurs(Entities pContexte) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionMajeursMineurs;
			lGraph.Type = RenderAs.Column;
			lGraph.Donnees = new ObservableCollection<DonneeGraph>();

			List<Inscription> lInscriptions = InscriptionDao.GetInstance(pContexte).ListSaisonCourante();

			// Nombre de majeurs
			DonneeGraph lDonneesMajeurs = new DonneeGraph();
			lDonneesMajeurs.Label = ResGraphs.Libelle_Majeurs;

			var qMajeurs = from Inscription i in lInscriptions
						   where i.Adherent.Age >= 18
						   select i;

			lDonneesMajeurs.YValue = qMajeurs.Count();
			lGraph.Donnees.Add(lDonneesMajeurs);

			// Nombre de mineurs
			DonneeGraph lDonneesMineurs = new DonneeGraph();
			lDonneesMineurs.Label = ResGraphs.Libelle_Mineurs;

			var qMineurs = from Inscription i in lInscriptions
						   where i.Adherent.Age < 18
						   select i;

			lDonneesMineurs.YValue = qMineurs.Count();
			lGraph.Donnees.Add(lDonneesMineurs);

			return lGraph;
		}

		/// <summary>
		/// Créé le graph de remplissage de répartition des adhérents résidents / extérieurs
		/// </summary>
		/// <param name="pContexte">Contexte de l'application</param>
		/// <returns>Objet Graph</returns>
		public static Graphique CreerGraphRepartitionResidentsExterieurs(Entities pContexte) {
			Graphique lGraph = new Graphique();
			lGraph.Titre = ResGraphs.Titre_RepartitionResidentsExterieurs;
			lGraph.Type = RenderAs.Column;
			lGraph.Donnees = new ObservableCollection<DonneeGraph>();

			List<Inscription> lInscriptions = InscriptionDao.GetInstance(pContexte).ListSaisonCourante();
			InfosClub lInfosClub = InfosClubDao.GetInstance(pContexte).Read();

			// Nombre de résidents
			DonneeGraph lDonneesResidents = new DonneeGraph();
			lDonneesResidents.Label = ResGraphs.Libelle_Residents;

			var qResidents = from Inscription i in lInscriptions
						   where i.Adherent.Adresse.ID_Ville == lInfosClub.Adresse.ID_Ville
						   select i;

			lDonneesResidents.YValue = qResidents.Count();
			lGraph.Donnees.Add(lDonneesResidents);

			// Nombre d'extérieurs
			DonneeGraph lDonneesExterieurs = new DonneeGraph();
			lDonneesExterieurs.Label = ResGraphs.Libelle_Exterieurs;

			var qExterieurs = from Inscription i in lInscriptions
							 where i.Adherent.Adresse.ID_Ville != lInfosClub.Adresse.ID_Ville
							 select i;

			lDonneesExterieurs.YValue = qExterieurs.Count();
			lGraph.Donnees.Add(lDonneesExterieurs);

			return lGraph;
		}
	}
}
