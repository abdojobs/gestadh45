using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationAdherentsUCViewModel : ViewModelBaseConsultation
	{
		public ICommand InscrireCommand { get; set; }
		
		private Adherent _adherent;
		private ICollectionView _adherents;

		private IAdherentDao _daoAdherent;

		/// <summary>
		/// Obtient/Définit l'adhérent à afficher
		/// </summary>
		public Adherent Adherent {
			get {
				return this._adherent;
			}
			set {
				if (this._adherent != value) {
					this._adherent = value;
					this.RaisePropertyChanged(()=>this.Adherent);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public ICollectionView Adherents {
			get {
				return this._adherents;
			}
			set {
				if (this._adherents != value) {
					this._adherents = value;
					this.RaisePropertyChanged(()=>this.Adherents);
				}
			}
		}

		public ConsultationAdherentsUCViewModel() {
			this._daoAdherent = DaoFactory.GetAdherentDao(ViewModelLocator.DataSource);

			this.InitialisationFormulaire();

			this.CreateInscrireCommand();
			this.CreateDupliquerCommand();

			Messenger.Default.Register<NotificationMessageSelectionElement<Adherent>>(
				this, 
				(msg) => this.SelectionnerAdherent(msg.Content)
			);
		}

		#region EditerCommand
		public override bool CanExecuteEditerCommand() {
			return (this.Adherent != null);
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
		#endregion

		#region SupprimerCommand
		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Adherent != null
				&& this._daoAdherent.Exists(this.Adherent)
				&& !this._daoAdherent.IsUsed(this.Adherent)
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
		#endregion

		#region InscrireCommand
		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand(
				this.ExecuteInscrireCommand, 
				this.CanExecuteInscrireCommand
			);
		}

		public bool CanExecuteInscrireCommand() {
			return (this.Adherent != null);
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
		#endregion

		#region AfficherDetailsCommand
		public override void ExecuteAfficherDetailsCommand(object pAdherent) {
			if (pAdherent != null && pAdherent is Adherent) {
				this.Adherent = pAdherent as Adherent;
			}
		}
		#endregion

		#region CreerCommand
		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireAdherent);
		}
		#endregion

		#region FiltrerListeCommand
		public override void ExecuteFiltrerListeCommand(string pFiltre) {
			if (string.IsNullOrEmpty(pFiltre)) {
				this.Adherents.Filter = null;
			}
			else {
				this.Adherents.Filter = (p) => ((Adherent)p).ToString().ToUpper().Contains(pFiltre.ToUpper());
			}
		}
		#endregion

		#region DuppliquerCommand
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
			var newAdherent = this.Adherent.Clone();
			newAdherent.Id = 0;
			newAdherent.Prenom = string.Empty;
			newAdherent.Commentaire = "Copie de " + this.Adherent.ToString();

			var msg = new MsgAfficherUC<Adherent>(CodesUC.FormulaireAdherent, MsgAfficherUC.TypeAffichage.Interne, newAdherent, MsgAfficherUC.TypeOuverture.Creation);
			Messenger.Default.Send(msg);
		}
		#endregion

		/// <summary>
		/// Méthode appellée à la réception de la réponse au message de confirmation de suppression
		/// </summary>
		/// <param name="pResult">Résultat de la demande de confirmation</param>
		private void ExecuteSupprimerAdherentCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this._daoAdherent.Delete(this.Adherent);
				this.InitialisationFormulaire();
				this.Adherent = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionAdherent);
			}
		}

		private void InitialisationFormulaire() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;

			var msg = new NotificationMessage(TypesNotification.EffacerFiltre);
			// TODO commenté tant que le pb n'est pas résolu
			//Messenger.Default.Send(msg);
		}

		private void SelectionnerAdherent(Adherent pAdh) {
			this.Adherent = pAdh;
			this.RaisePropertyChanged(() => this.Adherent);
		}
	}
}
