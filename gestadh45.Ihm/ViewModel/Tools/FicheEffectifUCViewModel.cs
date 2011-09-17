using System.ComponentModel;
using System.Windows.Data;
using gestadh45.dal;
using gestadh45.dao;

namespace gestadh45.Ihm.ViewModel.Tools
{
	public class FicheEffectifUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
		private ICollectionView _inscriptions;
		private ICollectionView _groupes;
		private Groupe _groupeFiltre;
		private bool _filtreActif;

		private IInscriptionDao _daoInscription;
		private IGroupeDao _daoGroupe;
		#endregion

		#region properties
		/// <summary>
		/// Obtient/Définit la liste des inscriptions à afficher
		/// </summary>
		public ICollectionView Inscriptions {
			get {
				return this._inscriptions;
			}
			set {
				if (this._inscriptions != value)
				{
					this._inscriptions = value;
					this.RaisePropertyChanged(() => this.Inscriptions);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des groupes
		/// </summary>
		public ICollectionView Groupes {
			get {
				return this._groupes;
			}
			set {
				if (this._groupes != value)
				{
					this._groupes = value;
					this.RaisePropertyChanged(() => this.Groupes);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit le groupe de filtre
		/// </summary>
		public Groupe GroupeFiltre {
			get {
				return this._groupeFiltre;
			}
			set {
				if (this._groupeFiltre != value)
				{
					this._groupeFiltre = value;
					this.RaisePropertyChanged(() => this.GroupeFiltre);
					this.FiltrerListeInscriptions();
				}
			}
		}

		/// <summary>
		/// Obtient/Définit un booléen indiquand si le filtre est actif
		/// </summary>
		public bool FiltreActif {
			get {
				return this._filtreActif;
			}
			set {
				if (this._filtreActif != value)
				{
					this._filtreActif = value;
					this.RaisePropertyChanged(() => this.FiltreActif);
					this.FiltrerListeInscriptions();
				}
			}
		}
		#endregion

		#region constructor
		public FicheEffectifUCViewModel() {
			this.CodeUCOrigine = CodesUC.ConsultationInscriptions;

			this._daoInscription = this.mDaoFactory.GetInscriptionDao();
			this._daoGroupe = this.mDaoFactory.GetGroupeDao();

			this.InitialisationListeInscriptions();
			this.InitialisationListeGroupes();
		}
		#endregion

		#region private methods
		private void InitialisationListeInscriptions() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoInscription.ListSaisonCourante());
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Adherent.Prenom", ListSortDirection.Ascending));
			this.Inscriptions = defaultView;
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoGroupe.ListSaisonCourante());
			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.Groupes = defaultView;
		}

		private void FiltrerListeInscriptions() {
			this.Inscriptions.Filter = null;
			
			if(this.FiltreActif && this.GroupeFiltre != null) {
				this.Inscriptions.Filter = (item) => ((Inscription)item).Groupe.ID == this.GroupeFiltre.ID;
			}
		}
		#endregion
	}
}
