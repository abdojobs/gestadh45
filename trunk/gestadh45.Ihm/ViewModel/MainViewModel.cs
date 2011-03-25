using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private string mInfosDataSource;
		private string mInfosSaisonCourante;

		public MainViewModel() {
			this.CreateAfficherUCCommand();
			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				this.NotificationChangementSaisonCourante
			);
		}

		public bool CanExecuteAfficherUCCommand(string pCodeUC) {
			return (ViewModelLocator.Context != null);
		}

		private void ChangeDataSource(string pFilePath) {
			try {
				ViewModelLocator.Context = new Entities(EntitySQLiteHelper.GetConnectionString(pFilePath));
				this.InfosDataSource = EntitySQLiteHelper.GetFilePathFromContext(ViewModelLocator.Context);
				this.InfosSaisonCourante = SaisonDao.GetInstance(ViewModelLocator.Context).ReadSaisonCourante().ToShortString();
				this.ExecuteAfficherUCCommand(CodesUC.ConsultationInfosClub);
			}
			catch (Exception exception) {
				ViewModelLocator.Context = null;
				NotificationMessageUtilisateur message = new NotificationMessageUtilisateur(
					TypesNotification.Erreur, 
					exception.Message
				);
				Messenger.Default.Send<NotificationMessageUtilisateur>(message);
			}
		}

		private void CreateAboutBoxCommand() {
			this.AboutBoxCommand = new RelayCommand(
				this.ExecuteAboutBoxCommand
			);
		}

		private void CreateAfficherUCCommand() {
			this.AfficherUCCommand = new RelayCommand<string>(
				this.ExecuteAfficherUCCommand, 
				this.CanExecuteAfficherUCCommand
			);
		}

		private void CreateChangerDataSourceCommand() {
			this.ChangerDataSourceCommand = new RelayCommand(
				this.ExecuteChangerDataSourceCommand
			);
		}

		public void ExecuteAboutBoxCommand() {
			Messenger.Default.Send<NotificationMessage>(
				new NotificationMessage(TypesNotification.AboutBox)
			);
		}

		public void ExecuteAfficherUCCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessage<string>>(
				new NotificationMessage<string>(pCodeUC, TypesNotification.ChangementUC)
			);
		}

		public void ExecuteChangerDataSourceCommand()
		{
			NotificationMessageActionFileDialog<string> message = 
				new NotificationMessageActionFileDialog<string>(
					TypesNotification.OpenFileDialog, 
					MainRessources.ExtensionBase, 
					string.Empty,
					this.ChangeDataSource
				);
			Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
		}

		private void NotificationChangementSaisonCourante(NotificationMessage<Saison> msg) {
			if (msg.Notification.Equals(TypesNotification.ChangementSaisonCourante)) {
				this.InfosSaisonCourante = msg.Content.ToShortString();
			}
		}

		public ICommand AboutBoxCommand { get; set; }

		public ICommand AfficherUCCommand { get; internal set; }

		public ICommand ChangerDataSourceCommand { get; internal set; }

		public string InfosDataSource {
			get {
				return this.mInfosDataSource;
			}
			set {
				if (this.mInfosDataSource != value) {
					this.mInfosDataSource = value;
					this.RaisePropertyChanged("InfosDataSource");
				}
			}
		}

		public string InfosSaisonCourante {
			get {
				return this.mInfosSaisonCourante;
			}
			set {
				if (this.mInfosSaisonCourante != value) {
					this.mInfosSaisonCourante = value;
					this.RaisePropertyChanged("InfosSaisonCourante");
				}
			}
		}
	}
}
