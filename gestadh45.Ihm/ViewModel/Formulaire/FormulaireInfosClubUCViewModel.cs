using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInfosClubUCViewModel : ViewModelBaseFormulaire
	{
		private InfosClub mInfosClub;
		private ICollectionView mVilles;

		private VilleDao _daoVille;
		private InfosClubDao _daoInfosClub;

		/// <summary>
		/// Obtient/Définit l'objet Infos club du formulaire
		/// </summary>
		public InfosClub InfosClub {
			get {
				return this.mInfosClub;
			}
			set {
				if (this.mInfosClub != value) {
					this.mInfosClub = value;
					this.RaisePropertyChanged(() => this.InfosClub);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public ICollectionView Villes {
			get {
				return this.mVilles;
			}
			set {
				if (this.mVilles != value) {
					this.mVilles = value;
					this.RaisePropertyChanged(() => this.Villes);
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

			var rq = from Ville v in this.Villes.SourceCollection
					 where v.Id == this.InfosClub.Adresse.Ville.Id
					 select v;

			if (rq.Count() > 0) {
				this.InfosClub.Adresse.Ville = rq.First();
				this.RaisePropertyChanged(() => this.InfosClub);
			}
		}

		private void SelectionnerVille(Ville pVille) {
			this.InitialisationFormulaire();
			
			var rq = from Ville v in this.Villes.SourceCollection
					 where v.Id == pVille.Id
					 select v;

			if (rq.Count() > 0) {
				this.InfosClub.Adresse.Ville = rq.First();
				this.RaisePropertyChanged(() => this.InfosClub);
			}
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
