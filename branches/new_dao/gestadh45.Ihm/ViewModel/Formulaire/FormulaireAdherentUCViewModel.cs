using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireAdherentUCViewModel : ViewModelBaseFormulaire
	{
		private Adherent mAdherent;
		private ICollectionView mSexes;
		private ICollectionView mVilles;

		private VilleDao _daoVille;
		private SexeDao _daoSexe;
		private AdherentDao _daoAdherent;

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
			this._daoVille = new VilleDao(ViewModelLocator.DataSource);
			this._daoSexe = new SexeDao(ViewModelLocator.DataSource);
			this._daoAdherent = new AdherentDao(ViewModelLocator.DataSource);

			this.Adherent = new Adherent();
			this.Adherent.Adresse = new Adresse();
			this.Adherent.Contact = new Contact();
			this.Adherent.DateNaissance = DateTime.Now;

			this.InitialisationFormulaire();

			this.CodeUCOrigine = CodesUC.ConsultationAdherents;
			base.EstEdition = false;

			Messenger.Default.Register<NotificationMessageSelectionElement<Ville>>(
				this, 
				(msg) => this.SelectionnerVille(msg.Content)
			);
		}

		public void SetAdherent(Adherent pAdherent) {
			this.Adherent = pAdherent;
			this.InitialisationFormulaire();
		}

		public override void ExecuteEnregistrerCommand() {
			var msg = new NotificationMessageSelectionElement<Adherent>(this.Adherent);

			if (this.VerifierSaisie() 
				&& base.EstEdition
				&& this._daoAdherent.Exists(this.Adherent)) {

					this._daoAdherent.Update(this.Adherent);

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition
				&& !this._daoAdherent.Exists(this.Adherent)) {

					this._daoAdherent.Create(this.Adherent);

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		private void SelectionnerVille(Ville pVille) {
			this.InitialisationFormulaire();

			var rqVille = from Ville v in this.Villes.SourceCollection
						  where v.Id == pVille.Id
						  select v;

			if (rqVille.Count() > 0) {
				this.Adherent.Adresse.Ville = rqVille.First();
				this.RaisePropertyChanged(() => this.Adherent);
			}
		}

		private void InitialisationFormulaire() {
			ICollectionView defaultViewSexes = CollectionViewSource.GetDefaultView(this._daoSexe.List());
			defaultViewSexes.SortDescriptions.Add(new SortDescription("LibelleCourt", ListSortDirection.Descending));
			this.Sexes = defaultViewSexes;

			ICollectionView defaultViewVilles = CollectionViewSource.GetDefaultView(this._daoVille.List());
			defaultViewVilles.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultViewVilles;

			if (this.Adherent.Sexe != null) {
				var rqSexe = from Sexe s in this.Sexes.SourceCollection
							 where s.Id == this.Adherent.Sexe.Id
							 select s;

				if (rqSexe.Count() > 0) {
					this.Adherent.Sexe = rqSexe.First();
					this.RaisePropertyChanged(() => this.Adherent);
				}
			}
			else {
				var rqSexe = from Sexe s in this.Sexes.SourceCollection
							 select s;

				this.Adherent.Sexe = rqSexe.First();
			}

			if (this.Adherent.Adresse.Ville != null) {
				var rqVille = from Ville v in this.Villes.SourceCollection
							  where v.Id == this.Adherent.Adresse.Ville.Id
							  select v;

				if (rqVille.Count() > 0) {
					this.Adherent.Adresse.Ville = rqVille.First();
					this.RaisePropertyChanged(() => this.Adherent);
				}
			}
			else {
				var rqVille = from Ville v in this.Villes.SourceCollection
							  select v;

				this.Adherent.Adresse.Ville = rqVille.First();
			}
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

			if (this.Adherent.Adresse == null || string.IsNullOrWhiteSpace(this.Adherent.Adresse.Libelle)) {
				lErreurs.Add(ResErreurs.Adherent_AdresseObligatoire);
			}

			if (this.Adherent.Adresse != null && this.Adherent.Adresse.Ville == null) {
				lErreurs.Add(ResErreurs.Adherent_VilleObligatoire);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this._daoAdherent.Exists(this.Adherent)) {

					lErreurs.Add(ResErreurs.Adherent_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
