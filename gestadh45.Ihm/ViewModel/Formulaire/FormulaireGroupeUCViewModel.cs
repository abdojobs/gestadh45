using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireGroupeUCViewModel : ViewModelBaseFormulaire
	{
		private Groupe _groupe;
		private ICollectionView _joursSemaine;

		private JourSemaineDao _daoJoursSemaine;
		private SaisonDao _daoSaison;
		private GroupeDao _daoGroupe;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Groupe Groupe {
			get {
				return this._groupe;
			}
			set {
				if (this._groupe != value) {
					this._groupe = value;
					this.RaisePropertyChanged(() => this.Groupe);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des jours de la semaine
		/// </summary>
		public ICollectionView JoursSemaine {
			get {
				return this._joursSemaine;
			}
			set {
				if (this._joursSemaine != value) {
					this._joursSemaine = value;
					this.RaisePropertyChanged(() => this.JoursSemaine);
				}
			}
		}

		public FormulaireGroupeUCViewModel() {
			this._daoJoursSemaine = new JourSemaineDao(ViewModelLocator.DataSource);
			this._daoSaison = new SaisonDao(ViewModelLocator.DataSource);
			this._daoGroupe = new GroupeDao(ViewModelLocator.DataSource);

			this.CreateSelectionnerJourSemaineCommand();

			this.Groupe = new Groupe();
			this.Groupe.Saison = this._daoSaison.ReadSaisonCourante();
			this.InitialisationListeJoursSemaine();
			this.CodeUCOrigine = CodesUC.ConsultationGroupes;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()
				&& !this._daoGroupe.Exists(this.Groupe)) {
					
				this._daoGroupe.Create(this.Groupe);
				base.ExecuteEnregistrerCommand();

				var msg = new NotificationMessageSelectionElement<Groupe>(this.Groupe);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		#region SelectionnerJourSemaineCommand
		public ICommand SelectionnerJourSemaineCommand { get; set; }

		private void CreateSelectionnerJourSemaineCommand() {
			this.SelectionnerJourSemaineCommand = new RelayCommand<JourSemaine>(
				this.ExecuteSelectionnerJourSemaineCommand,
				this.CanExecuteSelectionnerJourSemaineCommand
			);
		}

		public bool CanExecuteSelectionnerJourSemaineCommand(JourSemaine pJourSemaine) {
			return true;
		}

		public void ExecuteSelectionnerJourSemaineCommand(JourSemaine pJourSemaine) {
			this.Groupe.JourSemaine = pJourSemaine;
			this.RaisePropertyChanged(() => this.Groupe);
		}
		#endregion

		private void InitialisationListeJoursSemaine() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this._daoJoursSemaine.List());
			defaultView.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
			this.JoursSemaine = defaultView;
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.Groupe.Libelle)) {
				lErreurs.Add(ResErreurs.Groupe_LibelleObligatoire);
			}

			if (this.Groupe.JourSemaine == null) {
				lErreurs.Add(ResErreurs.Groupe_JourObligatoire);
			}

			if (this.Groupe.HeureDebut.Hour > this.Groupe.HeureFin.Hour
				|| (this.Groupe.HeureDebut.Hour == this.Groupe.HeureFin.Hour && this.Groupe.HeureDebut.Minute >= this.Groupe.HeureFin.Minute)) {
			        lErreurs.Add(ResErreurs.Groupe_HeureFinSupHeureDebut);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this._daoGroupe.Exists(this.Groupe)) {

					lErreurs.Add(ResErreurs.Groupe_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
