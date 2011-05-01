using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using gestadh45.dao;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInfosClubUCViewModel : ViewModelBaseFormulaire
	{
		private InfosClub mInfosClub;
		private ICollectionView mVilles;

		private IVilleDao mDaoVille;
		private IInfosClubDao mDaoInfosClub;

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
					this.RaisePropertyChanged("InfosClub");
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
					this.RaisePropertyChanged("Villes");
				}
			}
		}

		public FormulaireInfosClubUCViewModel() {
			this.mDaoVille = this.mDaoFactory.GetVilleDao();
			this.mDaoInfosClub = this.mDaoFactory.GetInfosClubDao();

			this.InitialisationListeVilles();

			this.InfosClub = this.mDaoInfosClub.Read();
			this.mDaoInfosClub.Refresh(this.InfosClub);
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;
			this.ErreursVisibles = false;
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.InfosClub != null) {
				this.mDaoInfosClub.Refresh(this.InfosClub);
			}

			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				this.mDaoInfosClub.Update(this.InfosClub);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.ErreursVisibles = true;
			}
		}

		public override void ExecuteFenetreCommand(string pCodeUC) {
			base.ExecuteFenetreCommand(pCodeUC);

			this.InitialisationListeVilles();
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoVille.List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				lErreurs.Add(ResErreurs.Groupe_LibelleObligatoire);
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
