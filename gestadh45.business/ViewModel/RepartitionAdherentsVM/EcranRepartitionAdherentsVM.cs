using System.Collections;
using System.Collections.Generic;
using System.Linq;
using gestadh45.business.IhmObjects;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.RepartitionAdherentsVM
{
	public class EcranRepartitionAdherentsVM : VMConsultationBase
	{
		#region TranchesEffectif
		private IList<TrancheEffectif> _tranchesEffectif;

		/// <summary>
		/// Obtient/Définit la liste des tranches d'effectif
		/// </summary>
		public IList<TrancheEffectif> TranchesEffectif {
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

		#region champs privés
		private IEnumerable _inscriptionsSaisonCourante;
		private Ville _villeResident;
		#endregion

		#region DAOs
		private Repository<Inscription> _daoInscriptions;
		private Repository<InfosClub> _daoInfosClub;
		private Repository<TrancheAge> _daoTranchesAge;
		#endregion

		#region Constructeur
		public EcranRepartitionAdherentsVM() {
			this.CreateDao();

			this._inscriptionsSaisonCourante =this._daoInscriptions.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante);
			this._villeResident = this._daoInfosClub.GetFirst().Ville;

			this.InitialisationTranchesEffectif();
		}
		#endregion

		private void CreateDao() {
			this._daoInscriptions = new Repository<Inscription>(this._context);
			this._daoInfosClub = new Repository<InfosClub>(this._context);
			this._daoTranchesAge = new Repository<TrancheAge>(this._context);
		}

		private void InitialisationTranchesEffectif() {
			this.TranchesEffectif = new List<TrancheEffectif>();

			foreach (TrancheAge tranche in this._daoTranchesAge.GetAll()) {
				this.TranchesEffectif.Add(this.CreerTrancheEffectif(tranche));
			}
		}

		private TrancheEffectif CreerTrancheEffectif(TrancheAge trancheAge) {
			var tranche = new TrancheEffectif()
			{
				AgeInferieur = (int)trancheAge.AgeInf,
				AgeSuperieur = (int)trancheAge.AgeSup
			};

			var rqEffectifResidentsH = from Inscription ins in this._inscriptionsSaisonCourante
									   where ins.Adherent.Age >= trancheAge.AgeInf
											 && ins.Adherent.Age <= trancheAge.AgeSup
											 && ins.Adherent.ID_Ville == this._villeResident.ID
											 && ins.Adherent.Sexe.LibelleCourt.Equals("M")
									   select ins;

			var rqEffectifResidentsF = from Inscription ins in this._inscriptionsSaisonCourante
									   where ins.Adherent.Age >= trancheAge.AgeInf
											 && ins.Adherent.Age <= trancheAge.AgeSup
											 && ins.Adherent.ID_Ville == this._villeResident.ID
											 && ins.Adherent.Sexe.LibelleCourt.Equals("F")
									   select ins;

			var rqEffectifExterieurH = from Inscription ins in this._inscriptionsSaisonCourante
									   where ins.Adherent.Age >= trancheAge.AgeInf
											 && ins.Adherent.Age <= trancheAge.AgeSup
											 && ins.Adherent.ID_Ville != this._villeResident.ID
											 && ins.Adherent.Sexe.LibelleCourt.Equals("M")
									   select ins;

			var rqEffectifExterieurF = from Inscription ins in this._inscriptionsSaisonCourante
									   where ins.Adherent.Age >= trancheAge.AgeInf
											 && ins.Adherent.Age <= trancheAge.AgeSup
											 && ins.Adherent.ID_Ville != this._villeResident.ID
											 && ins.Adherent.Sexe.LibelleCourt.Equals("F")
									   select ins;

			tranche.EffectifResidentsH = rqEffectifResidentsH.Count();
			tranche.EffectifResidentsF = rqEffectifResidentsF.Count();
			tranche.EffectifExterieursH = rqEffectifExterieurH.Count();
			tranche.EffectifExterieursF = rqEffectifExterieurF.Count();

			return tranche;
		}
	}
}
