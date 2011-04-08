using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireInfosClubUCViewModel : ViewModelBaseFormulaire
	{
		private InfosClub mInfosClub;
		private ICollectionView mVilles;

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
			this.InitialisationListeVilles();

			this.InfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();
			InfosClubDao.GetInstance(ViewModelLocator.Context).Refresh(this.InfosClub);
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.InfosClub != null) {
				InfosClubDao.GetInstance(ViewModelLocator.Context).Refresh(this.InfosClub);
			}

			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				InfosClubDao.GetInstance(ViewModelLocator.Context).Update(this.InfosClub);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.EnvoyerMessageErreur();
			}
		}

		public override void ExecuteFenetreCommand(string pCodeUC) {
			base.ExecuteFenetreCommand(pCodeUC);

			this.InitialisationListeVilles();
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(VilleDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		protected override bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				this.mErreurs.Add(ResErreurs.Groupe_LibelleObligatoire);
			}

			if (this.InfosClub.Adresse == null || string.IsNullOrWhiteSpace(this.InfosClub.Adresse.Libelle)) {
				this.mErreurs.Add(ResErreurs.InfosClub_AdresseObligatoire);
			}

			if (this.InfosClub.Adresse != null && this.InfosClub.Adresse.Ville == null) {
				this.mErreurs.Add(ResErreurs.InfosClub_VilleObligatoire);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
