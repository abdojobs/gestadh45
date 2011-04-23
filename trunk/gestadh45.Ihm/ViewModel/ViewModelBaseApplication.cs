﻿using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel
{
	public abstract class ViewModelBaseApplication : ViewModelBase
	{
		public ICommand AnnulerCommand { get; set; }
		public ICommand FenetreCommand { get; set; }

		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de cet élément
		/// </summary>
		public string CodeUCOrigine { get; set; }
		
		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'élément est dans sa propre fenêtre (True) ou dans l'écran principal (False)
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
		/// Envoie une notification à l'utilisateur
		/// </summary>
		/// <param name="pType">Type de notification</param>
		/// <param name="pMessage">Message</param>
		protected void EnvoyerNotificationUtilisateur(string pType, string pMessage) {
			Messenger.Default.Send<NotificationMessageUtilisateur>(
				new NotificationMessageUtilisateur(pType, pMessage)
			);
		}
	}
}