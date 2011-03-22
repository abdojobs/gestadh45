using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireSaisonUCViewModel : ViewModelBaseFormulaire
	{
		private Saison mSaison;
		private static int DureeSaison = 1;

		public FormulaireSaisonUCViewModel() {
			Saison saison = new Saison
			{
				AnneeDebut = DateTime.Now.Year,
				AnneeFin = DateTime.Now.Year + DureeSaison,
				EstSaisonCourante = 0L
			};
			this.Saison = saison;
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
			if (this.VerifierSaisie() && !SaisonDao.GetInstance(ViewModelLocator.Context).Exist(this.Saison)) {
				SaisonDao.GetInstance(ViewModelLocator.Context).Create(this.Saison);
				Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>("ConsultationSaisons", "ChangementUserControl"));
			}
			else {
				Messenger.Default.Send<NotificationMessageErreur>(
					new NotificationMessageErreur(ResMessages.CodeErreur, this.ChaineErreurs)
				);
			}
		}

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

		public ICommand EnregistrerCommand { get; set; }

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

		private bool VerifierSaisie() {
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
