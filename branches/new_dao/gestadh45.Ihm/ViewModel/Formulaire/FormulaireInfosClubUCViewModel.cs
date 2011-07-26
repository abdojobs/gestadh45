using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInfosClubUCViewModel : ViewModelBaseFormulaire
	{
		private InfosClub _infosClub;
		private ICollectionView _villes;
		private int _idVilleSelectionnee;

		private VilleDao _daoVille;
		private InfosClubDao _daoInfosClub;

		/// <summary>
		/// Obtient/Définit l'objet Infos club du formulaire
		/// </summary>
		public InfosClub InfosClub {
			get {
				return this._infosClub;
			}
			set {
				if (this._infosClub != value) {
					this._infosClub = value;
					this.RaisePropertyChanged(() => this.InfosClub);

					// maj de id ville selectionnee
					this.IdVilleSelectionnee = value.Adresse.Ville.Id;
					this.RaisePropertyChanged(() => this.IdVilleSelectionnee);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public ICollectionView Villes {
			get {
				return this._villes;
			}
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit l'id de la ville sélectionnée
		/// </summary>
		public int IdVilleSelectionnee {
			get {
				return this._idVilleSelectionnee;
			}
			set {
				if (this._idVilleSelectionnee != value) {
					this._idVilleSelectionnee = value;
					this.RaisePropertyChanged(() => this.IdVilleSelectionnee);

					// maj de InfosClub
					this.InfosClub.Adresse.Ville = this._daoVille.Read(value);
					this.RaisePropertyChanged(() => this.InfosClub);
				}
			}
		}

		public FormulaireInfosClubUCViewModel() {
			this._daoVille = new VilleDao(ViewModelLocator.DataSource);
			this._daoInfosClub = new InfosClubDao(ViewModelLocator.DataSource);

			this.InitialisationFormulaire();
			
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this, 
				(msg) => this.SelectionnerVille(msg.Content)
			);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				this._daoInfosClub.Update(this.InfosClub);
				base.ExecuteEnregistrerCommand();
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		private void InitialisationFormulaire() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoVille.List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
			this.InfosClub = this._daoInfosClub.Read(0);
		}

		private void SelectionnerVille(Ville pVille) {
			this.InitialisationFormulaire();
			this.IdVilleSelectionnee = pVille.Id;
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				lErreurs.Add(ResErreurs.InfosClub_NomObligatoire);
			}

			if (this.InfosClub.Adresse == null || string.IsNullOrWhiteSpace(this.InfosClub.Adresse.Libelle)) {
				lErreurs.Add(ResErreurs.InfosClub_AdresseObligatoire);
			}

			if (this.InfosClub.Adresse != null && this.InfosClub.Adresse.Ville == null) {
				lErreurs.Add(ResErreurs.InfosClub_VilleObligatoire);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
