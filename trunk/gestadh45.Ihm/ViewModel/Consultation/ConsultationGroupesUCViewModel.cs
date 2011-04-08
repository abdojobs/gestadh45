using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationGroupesUCViewModel : ViewModelBaseConsultation
	{
		private Groupe mGroupe;
		private ICollectionView mGroupesSaisonCourante;

		/// <summary>
		/// Obtient/Définit le groupe à afficher
		/// </summary>
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

		/// <summary>
		/// Obtient/Définit la liste des groupes de la saison courante
		/// </summary>
		public ICollectionView GroupesSaisonCourante {
			get {
				return this.mGroupesSaisonCourante;
			}
			set {
				if (this.mGroupesSaisonCourante != value) {
					this.mGroupesSaisonCourante = value;
					this.RaisePropertyChanged("GroupesSaisonCourante");
				}
			}
		}

		public ICommand AfficherDetailsGroupeCommand { get; set; }
		public ICommand SupprimerGroupeCommand { get; set; }

		public ConsultationGroupesUCViewModel() {
			this.InitialisationListeGroupes();

			this.CreateAfficherDetailsGroupeCommand();
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Groupe != null 
				&& GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe) 
				&& !GroupeDao.GetInstance(ViewModelLocator.Context).IsUsed(this.Groupe)
			);
		}

		private void CreateAfficherDetailsGroupeCommand() {
			this.AfficherDetailsGroupeCommand = new RelayCommand<Groupe>(
				this.ExecuteAfficherDetailsGroupeCommand
			);
		}

		public void ExecuteAfficherDetailsGroupeCommand(Groupe pGroupe) {
			if (pGroupe != null) {
				this.Groupe = pGroupe;
			}
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Groupe != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprGroupe, 
					this.ExecuteSupprimerGroupeCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
			this.CreateSupprimerCommand();
		}

		private void ExecuteSupprimerGroupeCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				GroupeDao.GetInstance(ViewModelLocator.Context).Delete(this.Groupe);
				this.InitialisationListeGroupes();
				this.Groupe = null;
			}
		}

		private void InitialisationListeGroupes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				GroupeDao.GetInstance(ViewModelLocator.Context).ListSaisonCourante()
			);

			foreach (Groupe lGroupe in defaultView) {
				GroupeDao.GetInstance(ViewModelLocator.Context).Refresh(lGroupe);
			}

			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));
			this.GroupesSaisonCourante = defaultView;
		}

		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireGroupe)
			);
		}
	}
}
