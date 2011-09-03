﻿using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.Tools;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public abstract class ViewModelBaseConsultation : ViewModelBaseUC
	{
		public ICommand CreerCommand { get; set; }
		public ICommand EditerCommand { get; set; }
		public ICommand SupprimerCommand { get; set; }
		public ICommand AfficherDetailsCommand { get; set; }
		public ICommand FiltrerListeCommand { get; set; }
		
		protected ViewModelBaseConsultation() {
			this.CreateCreerCommand();
			this.CreateEditerCommand();
			this.CreateSupprimerCommand();
			this.CreateAfficherDetailsCommand();
			this.CreateFiltrerListeCommand();

			Messenger.Default.Send(new NotificationMessageTransition(TransitionHelper.TranslationDroiteGauche));
		}

		protected void CreateCreerCommand() {
			this.CreerCommand = new RelayCommand(
				this.ExecuteCreerCommand
			);
		}

		protected void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand(
				this.ExecuteEditerCommand,
				this.CanExecuteEditerCommand
			);
		}

		protected void CreateSupprimerCommand() {
			this.SupprimerCommand = new RelayCommand(
				this.ExecuteSupprimerCommand,
				this.CanExecuteSupprimerCommand
			);
		}

		protected void CreateAfficherDetailsCommand() {
			this.AfficherDetailsCommand = new RelayCommand<object>(
				this.ExecuteAfficherDetailsCommand
			);
		}

		protected void CreateFiltrerListeCommand() {
			this.FiltrerListeCommand = new RelayCommand<string>(
				this.ExecuteFiltrerListeCommand
			);
		}

		public virtual bool CanExecuteEditerCommand() {
			return false;
		}

		public virtual bool CanExecuteSupprimerCommand() {
			return true;
		}

		// méthodes à redéfinir dans les VM qui les utilisent
		public virtual void ExecuteCreerCommand() {
			this.RazNotificationIhm();
		}

		public virtual void ExecuteEditerCommand() {
			this.RazNotificationIhm();
		}

		public virtual void ExecuteSupprimerCommand() { }
		public virtual void ExecuteAfficherDetailsCommand(object pObjet) { }
		public virtual void ExecuteFiltrerListeCommand(string pFiltre) { }
	}
}
