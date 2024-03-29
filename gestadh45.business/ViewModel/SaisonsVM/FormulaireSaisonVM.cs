﻿using System;
using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.SaisonsVM
{
	public class FormulaireSaisonVM : VMFormulaireBase
	{
		/// <summary>
		/// Durée (en année) d'une saison
		/// </summary>
		private const int DureeSaison = 1;
		
		#region CurrentSaison
		private Saison _currentSaison;

		/// <summary>
		/// Obtient/Définit la saison courante du formulaire
		/// </summary>
		public Saison CurrentSaison {
			get { return this._currentSaison; }
			set {
				if (this._currentSaison != value) {
					this._currentSaison = value;
					this.RaisePropertyChanged(() => this.CurrentSaison);
				}
			}
		}
		#endregion

		#region repository
		private Repository<Saison> repoSaison;
		#endregion

		public FormulaireSaisonVM() {
			this.repoSaison = new Repository<Saison>(this._context);

			this.CurrentSaison = new Saison()
			{
				AnneeDebut = DateTime.Now.Year,
				EstSaisonCourante = false
			};

			this.UCParentCode = CodesUC.ConsultationSaisons;
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this.PrepareValuesForTreatment();
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentSaison.ID = Guid.NewGuid();
				this.repoSaison.Add(this.CurrentSaison);
				this.repoSaison.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		protected override bool CurrentElementExists() {
			return this.repoSaison.GetAll().Where(
				(s) => s.AnneeDebut == this.CurrentSaison.AnneeDebut
			).Count() != 0;
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (this.CurrentSaison.AnneeDebut == 0) {
				errors.Add(ResSaisons.ErrAnneeDebutObligatoire);
			}

			if (this.CurrentSaison.AnneeFin == 0) {
				errors.Add(ResSaisons.ErrAnneeFinObligatoire);
			}

			if (errors.Count != 0 && this.CurrentSaison.AnneeDebut >= this.CurrentSaison.AnneeFin) {
				errors.Add(ResSaisons.ErrAnneeDebutInfAnneeFin);
			}

			if (errors.Count == 0 && this.CurrentElementExists()) {
				errors.Add(ResSaisons.ErrSaisonExiste);
			}

			return errors.Count == 0;
		}

		protected override void PrepareValuesForTreatment() {
			this.CurrentSaison.AnneeFin = this.CurrentSaison.AnneeDebut + DureeSaison;
		}
	}
}
