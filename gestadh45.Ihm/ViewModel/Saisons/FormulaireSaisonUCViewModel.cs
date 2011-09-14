using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Saisons
{
	public class FormulaireSaisonUCViewModel : ViewModelBaseFormulaire
	{
		private Saison mSaison;
		private const int DureeSaison = 1;
		private ISaisonDao mDaoSaison;

		/// <summary>
		/// Obtient/Définit l'année de début de la saison
		/// </summary>
		public int AnneeDebutIhm {
			get {
				return (int)this.Saison.AnneeDebut;
			}
			set {
				if (this.Saison.AnneeDebut != value) {
					this.Saison.AnneeDebut = value;
					this.Saison.AnneeFin = value + DureeSaison;
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

		public FormulaireSaisonUCViewModel() {
			this.mDaoSaison = this.mDaoFactory.GetSaisonDao();

			this.Saison = new Saison
			{
				AnneeDebut = DateTime.Now.Year,
				AnneeFin = DateTime.Now.Year + DureeSaison,
				EstSaisonCourante = false
			};

			this.CodeUCOrigine = CodesUC.ConsultationSaisons;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !this.mDaoSaison.Exists(this.Saison)) {
				this.mDaoSaison.Create(this.Saison);

				base.ExecuteEnregistrerCommand();

				var msg = new MsgSelectionElement<Saison>(this.Saison);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (this.Saison.AnneeDebut == 0) {
				lErreurs.Add(ResErreurs.Saison_AnneeDebutObligatoire);
			}

			if (this.Saison.AnneeFin == 0) {
				lErreurs.Add(ResErreurs.Saison_AnneeFinObligatoire);
			}

			if (lErreurs.Count != 0 && this.Saison.AnneeDebut >= this.Saison.AnneeFin) {
				lErreurs.Add(ResErreurs.Saison_AnneeFinSupAnneeDebut);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this.mDaoSaison.Exists(this.Saison)) {

					lErreurs.Add(ResErreurs.Saison_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
