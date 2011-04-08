using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireVilleUCViewModel : ViewModelBaseFormulaire
	{
		private Ville mVille;

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
			this.Ville = new Ville();
			this.CodeUCOrigine = CodesUC.ConsultationVilles;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !VilleDao.GetInstance(ViewModelLocator.Context).Exist(this.Ville)) {
				VilleDao.GetInstance(ViewModelLocator.Context).Create(this.Ville);

				base.ExecuteEnregistrerCommand();
			}
			else {
				Messenger.Default.Send<NotificationMessageUtilisateur>(
					new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
				);
			}
		}

		protected override bool VerifierSaisie() {
			this.mErreurs = new List<string>();

			if (string.IsNullOrWhiteSpace(this.Ville.Libelle)) {
				this.mErreurs.Add(ResErreurs.Ville_LibelleObligatoire);
			}

			if (string.IsNullOrWhiteSpace(this.Ville.CodePostal)) {
				this.mErreurs.Add(ResErreurs.Ville_CodePostalObligatoire);
			}

			if (!this.EstEdition
				&& this.mErreurs.Count == 0
				&& VilleDao.GetInstance(ViewModelLocator.Context).Exist(this.Ville)) {

					this.mErreurs.Add(ResErreurs.Ville_Existe);
			}

			return this.mErreurs.Count == 0;
		}
	}
}
