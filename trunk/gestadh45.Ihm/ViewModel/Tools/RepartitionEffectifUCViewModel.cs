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
		private ITrancheAgeDao _daoTrancheAge;

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
			this._daoTrancheAge = this.mDaoFactory.GetTrancheAgeDao();

			this._inscriptionsSaisonCourante = this._daoInscription.ListSaisonCourante();
			this._villeResident = this._daoInfosClub.Read().Ville;

			this.InitialisationTranchesEffectif();
		}
		#endregion

		#region private methods
		private void InitialisationTranchesEffectif() {
			var tranches = new List<TrancheEffectif>();

			foreach (TrancheAge tranche in this._daoTrancheAge.List()) {
				tranches.Add(this.CreerTrancheEffectif(tranche));
			}

			ICollectionView defaultView = CollectionViewSource.GetDefaultView(tranches);
			defaultView.SortDescriptions.Add(new SortDescription("AgeInferieur", ListSortDirection.Ascending));
			this.TranchesEffectif = defaultView;
		}

		private TrancheEffectif CreerTrancheEffectif(TrancheAge trancheAge) {
			var tranche = new TrancheEffectif()
			{
				AgeInferieur = (int)trancheAge.AgeInf,
				AgeSuperieur = (int)trancheAge.AgeSup
			};

			var rqEffectifResidents = from Inscription ins in this._inscriptionsSaisonCourante
									  where ins.Adherent.Age >= trancheAge.AgeInf
											&& ins.Adherent.Age <= trancheAge.AgeSup 
											&& ins.Adherent.ID_Ville == this._villeResident.ID
									 select ins;

			tranche.EffectifResidents = rqEffectifResidents.Count();

			var rqEffectifExterieur = from Inscription ins in this._inscriptionsSaisonCourante
									  where ins.Adherent.Age >= trancheAge.AgeInf
											&& ins.Adherent.Age <= trancheAge.AgeSup
											&& ins.Adherent.ID_Ville != this._villeResident.ID
									  select ins;

			tranche.EffectifExterieurs = rqEffectifExterieur.Count();

			return tranche;
		}
		#endregion
	}
}
