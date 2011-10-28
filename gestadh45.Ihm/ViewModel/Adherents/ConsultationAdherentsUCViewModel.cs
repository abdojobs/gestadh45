using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Adherents
{
	public class ConsultationAdherentsUCViewModel : ViewModelBaseConsultation
	{
		public ICommand InscrireCommand { get; set; }
		
		private Adherent mAdherent;
		private ICollectionView mAdherents;

		/// <summary>
		/// Obtient/Définit l'adhérent à afficher
		/// </summary>
		public Adherent Adherent {
			get {
				return this.mAdherent;
			}
			set {
				if (this.mAdherent != value) {
					this.mAdherent = value;
					this.RaisePropertyChanged(()=>this.Adherent);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public ICollectionView Adherents {
			get {
				return this.mAdherents;
			}
			set {
				if (this.mAdherents != value) {
					this.mAdherents = value;
					this.RaisePropertyChanged(()=>this.Adherents);
				}
			}
		}

		public ConsultationAdherentsUCViewModel() {
			this.InitialisationListeAdherents();

			this.CreateInscrireCommand();
			this.CreateDupliquerCommand();

			Messenger.Default.Register<MsgSelectionElement<Adherent>>(this, this.SelectionnerAdherent);
		}

		public override bool CanExecuteEditerCommand() {
			return (this.Adherent != null);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Adherent != null
				&& ViewModelLocator.DaoAdherent.Exists(this.Adherent)
				&& !ViewModelLocator.DaoAdherent.IsUsed(this.Adherent)
				);
		}

		public bool CanExecuteInscrireCommand() {
			return (this.Adherent != null);
		}

		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand(
				this.ExecuteInscrireCommand, 
				this.CanExecuteInscrireCommand
			);
		}

		public override void ExecuteAfficherDetailsCommand(object pAdherent) {
			if (pAdherent != null && pAdherent is Adherent) {
				this.Adherent = pAdherent as Adherent;
			}
		}

		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireAdherent);
		}

		public override void ExecuteEditerCommand() {
			base.ExecuteEditerCommand();

			Messenger.Default.Send<MsgAfficherUC<Adherent>>(
				new MsgAfficherUC<Adherent>(
					CodesUC.FormulaireAdherent,
					MsgAfficherUC.TypeAffichage.Interne,
					this.Adherent
				)
			);
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Adherent != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprAdherent,
					this.ExecuteSupprimerAdherentCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
		}

		public override void ExecuteFiltrerListeCommand(string pFiltre) {
			if (string.IsNullOrEmpty(pFiltre)) {
				this.Adherents.Filter = null;
			}
			else {
				this.Adherents.Filter = (p) => ((Adherent)p).ToString().ToUpper().Contains(pFiltre.ToUpper());
			}
		}

		public void ExecuteInscrireCommand() {
			Messenger.Default.Send<MsgAfficherUC<Adherent>>(
				new MsgAfficherUC<Adherent>(
					CodesUC.FormulaireInscription, 
					MsgAfficherUC.TypeAffichage.Interne,
					this.Adherent
				)
			);
		}

		#region DupliquerCommand
		public ICommand DupliquerCommand { get; set; }

		private void CreateDupliquerCommand() {
			this.DupliquerCommand = new RelayCommand(
				this.ExecuteDupliquerCommand,
				this.CanExecuteDupliquerCommand
			);
		}

		public bool CanExecuteDupliquerCommand() {
			return this.Adherent != null;
		}

		public void ExecuteDupliquerCommand() {
			var newAdherent = this.Adherent.Clone() as Adherent;
			newAdherent.ID = 0;
			newAdherent.Prenom += " (copie)";
			newAdherent.Commentaire = "Copie de " + this.Adherent.ToString();

			ViewModelLocator.DaoAdherent.Create(newAdherent);
			this.InitialisationListeAdherents();

			this.AfficherInformationIhm(ResMessages.MessageConfirmDuplicationAdherent);
		}
		#endregion

		/// <summary>
		/// Méthode appellée à la réception de la réponse au message de confirmation de suppression
		/// </summary>
		/// <param name="pResult">Résultat de la demande de confirmation</param>
		private void ExecuteSupprimerAdherentCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				ViewModelLocator.DaoAdherent.Delete(this.Adherent);
				this.InitialisationListeAdherents();
				this.Adherent = null;

				var msg = new NotificationMessage(TypesNotification.EffacerFiltre);
				Messenger.Default.Send(msg);

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionAdherent);
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void SelectionnerAdherent(MsgSelectionElement<Adherent> msg) {
			this.Adherent = msg.Content;
			this.RaisePropertyChanged(() => this.Adherent);
		}
	}
}
