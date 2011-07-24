﻿using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireVilleUCViewModel : ViewModelBaseFormulaire
	{
		private Ville mVille;
		private VilleDao _daoVille;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Ville Ville {
			get {
				return this.mVille;
			}
			set {
				if (this.mVille != value) {
					this.mVille = value;
					this.RaisePropertyChanged("Ville");
				}
			}
		}

		public FormulaireVilleUCViewModel() {
			this._daoVille = new VilleDao(ViewModelLocator.DataSource);
			this.Ville = new Ville();
			this.CodeUCOrigine = CodesUC.ConsultationVilles;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !this._daoVille.Exists(this.Ville)) {
				this._daoVille.Create(this.Ville);

				base.ExecuteEnregistrerCommand();

				var msg = new NotificationMessageSelectionElement<Ville>(this.Ville);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreurIhm(this.Erreurs.ToString());
			}
		}

		protected override bool VerifierSaisie() {
			List<string> lErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.Ville.Libelle)) {
				lErreurs.Add(ResErreurs.Ville_LibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.Ville.CodePostal)) {
				lErreurs.Add(ResErreurs.Ville_CodePostalObligatoire);
			}

			if (!this.EstEdition
				&& lErreurs.Count == 0
				&& this._daoVille.Exists(this.Ville)) {

					lErreurs.Add(ResErreurs.Ville_Existe);
			}

			this.Erreurs = new List<string>(lErreurs);

			return this.Erreurs.Count == 0;
		}
	}
}