using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Adherents
{
	public class FormulaireAdherentUCViewModel : ViewModelBaseFormulaire
	{
		private Adherent _adherent;
		private ICollectionView _sexes;
		private ICollectionView _villes;

		private IVilleDao _daoVille;
		private ISexeDao _daoSexe;
		private IAdherentDao _daoAdherent;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Adherent Adherent {
			get {
				return this._adherent;
			}
			set {
				if (this._adherent != value) {
					this._adherent = value;
					this.RaisePropertyChanged(() => this.Adherent);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des sexes
		/// </summary>
		public ICollectionView Sexes {
			get {
				return this._sexes;
			}
			set {
				if (this._sexes != value) {
					this._sexes = value;
					this.RaisePropertyChanged(() => this.Sexes);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public ICollectionView Villes {
			get {
				return this._villes;
			}
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}

		public FormulaireAdherentUCViewModel() {
			this._daoVille = DaoFactory.GetVilleDao(ViewModelLocator.DataSource);
			this._daoSexe = DaoFactory.GetSexeDao(ViewModelLocator.DataSource);
			this._daoAdherent = DaoFactory.GetAdherentDao(ViewModelLocator.DataSource);

			this.CreateSelectionnerSexeCommand();
			this.CreateSelectionnerVilleCommand();

			this.Adherent = new Adherent();
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
			this.InitialisationFormulaire();
			this.Adherent = pAdherent;
			this.RaisePropertyChanged(() => this.Adherent);
		}

		public override void ExecuteEnregistrerCommand() {
			var msg = new NotificationMessageSelectionElement<Adherent>(this.Adherent);

			bool saisieValide = this.VerifierSaisie();

			if (saisieValide 
				&& base.EstEdition
				&& this._daoAdherent.Exists(this.Adherent)) {

				this._daoAdherent.Update(this.Adherent);

				base.ExecuteEnregistrerCommand();
				Messenger.Default.Send(msg);
			}
			else if (saisieValide
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

		#region SelectionnerSexeCommand
		public ICommand SelectionnerSexeCommand { get; set; }

		private void CreateSelectionnerSexeCommand() {
			this.SelectionnerSexeCommand = new RelayCommand<Sexe>(
				this.ExecuteSelectionnerSexeCommand,
				this.CanExecuteSelectionnerSexeCommand
			);
		}

		public bool CanExecuteSelectionnerSexeCommand(Sexe pSexe) {
			return true;
		}

		public void ExecuteSelectionnerSexeCommand(Sexe pSexe) {
			this.Adherent.Sexe = pSexe;
			this.RaisePropertyChanged(() => this.Adherent);
		}
		#endregion

		#region SelectionnerVilleCommand
		public ICommand SelectionnerVilleCommand { get; set; }

		private void CreateSelectionnerVilleCommand() {
			this.SelectionnerVilleCommand = new RelayCommand<Ville>(
				this.ExecuteSelectionnerVilleCommand,
				this.CanExecuteSelectionnerVilleCommand
			);
		}

		public bool CanExecuteSelectionnerVilleCommand(Ville pVille) {
			return true;
		}

		public void ExecuteSelectionnerVilleCommand(Ville pVille) {
			this.Adherent.Adresse.Ville = pVille;
			this.RaisePropertyChanged(() => this.Adherent);
		}
		#endregion

		private void SelectionnerVille(Ville pVille) {
			this.InitialisationFormulaire();
			this.Adherent.Adresse.Ville = pVille;
			this.RaisePropertyChanged(() => this.Adherent);
		}

		private void InitialisationFormulaire() {
			ICollectionView defaultViewSexes = CollectionViewSource.GetDefaultView(this._daoSexe.List());
			defaultViewSexes.SortDescriptions.Add(new SortDescription("LibelleCourt", ListSortDirection.Descending));
			this.Sexes = defaultViewSexes;

			ICollectionView defaultViewVilles = CollectionViewSource.GetDefaultView(this._daoVille.List());
			defaultViewVilles.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultViewVilles;
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

			if (string.IsNullOrWhiteSpace(this.Adherent.Adresse.Libelle)) {
				lErreurs.Add(ResErreurs.Adherent_AdresseObligatoire);
			}

			if (this.Adherent.Adresse.Ville == null
				|| string.IsNullOrWhiteSpace(this.Adherent.Adresse.Ville.CodePostal)
				|| string.IsNullOrWhiteSpace(this.Adherent.Adresse.Ville.Libelle)) {
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
