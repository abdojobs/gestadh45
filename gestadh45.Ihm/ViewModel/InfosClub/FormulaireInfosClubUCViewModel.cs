﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.InfosClubs
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
			this.InitialisationListeVilles();

			this.InfosClub = ViewModelLocator.DaoInfosClub.Read();
			ViewModelLocator.DaoInfosClub.Refresh(this.InfosClub);
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			Messenger.Default.Register<MsgSelectionElement<Ville>>(this, this.SelectionnerVille);
		}

		public override void ExecuteAnnulerCommand() {
			if (this.InfosClub != null) {
				ViewModelLocator.DaoVille.Refresh(this.InfosClub.Ville);
				ViewModelLocator.DaoInfosClub.Refresh(this.InfosClub);
			}

			base.ExecuteAnnulerCommand();
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				ViewModelLocator.DaoInfosClub.Update(this.InfosClub);

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
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoVille.List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		private void SelectionnerVille(MsgSelectionElement<Ville> msg) {
			this.InfosClub.Ville = msg.Content;
			this.RaisePropertyChanged(() => this.InfosClub);
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				lErreurs.Add(ResErreurs.InfosClub_NomObligatoire);
			}

			if (this.InfosClub.Adresse == null || string.IsNullOrWhiteSpace(this.InfosClub.Adresse)) {
				lErreurs.Add(ResErreurs.InfosClub_AdresseObligatoire);
			}

			if (this.InfosClub.Adresse != null && this.InfosClub.Ville == null) {
				lErreurs.Add(ResErreurs.InfosClub_VilleObligatoire);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
