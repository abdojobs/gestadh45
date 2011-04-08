using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public abstract class ViewModelBaseFormulaire : ViewModelBaseUC
	{
		private bool mEstEdition;

		protected List<string> mErreurs;

		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de ce formulaire
		/// </summary>
		public string CodeUCOrigine { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si on est en mode édition (True) ou création (False)
		/// </summary>
		public bool EstEdition {
			get {
				return this.mEstEdition;
			}
			set {
				if (this.mEstEdition != value) {
					this.mEstEdition = value;
					this.RaisePropertyChanged("EstEdition");
				}
			}
		}

		/// <summary>
		/// Obtient la liste des erreurs sous forme d'une chaine unique
		/// </summary>
		public string ChaineErreurs {
			get {
				StringBuilder lSb = new StringBuilder();

				foreach (string lErreur in this.mErreurs) {
					lSb.Append(lErreur + "\r\n");
				}

				return lSb.ToString();
			}
		}

		public ICommand AnnulerCommand { get; set; }
		public ICommand EnregistrerCommand { get; set; }
		public ICommand FenetreCommand { get; set; }

		public ViewModelBaseFormulaire() {
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			this.CreateAnnulerCommand();
			this.CreateEnregistrerCommand();
			this.CreateFenetreCommand();
		}

		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand<string>(
				this.ExecuteAnnulerCommand
			);
		}

		protected void CreateEnregistrerCommand() {
			this.EnregistrerCommand = new RelayCommand(
				this.ExecuteEnregistrerCommand,
				this.CanExecuteEnregistrerCommand
			);
		}

		protected void CreateFenetreCommand() {
			this.FenetreCommand = new RelayCommand<string>(
				this.ExecuteFenetreCommand,
				this.CanExecuteFenetreCommand
			);
		}

		public virtual void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.ModeFenetre) {
				Messenger.Default.Send<NotificationMessageFermetureFenetre>(
					new NotificationMessageFermetureFenetre()
				);
			}
			else {
				Messenger.Default.Send<NotificationMessageChangementUC>(
					new NotificationMessageChangementUC(pCodeUc)
				);
			}
		}

		public virtual bool CanExecuteEnregistrerCommand() {
			return true;
		}

		public virtual void ExecuteEnregistrerCommand() {
			if (this.ModeFenetre) {
				Messenger.Default.Send<NotificationMessageFermetureFenetre>(
					new NotificationMessageFermetureFenetre()
				);
			}
			else {
				Messenger.Default.Send<NotificationMessageChangementUC>(
					new NotificationMessageChangementUC(this.CodeUCOrigine)
				);
			}
		}

		public virtual bool CanExecuteFenetreCommand(string pCodeUC) {
			return true;
		}

		public virtual void ExecuteFenetreCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageOuvertureFenetre>(
				new NotificationMessageOuvertureFenetre(pCodeUC)
			);
		}

		protected virtual bool VerifierSaisie() {
			return true;
		}

		/// <summary>
		/// Envoie un message d'erreur avec le contenu de la propriété ChaineErreurs
		/// </summary>
		protected void EnvoyerMessageErreur() {
			Messenger.Default.Send<NotificationMessageUtilisateur>(
				new NotificationMessageUtilisateur(TypesNotification.Erreur, this.ChaineErreurs)
			);
		}
	}
}
