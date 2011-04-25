using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;
using System;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireGroupeUCViewModel : ViewModelBaseFormulaire
	{
		private Groupe mGroupe;
		private ICollectionView mJoursSemaine;

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
			this.Groupe = new Groupe();
			this.Groupe.Saison = SaisonDao.GetInstance(ViewModelLocator.Context).ReadSaisonCourante();
			this.InitialisationListeJoursSemaine();
			this.CodeUCOrigine = CodesUC.ConsultationGroupes;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() 
				&& !GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe)) {
				
				GroupeDao.GetInstance(ViewModelLocator.Context).Create(this.Groupe);

				base.ExecuteEnregistrerCommand();
			}
			else {
				this.ErreursVisibles = true;
			}
		}

		private void InitialisationListeJoursSemaine() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(JourSemaineDao.GetInstance(ViewModelLocator.Context).List());
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

			if (this.Groupe.HeureDebutDT.Hour > this.Groupe.HeureFinDT.Hour
				|| (this.Groupe.HeureDebutDT.Hour == this.Groupe.HeureFinDT.Hour && this.Groupe.HeureDebutDT.Minute >= this.Groupe.HeureFinDT.Minute)) {
			        lErreurs.Add(ResErreurs.Groupe_HeureFinSupHeureDebut);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe)) {

					lErreurs.Add(ResErreurs.Groupe_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
