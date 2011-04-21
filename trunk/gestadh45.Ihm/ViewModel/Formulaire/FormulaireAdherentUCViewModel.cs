using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
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
					this.RaisePropertyChanged("Adherent");
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
					this.RaisePropertyChanged("Sexes");
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
					this.RaisePropertyChanged("Villes");
				}
			}
		}

		public FormulaireAdherentUCViewModel() {
			this.Adherent = new Adherent();
			this.Adherent.Adresse = new Adresse();
			this.Adherent.Contact = new Contact();
			this.Adherent.DateNaissance = DateTime.Now;

			this.InitialisationListeVilles();
			this.InitialisationListeSexes();

			this.CodeUCOrigine = CodesUC.ConsultationAdherents;
			base.EstEdition = false;
		}

		public override void ExecuteAnnulerCommand(string pCodeUc) {
			if ((this.Adherent != null) && base.EstEdition) {
				AdherentDao.GetInstance(ViewModelLocator.Context).Refresh(this.Adherent);
			}
			base.ExecuteAnnulerCommand(pCodeUc);
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() 
				&& base.EstEdition 
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {
				
				AdherentDao.GetInstance(ViewModelLocator.Context).Update(this.Adherent);

				base.ExecuteEnregistrerCommand();
			}
			else if (this.VerifierSaisie() 
				&& !base.EstEdition 
				&& !AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {

				AdherentDao.GetInstance(ViewModelLocator.Context).Create(this.Adherent);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.ErreursVisibles = true;
			}
		}

		public override void ExecuteFenetreCommand(string pCodeUC) {
			base.ExecuteFenetreCommand(pCodeUC);

			this.InitialisationListeVilles();
		}

		private void InitialisationListeSexes() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(SexeDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("LibelleCourt", ListSortDirection.Descending));
			this.Sexes = defaultView;
		}

		private void InitialisationListeVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(VilleDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
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
				&& AdherentDao.GetInstance(ViewModelLocator.Context).Exist(this.Adherent)) {

					lErreurs.Add(ResErreurs.Adherent_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
