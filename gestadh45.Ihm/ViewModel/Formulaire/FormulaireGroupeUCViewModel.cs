﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

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
			this.Groupe.Commentaire = string.Empty;
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
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
				);
			}
		}

		private void InitialisationListeJoursSemaine() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(JourSemaineDao.GetInstance(ViewModelLocator.Context).List());
			defaultView.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
			this.JoursSemaine = defaultView;
		}

		protected override bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.Groupe.Libelle)) {
				this.mErreurs.Add(ResErreurs.Groupe_LibelleObligatoire);
			}

			if (this.Groupe.JourSemaine == null) {
				this.mErreurs.Add(ResErreurs.Groupe_JourObligatoire);
			}

			if (this.Groupe.HeureDebut > this.Groupe.HeureFin
				|| (this.Groupe.HeureDebut == this.Groupe.HeureFin && this.Groupe.MinuteDebut >= this.Groupe.MinuteFin)) {
					this.mErreurs.Add(ResErreurs.Groupe_HeureFinSupHeureDebut);
			}

			if (!this.EstEdition 
				&& this.mErreurs.Count == 0
				&& GroupeDao.GetInstance(ViewModelLocator.Context).Exist(this.Groupe)) {

				this.mErreurs.Add(ResErreurs.Groupe_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}