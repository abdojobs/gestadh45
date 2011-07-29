using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireSaisonUCViewModel : ViewModelBaseFormulaire
	{
		private Saison _saison;
		private const int DureeSaison = 1;
		private ISaisonDao _daoSaison;

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
				return this._saison;
			}
			set {
				if (this._saison != value) {
					this._saison = value;
					this.RaisePropertyChanged("Saison");
				}
			}
		}

		public FormulaireSaisonUCViewModel() {
			this._daoSaison = DaoFactory.GetSaisonDao(ViewModelLocator.DataSource);

			this.Saison = new Saison
			{
				AnneeDebut = DateTime.Now.Year,
				AnneeFin = DateTime.Now.Year + DureeSaison,
				EstSaisonCourante = false
			};

			this.CodeUCOrigine = CodesUC.ConsultationSaisons;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !this._daoSaison.Exists(this.Saison)) {
				this._daoSaison.Create(this.Saison);

				base.ExecuteEnregistrerCommand();

				var msg = new NotificationMessageSelectionElement<Saison>(this.Saison);
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
				&& this._daoSaison.Exists(this.Saison)) {

					lErreurs.Add(ResErreurs.Saison_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}
