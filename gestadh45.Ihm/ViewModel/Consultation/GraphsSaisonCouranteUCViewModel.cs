using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.service.Graphs;

namespace gestadh45.Ihm.ViewModel.Consultation
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
					RaisePropertyChanged("Graphique");
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
					this.RaisePropertyChanged("ListeGraphs");
				}
			}
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

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.GraphsSaisonCourante)
			);
		}
	}
}
