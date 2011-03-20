using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireVilleUCViewModel : ViewModelBaseFormulaire
	{
		private Ville mVille;

		public FormulaireVilleUCViewModel() {
			this.Ville = new Ville();
			base.CreateAnnulerCommand();
			this.CreateEnregistrerCommand();
		}

		public bool CanExecuteEnregistrerCommand() {
			return true;
		}

		private void CreateEnregistrerCommand() {
			this.EnregistrerCommand = new RelayCommand(
				this.ExecuteEnregistrerCommand, 
				this.CanExecuteEnregistrerCommand
			);
		}

		public void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !VilleDao.GetInstance(ViewModelLocator.Context).Exist(this.Ville)) {
				VilleDao.GetInstance(ViewModelLocator.Context).Create(this.Ville);
				Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>("ConsultationVilles", "ChangementUserControl"));
			}
			else {
				// TODO verifier message erreur
				Messenger.Default.Send<NotificationMessageErreur>(
					new NotificationMessageErreur("ErrFormSaison", this.ChaineErreurs)
				);
			}
		}

		public ICommand EnregistrerCommand { get; set; }

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

		private bool VerifierSaisie() {
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
