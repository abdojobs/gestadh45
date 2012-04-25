﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Villes
{
	public class ConsultationVillesUCViewModel : ViewModelBaseConsultation
	{
		private Ville mVille;
		private ICollectionView mVilles;

		/// <summary>
		/// Obtient/Définit la ville à afficher
		/// </summary>
		public Ville Ville {
			get {
				return this.mVille;
			}
			set {
				if (this.mVille != value) {
					this.mVille = value;
					this.RaisePropertyChanged(() => this.Ville);
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

		public ConsultationVillesUCViewModel() {
			this.InitialisationListeVilles();

			Messenger.Default.Register<MsgSelectionElement<Ville>>(this, this.SelectionnerVille);
		}

		public override bool CanExecuteSupprimerCommand() {
			return (
				this.Ville != null
				&& ViewModelLocator.DaoVille.Exists(this.Ville)
				&& !ViewModelLocator.DaoVille.IsUsed(this.Ville)
				);
		}

		public override void ExecuteAfficherDetailsCommand(object pVille) {
			if (pVille != null && pVille is Ville) {
				this.Ville = pVille as Ville;
			}
		}

		public override void ExecuteSupprimerCommand() {
			if (this.Ville != null) {
				DialogMessageConfirmation message = new DialogMessageConfirmation(
					ResMessages.MessageConfirmSupprVille, 
					this.ExecuteSupprimerVilleCommandCallBack
				);

				Messenger.Default.Send<DialogMessageConfirmation>(message);
			}
		}

		private void ExecuteSupprimerVilleCommandCallBack(MessageBoxResult pResult) {
			if (pResult == MessageBoxResult.OK) {
				ViewModelLocator.DaoVille.Delete(this.Ville);
				this.InitialisationListeVilles();
				this.Ville = null;

				this.AfficherInformationIhm(ResMessages.MessageInfoSuppressionVille);
			}
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(
				ViewModelLocator.DaoVille.List()
			);

			foreach (Ville lVille in defaultView) {
				ViewModelLocator.DaoVille.Refresh(lVille);
			}

			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		public override void ExecuteCreerCommand() {
			base.ExecuteCreerCommand();

			this.AfficherEcran(CodesUC.FormulaireVille);
		}

		private void SelectionnerVille(MsgSelectionElement<Ville> msg) {
			this.Ville = msg.Content;
			this.RaisePropertyChanged(() => this.Ville);
		}
	}
}