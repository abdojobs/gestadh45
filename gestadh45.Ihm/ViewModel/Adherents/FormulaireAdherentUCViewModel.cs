using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Adherents
{
	public class FormulaireAdherentUCViewModel : ViewModelBaseFormulaire
	{
		private Adherent mAdherent;
		private ICollectionView mSexes;
		private ICollectionView mVilles;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Adherent Adherent {
			get {
				return this.mAdherent;
			}
			set {
				if (this.mAdherent != value) {
					this.mAdherent = value;
					this.RaisePropertyChanged(() => this.Adherent);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des sexes
		/// </summary>
		public ICollectionView Sexes {
			get {
				return this.mSexes;
			}
			set {
				if (this.mSexes != value) {
					this.mSexes = value;
					this.RaisePropertyChanged(() => this.Sexes);
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

		public FormulaireAdherentUCViewModel() {
			this.Adherent = new Adherent();
			this.Adherent.DateNaissance = DateTime.Now;

			this.InitialisationListeVilles();
			this.InitialisationListeSexes();

			this.CodeUCOrigine = CodesUC.ConsultationAdherents;
			base.EstEdition = false;

			Messenger.Default.Register<MsgSelectionElement<Ville>>(this, this.SelectionnerVille);
		}

		public override void ExecuteAnnulerCommand() {
			if ((this.Adherent != null) && base.EstEdition) {
				ViewModelLocator.DaoVille.Refresh(this.Adherent.Ville);
				ViewModelLocator.DaoAdherent.Refresh(this.Adherent);
			}
			base.ExecuteAnnulerCommand();
		}

		public override void ExecuteEnregistrerCommand() {
			var msg = new MsgSelectionElement<Adherent>(this.Adherent);

			if (this.VerifierSaisie()) {
				if (this.EstEdition) {
					ViewModelLocator.DaoAdherent.Update(this.Adherent);
				}
				else {
					ViewModelLocator.DaoAdherent.Create(this.Adherent);
				}

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		public override void ExecuteFenetreCommand(string pCodeUC) {
			base.ExecuteFenetreCommand(pCodeUC);

			this.InitialisationListeVilles();
		}

		private void InitialisationListeSexes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoSexe.List());
			defaultView.SortDescriptions.Add(new SortDescription("LibelleCourt", ListSortDirection.Descending));
			this.Sexes = defaultView;
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(ViewModelLocator.DaoVille.List());
			defaultView.SortDescriptions.Add(new SortDescription("CodePostal", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		private void SelectionnerVille(MsgSelectionElement<Ville> msg) {
			this.Adherent.Ville = msg.Content;
			this.RaisePropertyChanged(() => this.Adherent);
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if(string.IsNullOrWhiteSpace(this.Adherent.Nom)) {
				lErreurs.Add(ResErreurs.Adherent_NomObligatoire);
			}

			if(string.IsNullOrWhiteSpace(this.Adherent.Prenom)) {
				lErreurs.Add(ResErreurs.Adherent_PrenomObligatoire);
			}

			if (this.Adherent.DateNaissance == null) {
				lErreurs.Add(ResErreurs.Adherent_DateNaissanceObligatoire);
			}

			if (this.Adherent.Sexe == null) {
				lErreurs.Add(ResErreurs.Adherent_SexeObligatoire);
			}

			if (this.Adherent.Adresse == null || string.IsNullOrWhiteSpace(this.Adherent.Adresse)) {
				lErreurs.Add(ResErreurs.Adherent_AdresseObligatoire);
			}

			if (this.Adherent.Adresse != null && this.Adherent.Ville == null) {
				lErreurs.Add(ResErreurs.Adherent_VilleObligatoire);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& ViewModelLocator.DaoAdherent.Exists(this.Adherent)) {

					lErreurs.Add(ResErreurs.Adherent_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
