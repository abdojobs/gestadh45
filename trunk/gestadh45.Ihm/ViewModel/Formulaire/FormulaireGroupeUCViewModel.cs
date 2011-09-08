using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.dal;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireGroupeUCViewModel : ViewModelBaseFormulaire
	{
		private Groupe mGroupe;
		private ICollectionView mJoursSemaine;

		private IJourSemaineDao mDaoJoursSemaine;
		private ISaisonDao mDaoSaison;
		private IGroupeDao mDaoGroupe;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Groupe Groupe {
			get {
				return this.mGroupe;
			}
			set {
				if (this.mGroupe != value) {
					this.mGroupe = value;
					this.RaisePropertyChanged("Groupe");
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des jours de la semaine
		/// </summary>
		public ICollectionView JoursSemaine {
			get {
				return this.mJoursSemaine;
			}
			set {
				if (this.mJoursSemaine != value) {
					this.mJoursSemaine = value;
					this.RaisePropertyChanged("JoursSemaine");
				}
			}
		}

		public FormulaireGroupeUCViewModel() {
			this.mDaoJoursSemaine = this.mDaoFactory.GetJourSemaineDao();
			this.mDaoSaison = this.mDaoFactory.GetSaisonDao();
			this.mDaoGroupe = this.mDaoFactory.GetGroupeDao();

			this.Groupe = new Groupe();
			this.Groupe.Saison = this.mDaoSaison.ReadSaisonCourante();
			this.InitialisationListeJoursSemaine();
			this.CodeUCOrigine = CodesUC.ConsultationGroupes;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()
				&& !this.mDaoGroupe.Exists(this.Groupe)) {
					
				this.mDaoGroupe.Create(this.Groupe);
				base.ExecuteEnregistrerCommand();

				var msg = new NotificationMessageSelectionElement<Groupe>(this.Groupe);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		private void InitialisationListeJoursSemaine() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.mDaoJoursSemaine.List());
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
				&& this.mDaoGroupe.Exists(this.Groupe)) {

					lErreurs.Add(ResErreurs.Groupe_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
