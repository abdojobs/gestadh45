using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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

		public FormulaireInfosClubUCViewModel() {
			this.InitialisationListeVilles();
			this.InfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();
			base.CreateAnnulerCommand();
			this.CreateEnregistrerCommand();
		}

		public bool CanExecuteEnregistrerCommand() {
			return true;
		}

		private void CreateEnregistrerCommand() {
			this.EnregistrerCommand = new RelayCommand(
				this.ExecuteEnregistrerCommand, 
				this.CanExecuteEnregistrerCommand
			);
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.InfosClub != null) {
				InfosClubDao.GetInstance(ViewModelLocator.Context).Refresh(this.InfosClub);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				InfosClubDao.GetInstance(ViewModelLocator.Context).Update(this.InfosClub);
				Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>("ConsultationInfosClub", "ChangementUserControl"));
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
				);
			}
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(VilleDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public ICommand EnregistrerCommand { get; set; }

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

		private bool VerifierSaisie() {
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
