﻿using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.Tools;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public abstract class ViewModelBaseFormulaire : ViewModelBaseApplication
	{
		public ICommand EnregistrerCommand { get; set; }
		
		private bool mEstEdition;
		private List<string> mErreurs;

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
		/// Obtient/Définit la liste des erreurs
		/// </summary>
		public List<string> Erreurs {
			get { return this.mErreurs; }
			set {
				if (this.mErreurs != value) {
					this.mErreurs = value;
					this.RaisePropertyChanged("Erreurs");
				}
			}
		}

		public ViewModelBaseFormulaire() {
			this.CreateEnregistrerCommand();

			Messenger.Default.Send(new NotificationMessageTransition(TransitionHelper.TranslationGaucheDroite));
		}

		protected void CreateEnregistrerCommand() {
			this.EnregistrerCommand = new RelayCommand(
				this.ExecuteEnregistrerCommand,
				this.CanExecuteEnregistrerCommand
			);
		}

		public virtual bool CanExecuteEnregistrerCommand() {
			return true;
		}

		public virtual void ExecuteEnregistrerCommand() {
			this.RazNotificationIhm();

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

		protected virtual bool VerifierSaisie() {
			return true;
		}
	}
}
