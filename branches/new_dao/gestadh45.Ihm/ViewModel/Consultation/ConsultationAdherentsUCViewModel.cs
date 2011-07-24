﻿using System.ComponentModel;
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
		
		private Adherent mAdherent;
		private ICollectionView mAdherents;

		private AdherentDao _daoAdherent;

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
			this._daoAdherent = new AdherentDao(ViewModelLocator.DataSource);

			this.InitialisationListeAdherents();

			this.CreateInscrireCommand();

			Messenger.Default.Register<NotificationMessageSelectionElement<Adherent>>(this, this.SelectionnerAdherent);
		}

		public override bool CanExecuteEditerCommand() {
			return (this.Adherent != null);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Adherent != null
				&& this._daoAdherent.Exists(this.Adherent)
				&& !this._daoAdherent.IsUsed(this.Adherent)
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

		/// <summary>
		/// Méthode appellée à la réception de la réponse au message de confirmation de suppression
		/// </summary>
		/// <param name="pResult">Résultat de la demande de confirmation</param>
		private void ExecuteSupprimerAdherentCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				this._daoAdherent.Delete(this.Adherent);
				this.InitialisationListeAdherents();
				this.Adherent = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionAdherent);
			}
		}

		private void InitialisationListeAdherents() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoAdherent.List());
			defaultView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
			this.Adherents = defaultView;
		}

		private void SelectionnerAdherent(NotificationMessageSelectionElement<Adherent> msg) {
			this.Adherent = msg.Content;
			this.RaisePropertyChanged(() => this.Adherent);
		}
	}
}
