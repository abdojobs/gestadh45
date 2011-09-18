using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using gestadh45.dal;
using gestadh45.dao;
using gestadh45.Ihm.ViewModel.Tools.Effectif;

namespace gestadh45.Ihm.ViewModel.Tools
{
	public class RepartitionEffectifUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
		private ICollectionView _tranchesEffectif;

		private IInscriptionDao _daoInscription;
		private IInfosClubDao _daoInfosClub;

		private IEnumerable _inscriptionsSaisonCourante;
		private Ville _villeResident;
		#endregion

		#region properties
		/// <summary>
		/// Obtient/Définit la liste des tranches d'effectif
		/// </summary>
		public ICollectionView TranchesEffectif {
			get {
				return this._tranchesEffectif;
			}
			set {
				if (this._tranchesEffectif != value) {
					this._tranchesEffectif = value;
					this.RaisePropertyChanged(() => this.TranchesEffectif);
				}
			}
		}
		#endregion

		#region Constructor
		public RepartitionEffectifUCViewModel() {
			this._daoInscription = this.mDaoFactory.GetInscriptionDao();
			this._daoInfosClub = this.mDaoFactory.GetInfosClubDao();

			this._inscriptionsSaisonCourante = this._daoInscription.ListSaisonCourante();
			this._villeResident = this._daoInfosClub.Read().Ville;

			this.InitialisationTranchesEffectif();
		}
		#endregion

		#region private methods
		private void InitialisationTranchesEffectif() {
			var tranches = new List<TrancheEffectif>();

			// Création et alimentation des tranches
			tranches.Add(this.CreerTrancheEffectif(2, 12, true));
			tranches.Add(this.CreerTrancheEffectif(2, 12, false));
			tranches.Add(this.CreerTrancheEffectif(13, 16, true));
			tranches.Add(this.CreerTrancheEffectif(13, 16, false));
			tranches.Add(this.CreerTrancheEffectif(17, 25, true));
			tranches.Add(this.CreerTrancheEffectif(17, 25, false));
			tranches.Add(this.CreerTrancheEffectif(26, 59, true));
			tranches.Add(this.CreerTrancheEffectif(26, 59, false));
			tranches.Add(this.CreerTrancheEffectif(60, 999, true));
			tranches.Add(this.CreerTrancheEffectif(60, 999, false));

			ICollectionView defaultView = CollectionViewSource.GetDefaultView(tranches);
			defaultView.SortDescriptions.Add(new SortDescription("AgeInferieur", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("EstResident", ListSortDirection.Descending));
			this.TranchesEffectif = defaultView;
		}

		private TrancheEffectif CreerTrancheEffectif(int pAgeMini, int pAgeMaxi, bool pEstResident) {
			var tranche = new TrancheEffectif()
			{
				AgeInferieur = pAgeMini,
				AgeSuperieur = pAgeMaxi,
				EstResident = pEstResident
			};

			var rqEffectif = from Inscription ins in this._inscriptionsSaisonCourante
							 where ins.Adherent.Age >= pAgeMini 
							 && ins.Adherent.Age <= pAgeMaxi 
							 && ((pEstResident)?(ins.Adherent.ID_Ville == this._villeResident.ID):(ins.Adherent.ID_Ville != this._villeResident.ID))
							 select ins;


			tranche.Effectif = rqEffectif.Count();

			return tranche;
		}
		#endregion
	}
}
