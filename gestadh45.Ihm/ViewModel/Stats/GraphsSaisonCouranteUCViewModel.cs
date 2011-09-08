using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.service.Graphs;

namespace gestadh45.Ihm.ViewModel.Stats
{
	public class GraphsSaisonCouranteUCViewModel : ViewModelBaseConsultation
	{
		private List<StructCodesGraphs> mListeGraphs;
		private Graphique mGraphique;

		/// <summary>
		/// Obtient/Définit le graphique à afficher
		/// </summary>
		public Graphique Graphique {
			get {
				return this.mGraphique;
			}

			set {
				if(this.mGraphique != value) {
					this.mGraphique = value;
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
				return this.mListeGraphs;
			}
			set {
				if (this.mListeGraphs != value) {
					this.mListeGraphs = value;
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
			this.initialisationListeGraphs();
			this.CreateAfficherGraphCommand();
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
					this.Graphique = GenerateurGraph.CreerGraphRemplissageGroupe();
					break;

				case CodesGraphs.RepartitionSexes:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionSexe();
					break;

				case CodesGraphs.RepartitionAges:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionAge();
					break;

				case CodesGraphs.RepartitionMajeursMineurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionMajeursMineurs();
					break;

				case CodesGraphs.RepartitionResidentsExterieurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionResidentsExterieurs();
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
