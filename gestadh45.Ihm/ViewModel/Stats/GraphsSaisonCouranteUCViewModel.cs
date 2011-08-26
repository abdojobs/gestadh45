using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.dao;
using gestadh45.Ihm.ViewModel.Consultation.Stats.Graphs;
using gestadh45.Ihm.ViewModel.Stats.Graphs;

namespace gestadh45.Ihm.ViewModel.Stats
{
	public class GraphsSaisonCouranteUCViewModel : ViewModelBaseConsultation
	{
		private List<StructCodesGraphs> _listeGraphs;
		private Graphique _graphique;
		private DonneesGraph _donneesGraph;

		/// <summary>
		/// Obtient/Définit le graphique à afficher
		/// </summary>
		public Graphique Graphique {
			get {
				return this._graphique;
			}

			set {
				if(this._graphique != value) {
					this._graphique = value;
					this.RaisePropertyChanged(() => this.Graphique);
					this.RaisePropertyChanged(() => this.GraphVisible);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des graphs
		/// </summary>
		public List<StructCodesGraphs> ListeGraphs {
			get {
				return this._listeGraphs;
			}
			set {
				if (this._listeGraphs != value) {
					this._listeGraphs = value;
					this.RaisePropertyChanged(() => this.ListeGraphs);
				}
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si le conteneur de graph doit être visible
		/// </summary>
		public bool GraphVisible {
			get { return this.Graphique != null; }
		}

		public ICommand AfficherGraphCommand { get; set; }

		public GraphsSaisonCouranteUCViewModel()
		{
			IInfosClubDao daoInfosClub = DaoFactory.GetInfosClubDao(ViewModelLocator.DataSource);
			IInscriptionDao daoInscription = DaoFactory.GetInscriptionDao(ViewModelLocator.DataSource);
			IGroupeDao daoGroupe = DaoFactory.GetGroupeDao(ViewModelLocator.DataSource);
			ISexeDao daoSexe = DaoFactory.GetSexeDao(ViewModelLocator.DataSource);
			
			this.initialisationListeGraphs();
			this.CreateAfficherGraphCommand();

			// on récupère une fois pour toute les données du graph
			this._donneesGraph = new DonneesGraph()
			{
				InfosClub = daoInfosClub.Read(),
				GroupesSaisonCourante = daoGroupe.ListSaisonCourante(),
				InscriptionsSaisonCourante = daoInscription.ListSaisonCourante(),
				Sexes = daoSexe.List()
			};
		}

		private void CreateAfficherGraphCommand()
		{
			this.AfficherGraphCommand = new RelayCommand<StructCodesGraphs>(
				this.ExecuteAfficherGraphCommand
			);
		}

		public void ExecuteAfficherGraphCommand(StructCodesGraphs pCodeGraph)
		{
			switch (pCodeGraph.Code) {
				case CodesGraphs.RemplissageGroupes:
					this.Graphique = GenerateurGraph.CreerGraphRemplissageGroupe(this._donneesGraph);
					break;

				case CodesGraphs.RepartitionSexes:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionSexe(this._donneesGraph);
					break;

				case CodesGraphs.RepartitionAges:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionAge(this._donneesGraph);
					break;

				case CodesGraphs.RepartitionMajeursMineurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionMajeursMineurs(this._donneesGraph);
					break;

				case CodesGraphs.RepartitionResidentsExterieurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionResidentsExterieurs(this._donneesGraph);
					break;
			}
		}

		private void initialisationListeGraphs()
		{
			this.ListeGraphs = new List<StructCodesGraphs>();

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				Code = CodesGraphs.RemplissageGroupes,
				Libelle = ResGraphs.Titre_RemplissageGroupes
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				Code = CodesGraphs.RepartitionSexes,
				Libelle = ResGraphs.Titre_RepartitionSexes
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				Code = CodesGraphs.RepartitionMajeursMineurs,
				Libelle = ResGraphs.Titre_RepartitionMajeursMineurs
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				Code = CodesGraphs.RepartitionResidentsExterieurs,
				Libelle = ResGraphs.Titre_RepartitionResidentsExterieurs
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				Code = CodesGraphs.RepartitionAges,
				Libelle = ResGraphs.Titre_RepartitionAges
			});
		}
	}
}
