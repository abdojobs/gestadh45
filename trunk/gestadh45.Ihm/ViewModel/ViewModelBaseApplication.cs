using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;
using System;

namespace gestadh45.Ihm.ViewModel
{
	public abstract class ViewModelBaseApplication : ViewModelBase
	{
		public ICommand AnnulerCommand { get; set; }
		public ICommand FenetreCommand { get; set; }

		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de ce formulaire
		/// </summary>
		public string CodeUCOrigine { get; set; }
		
		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'UC est dans sa propre fenêtre (True) ou dans l'écran principal (False)
		/// </summary>
		public bool ModeFenetre { get; set; }

		public ViewModelBaseApplication() {
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			this.CreateAnnulerCommand();
			this.CreateFenetreCommand();
		}

		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand<string>(
				this.ExecuteAnnulerCommand
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

		public virtual bool CanExecuteFenetreCommand(string pCodeUC) {
			return true;
		}

		public virtual void ExecuteFenetreCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageOuvertureFenetre>(
				new NotificationMessageOuvertureFenetre(pCodeUC)
			);
		}

		/// <summary>
		/// Envoie une notification utilisateur contenant le message de l'exception et éventuellement quitte l'application
		/// </summary>
		/// <param name="pMessage">Message de l'exception</param>
		/// <param name="pEstCritique">Booléen indiquant si il faut quitter l'application ou non</param>
		protected void AfficherException(Exception pException, bool pQuitterApplication) {
			Messenger.Default.Send<NotificationMessageException>(
				new NotificationMessageException(pException, pQuitterApplication)
			);
		}
	}
}
