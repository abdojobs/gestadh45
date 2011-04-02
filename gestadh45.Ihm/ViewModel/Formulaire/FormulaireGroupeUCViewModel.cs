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
	public class FormulaireGroupeUCViewModel : ViewModelBaseFormulaire
	{
		private Groupe mGroupe;
		private ICollectionView mJoursSemaine;

		public FormulaireGroupeUCViewModel() {
			this.Groupe = new Groupe();
			this.Groupe.Saison = SaisonDao.GetInstance(ViewModelLocator.Context).ReadSaisonCourante();
			this.InitialisationListeJoursSemaine();
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

		public void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() 
				&& !GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe)) {
				
				GroupeDao.GetInstance(ViewModelLocator.Context).Create(this.Groupe);
				Messenger.Default.Send<NotificationMessageChangementUC>(
					new NotificationMessageChangementUC(CodesUC.ConsultationGroupes)
				);
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
				);
			}
		}

		private void InitialisationListeJoursSemaine() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(JourSemaineDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
			this.JoursSemaine = defaultView;
		}

		public ICommand EnregistrerCommand { get; set; }

		public Groupe Groupe {
			get {
				return this.mGroupe;
			}
			set {
				if (this.mGroupe != value) {
					this.mGroupe = value;
					this.RaisePropertyChanged("Groupe");
				}
			}
		}

		public ICollectionView JoursSemaine {
			get {
				return this.mJoursSemaine;
			}
			set {
				if (this.mJoursSemaine != value) {
					this.mJoursSemaine = value;
					this.RaisePropertyChanged("JoursSemaine");
				}
			}
		}

		private bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.Groupe.Libelle)) {
				this.mErreurs.Add(ResErreurs.Groupe_LibelleObligatoire);
			}

			if (this.Groupe.JourSemaine == null) {
				this.mErreurs.Add(ResErreurs.Groupe_JourObligatoire);
			}

			if (this.Groupe.HeureDebut > this.Groupe.HeureFin
				|| (this.Groupe.HeureDebut == this.Groupe.HeureDebut && this.Groupe.MinuteDebut >= this.Groupe.MinuteFin)) {
					this.mErreurs.Add(ResErreurs.Groupe_HeureFinSupHeureDebut);
			}

			if (!this.EstEdition 
				&& this.mErreurs.Count == 0
				&& GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe)) {

				this.mErreurs.Add(ResErreurs.Inscription_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
