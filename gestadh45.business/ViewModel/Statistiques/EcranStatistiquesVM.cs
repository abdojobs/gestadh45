using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.business.IhmObjects;
using gestadh45.dal;
using System.Linq;

namespace gestadh45.business.ViewModel.Statistiques
{
	public class EcranStatistiquesVM : VMConsultationBase
	{
		#region ListeGraphs
		private IList<ChoixGraphIhm> _listeGraphs;

		/// <summary>
		/// Gets or sets the liste graphs.
		/// </summary>
		/// <value>
		/// The liste graphs.
		/// </value>
		public IList<ChoixGraphIhm> ListeGraphs {
			get {
				return this._listeGraphs;
			}

			set {
				if(this._listeGraphs != value) {
					this._listeGraphs = value;
					this.RaisePropertyChanged(()=>this.ListeGraphs);
				}
			}
		}
		#endregion

		#region ChartKeysValues
		private List<KeyValuePair<string, int>> _chartKeysValues;

		/// <summary>
		/// Gets or sets the chart keys values.
		/// </summary>
		/// <value>
		/// The chart keys values.
		/// </value>
		public List<KeyValuePair<string, int>> ChartKeysValues {
			get {
				return this._chartKeysValues;
			}

			set {
				if (this._chartKeysValues != value) {
					this._chartKeysValues = value;
					this.RaisePropertyChanged(() => this.ChartKeysValues);
				}
			}
		}
		#endregion

		#region TitreGraph
		private string _titreGraph;

		/// <summary>
		/// Gets or sets the titre graph.
		/// </summary>
		/// <value>
		/// The titre graph.
		/// </value>
		public string TitreGraph {
			get {
				return this._titreGraph;
			}

			set {
				if (this._titreGraph != value) {
					this._titreGraph = value;
					this.RaisePropertyChanged(() => this.TitreGraph);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Inscription> repoInscriptions;
		private Repository<Groupe> repoGroupes;
		private Repository<Sexe> repoSexes;
		private Repository<InfosClub> repoInfosClub;
		private Repository<Ville> _repoVilles;
		#endregion

		#region private fields
		private List<Groupe> _groupesSaisonCourante;
		private List<Inscription> _inscriptionsSaisonCourante;
		#endregion

		public EcranStatistiquesVM() {
			this.repoInscriptions = new Repository<Inscription>(this._context);
			this.repoGroupes = new Repository<Groupe>(this._context);
			this.repoSexes = new Repository<Sexe>(this._context);
			this.repoInfosClub = new Repository<InfosClub>(this._context);
			this._repoVilles = new Repository<Ville>(this._context);

			this._groupesSaisonCourante = this.repoGroupes.GetAll()
				.Where(grp => grp.Saison.EstSaisonCourante)
				.OrderBy(grp => grp.JourSemaine.Numero)
				.ThenBy(grp => grp.HeureDebut)
				.ToList();

			this._inscriptionsSaisonCourante = this.repoInscriptions.GetAll()
				.Where(ins => ins.Groupe.Saison.EstSaisonCourante)
				.ToList();

			this.PopulateListeGraphs();
			this.CreateChangeChartCommand();
		}

		#region ChangeChartCommand
		public ICommand ChangeChartCommand {
			get;
			set;
		}

		private void CreateChangeChartCommand() {
			this.ChangeChartCommand = new RelayCommand<ChoixGraphIhm>(
				this.ExecuteChangeChartCommand,
				this.CanExecuteChangeChartCommand
			);
		}

		public bool CanExecuteChangeChartCommand(ChoixGraphIhm choixGraph) {
			return true;
		}

		public void ExecuteChangeChartCommand(ChoixGraphIhm choixGraph) {
			this.TitreGraph = choixGraph.ToString();

			// HACK pour contourner le problème de refresh du graph
			this.ChartKeysValues = new List<KeyValuePair<string, int>>();

			switch (choixGraph.Code) {
				case CodesGraphs.RemplissageGroupes:
					this.ChartKeysValues = this.GetRemplissageGroupes();
					break;

				case CodesGraphs.RepartitionHommesFemmes:
					this.ChartKeysValues = this.GetRepartitionHommeFemmes();
					break;

				case CodesGraphs.RepartitionMajeursMineurs:
					this.ChartKeysValues = this.GetRepartitionMajeursMineurs();
					break;

				case CodesGraphs.RepartitionResidentsExterieurs:
					this.ChartKeysValues = this.GetRepartitionResidentsExterieurs();
					break;

				case CodesGraphs.RepartitionAdherentsVilles:
					this.ChartKeysValues = this.GetRepartitionAdherentsVilles();
					break;

				default:
					this.ChartKeysValues.Clear();
					this.ChartKeysValues = null;
					break;
			}
		}
		#endregion

		private void PopulateListeGraphs() {
			this.ListeGraphs = new List<ChoixGraphIhm>();
			this.ListeGraphs.Add(new ChoixGraphIhm(CodesGraphs.RemplissageGroupes));
			this.ListeGraphs.Add(new ChoixGraphIhm(CodesGraphs.RepartitionHommesFemmes));
			this.ListeGraphs.Add(new ChoixGraphIhm(CodesGraphs.RepartitionMajeursMineurs));
			this.ListeGraphs.Add(new ChoixGraphIhm(CodesGraphs.RepartitionResidentsExterieurs));
			this.ListeGraphs.Add(new ChoixGraphIhm(CodesGraphs.RepartitionAdherentsVilles));
		}

		#region Alimentation des graphs
		private List<KeyValuePair<string, int>> GetRemplissageGroupes() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			foreach (Groupe grp in this._groupesSaisonCourante) {
				int nb = this._inscriptionsSaisonCourante
					.Where(ins => ins.Groupe.ID == grp.ID)
					.Count();

				keyValues.Add(new KeyValuePair<string, int>(grp.ToString(), nb));
			}

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionHommeFemmes() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			foreach (Sexe sexe in this.repoSexes.GetAll()) {
				int nb = this._inscriptionsSaisonCourante
					.Where(ins => ins.Adherent.Sexe.ID == sexe.ID)
					.Count();

				keyValues.Add(new KeyValuePair<string, int>(sexe.LibelleCourt, nb));
			}

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionMajeursMineurs() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			int nbMineurs = this._inscriptionsSaisonCourante.Where(ins => ins.Adherent.Age < 18).Count();
			int nbMajeurs = this._inscriptionsSaisonCourante.Where(ins => ins.Adherent.Age >= 18).Count();

			keyValues.Add(new KeyValuePair<string, int>(RessourcesStats.LibelleMineurs, nbMineurs));
			keyValues.Add(new KeyValuePair<string, int>(RessourcesStats.LibelleMajeurs, nbMajeurs));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionResidentsExterieurs() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			Ville villeClub = this.repoInfosClub.GetFirst().Ville;

			int nbResidents = this._inscriptionsSaisonCourante.Where(ins => ins.Adherent.Ville.ID == villeClub.ID).Count();
			int nbExterieurs = this._inscriptionsSaisonCourante.Where(ins => ins.Adherent.Ville.ID != villeClub.ID).Count();

			keyValues.Add(new KeyValuePair<string, int>(RessourcesStats.LibelleResidents, nbResidents));
			keyValues.Add(new KeyValuePair<string, int>(RessourcesStats.LibelleExtérieurs, nbExterieurs));

			return keyValues;
		}

		private List<KeyValuePair<string, int>> GetRepartitionAdherentsVilles() {
			List<KeyValuePair<string, int>> keyValues = new List<KeyValuePair<string, int>>();

			foreach (Ville ville in this._repoVilles.GetAll()) {
				var nbAdh = this._inscriptionsSaisonCourante.Count(i => i.Adherent.Ville.ID == ville.ID);
				keyValues.Add(new KeyValuePair<string, int>(ville.Libelle, nbAdh));
			}

			return keyValues;
		}
		#endregion
	}
}
