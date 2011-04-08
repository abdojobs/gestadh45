using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireSaisonUCViewModel : ViewModelBaseFormulaire
	{
		private Saison mSaison;

		/// <summary>
		/// Obtient/Définit l'année de début de la saison
		/// </summary>
		public double AnneeDebutIhm {
			get {
				return (double)this.Saison.AnneeDebut;
			}
			set {
				if (this.Saison.AnneeDebut != ((int)value)) {
					this.Saison.AnneeDebut = (int)value;
					this.Saison.AnneeFin = ((int)value) + DureeSaison;
					this.RaisePropertyChanged("Saison");
				}
			}
		}

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Saison Saison {
			get {
				return this.mSaison;
			}
			set {
				if (this.mSaison != value) {
					this.mSaison = value;
					this.RaisePropertyChanged("Saison");
				}
			}
		}

		private const int DureeSaison = 1;

		public FormulaireSaisonUCViewModel() {
			Saison saison = new Saison
			{
				AnneeDebut = DateTime.Now.Year,
				AnneeFin = DateTime.Now.Year + DureeSaison,
				EstSaisonCourante = 0L
			};
			this.Saison = saison;

			this.CodeUCOrigine = CodesUC.ConsultationSaisons;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !SaisonDao.GetInstance(ViewModelLocator.Context).Exist(this.Saison)) {
				SaisonDao.GetInstance(ViewModelLocator.Context).Create(this.Saison);

				base.ExecuteEnregistrerCommand();
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(
						TypesNotification.Erreur, 
						this.ChaineErreurs
					)
				);
			}
		}

		protected override bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (this.Saison.AnneeDebut == 0) {
				this.mErreurs.Add(ResErreurs.Saison_AnneeDebutObligatoire);
			}

			if (this.Saison.AnneeFin == 0) {
				this.mErreurs.Add(ResErreurs.Saison_AnneeFinObligatoire);
			}

			if (this.mErreurs.Count != 0 && this.Saison.AnneeDebut >= this.Saison.AnneeFin) {
				this.mErreurs.Add(ResErreurs.Saison_AnneeFinSupAnneeDebut);
			}

			if (!this.EstEdition
				&& this.mErreurs.Count == 0
				&& SaisonDao.GetInstance(ViewModelLocator.Context).Exist(this.Saison)) {

				this.mErreurs.Add(ResErreurs.Saison_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
