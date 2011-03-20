using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.service.Graphs;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class GraphsSaisonCouranteUCViewModel : ViewModelBaseConsultation
	{
		private List<StructCodesGraphs> mListeGraphs;
		private Graphique mGraphique;

		public Graphique Graphique {
			get {
				return this.mGraphique;
			}

			set {
				if(this.mGraphique != value) {
					this.mGraphique = value;
					RaisePropertyChanged("Graphique");
				}
			}
		}

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
			switch (pCodeGraph.code) {
				case CodesGraphs.RemplissageGroupes:
					this.Graphique = GenerateurGraph.CreerGraphRemplissageGroupe(ViewModelLocator.Context);
					break;

				case CodesGraphs.RepartitionSexes:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionSexe(ViewModelLocator.Context);
					break;

				case CodesGraphs.RepartitionAges:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionAge(ViewModelLocator.Context);
					break;

				case CodesGraphs.RepartitionMajeursMineurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionMajeursMineurs(ViewModelLocator.Context);
					break;

				case CodesGraphs.RepartitionResidentsExterieurs:
					this.Graphique = GenerateurGraph.CreerGraphRepartitionResidentsExterieurs(ViewModelLocator.Context);
					break;
			}
		}

		private void initialisationListeGraphs()
		{
			this.ListeGraphs.Add(new StructCodesGraphs
			{
				code = CodesGraphs.RemplissageGroupes,
				libelle = ResGraphs.Titre_RemplissageGroupes
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				code = CodesGraphs.RepartitionSexes,
				libelle = ResGraphs.Titre_RepartitionSexes
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				code = CodesGraphs.RepartitionMajeursMineurs,
				libelle = ResGraphs.Titre_RepartitionMajeursMineurs
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				code = CodesGraphs.RepartitionResidentsExterieurs,
				libelle = ResGraphs.Titre_RepartitionResidentsExterieurs
			});

			this.ListeGraphs.Add(new StructCodesGraphs
			{
				code = CodesGraphs.RepartitionAges,
				libelle = ResGraphs.Titre_RepartitionAges
			});
		}

		public ICommand AfficherGraphCommand { get; set; }

		public List<StructCodesGraphs> ListeGraphs
		{
			get
			{
				return this.mListeGraphs;
			}
			set
			{
				if (this.mListeGraphs != value)
				{
					this.mListeGraphs = value;
					this.RaisePropertyChanged("ListeGraphs");
				}
			}
		}
	}
}
