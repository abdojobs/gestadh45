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

		private IVilleDao mDaoVille;
		private IInfosClubDao mDaoInfosClub;
		private IContactDao mDaoContact;
		private IAdresseDao mDaoAdresse;

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
			this.mDaoVille = this.mDaoFactory.GetVilleDao();
			this.mDaoInfosClub = this.mDaoFactory.GetInfosClubDao();
			this.mDaoContact = this.mDaoFactory.GetContactDao();
			this.mDaoAdresse = this.mDaoFactory.GetAdresseDao();

			this.InitialisationListeVilles();

			this.InfosClub = this.mDaoInfosClub.Read();
			this.mDaoInfosClub.Refresh(this.InfosClub);
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(this, this.SelectionnerVille);
		}

		public override void ExecuteAnnulerCommand() {
			if (this.InfosClub != null) {
				this.mDaoVille.Refresh(this.InfosClub.Adresse.Ville);
				this.mDaoAdresse.Refresh(this.InfosClub.Adresse);
				this.mDaoContact.Refresh(this.InfosClub.Contact);
				this.mDaoInfosClub.Refresh(this.InfosClub);
			}

			base.ExecuteAnnulerCommand();
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				this.mDaoInfosClub.Update(this.InfosClub);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
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

		private void SelectionnerVille(NotificationMessageSelectionElement<Ville> msg) {
			this.InfosClub.Adresse.Ville = msg.Content;
			this.RaisePropertyChanged(() => this.InfosClub);
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
