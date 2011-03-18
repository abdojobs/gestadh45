using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

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
				new Action<NotificationMessage<Saison>>(
					this, 
					this.NotificationChangementSaisonCourante
				)
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
				this.ExecuteAfficherUCCommand("ConsultationInfosClub");
			}
			catch (Exception exception) {
				ViewModelLocator.Context = null;
				NotificationMessageErreur message = new NotificationMessageErreur("Erreur", exception.Message);
				Messenger.Default.Send<NotificationMessageErreur>(message);
			}
		}

		private void CreateAboutBoxCommand() {
			this.AboutBoxCommand = new RelayCommand(
				new Action(this, this.ExecuteAboutBoxCommand));
		}

		private void CreateAfficherUCCommand() {
			this.AfficherUCCommand = new RelayCommand<string>(
				new Action<string>(this, this.ExecuteAfficherUCCommand), new Predicate<string>(this, this.CanExecuteAfficherUCCommand));
		}

		private void CreateChangerDataSourceCommand() {
			this.ChangerDataSourceCommand = new RelayCommand(
				new Action(this, this.ExecuteChangerDataSourceCommand));
		}

		public void ExecuteAboutBoxCommand() {
			Messenger.Default.Send<NotificationMessage>(new NotificationMessage("AboutBox"));
		}

		public void ExecuteAfficherUCCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(pCodeUC, "ChangementUserControl"));
		}

		public void ExecuteChangerDataSourceCommand()
		{
			// TODO sortir les chaines de caracteres
			NotificationMessageActionFileDialog<string> message = 
				new NotificationMessageActionFileDialog<string>(
					"OpenFileDialog", 
					".eyb", 
					string.Empty, 
					new Action<string>(this, this.ExecuteChangerDataSourceCommand)
				);
			Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
		}

		private void NotificationChangementSaisonCourante(NotificationMessage<Saison> msg) {
			if (msg.Notification.Equals("ChangementSaisonCourante")) {
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
