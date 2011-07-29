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

		private IVilleDao _daoVille;
		private IInfosClubDao _daoInfosClub;

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

		public FormulaireInfosClubUCViewModel() {
			this._daoVille = DaoFactory.GetVilleDao(ViewModelLocator.DataSource);
			this._daoInfosClub = DaoFactory.GetInfosClubDao(ViewModelLocator.DataSource);

			this.InfosClub = this._daoInfosClub.Read();
			this.InitialisationFormulaire();
			
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this, 
				(msg) => this.SelectionnerVille(msg.Content)
			);
		}

		public override void ExecuteEnregistrerCommand() {
			var v = this._daoVille.Read(this.InfosClub.Adresse.Ville.Id);
			if (v != null) {
				this.InfosClub.Adresse.Ville = v;
			}

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
		}

		private void SelectionnerVille(Ville pVille) {
			this.InitialisationFormulaire();
			this.InfosClub.Adresse.Ville = pVille;
			this.RaisePropertyChanged(() => this.InfosClub);
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				lErreurs.Add(ResErreurs.InfosClub_NomObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.InfosClub.Adresse.Libelle)) {
				lErreurs.Add(ResErreurs.InfosClub_AdresseObligatoire);
			}

			if (this.InfosClub.Adresse.Ville == null 
				|| string.IsNullOrWhiteSpace(this.InfosClub.Adresse.Ville.CodePostal) 
				|| string.IsNullOrWhiteSpace(this.InfosClub.Adresse.Ville.Libelle)) {
				lErreurs.Add(ResErreurs.InfosClub_VilleObligatoire);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
