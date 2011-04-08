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

		public ICommand AnnulerCommand { get; set; }

		protected ViewModelBaseFormulaire() {
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;
		}

		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand<string>(
				this.ExecuteAnnulerCommand
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

		/// <summary>
		/// Méthode appellee une fois l'enregistrement terminé avec succes
		/// Envoie le message de fermeture de fenetre ou de changement UC en fonction du cas d'utilisation
		/// </summary>
		protected void SuiteEnregistrementOk() {
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
	}
}
