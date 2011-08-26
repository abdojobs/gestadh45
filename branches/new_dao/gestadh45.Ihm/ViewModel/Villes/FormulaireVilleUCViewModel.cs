using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Villes
{
	public class FormulaireVilleUCViewModel : ViewModelBaseFormulaire
	{
		private Ville _ville;
		private IVilleDao _daoVille;

		/// <summary>
		/// Obtient/Définit l'objet du formulaire
		/// </summary>
		public Ville Ville {
			get {
				return this._ville;
			}
			set {
				if (this._ville != value) {
					this._ville = value;
					this.RaisePropertyChanged(() => this.Ville);
				}
			}
		}

		public FormulaireVilleUCViewModel() {
			this._daoVille = DaoFactory.GetVilleDao(ViewModelLocator.DataSource);
			this.Ville = new Ville();
			this.CodeUCOrigine = CodesUC.ConsultationVilles;
		}

		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !this._daoVille.Exists(this.Ville)) {
				int id = this._daoVille.Create(this.Ville);
				this.Ville.Id = id;

				base.ExecuteEnregistrerCommand();

				var msg = new NotificationMessageSelectionElement<Ville>(this.Ville);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
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
