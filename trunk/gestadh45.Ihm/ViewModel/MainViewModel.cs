﻿using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;
using gestadh45.service.Database;

namespace gestadh45.Ihm.ViewModel
{
	public class MainViewModel : ViewModelBaseApplication
	{
		public ICommand AboutBoxCommand { get; set; }
		public ICommand AfficherUCCommand { get; internal set; }
		public ICommand ChangerDataSourceCommand { get; internal set; }
		public ICommand CreerDatabaseCommand { get; set; }
		
		private string mInfosDataSource;
		private string mInfosSaisonCourante;

		/// <summary>
		/// Obtient/Définit l'information sur le datasource actuel
		/// </summary>
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

		/// <summary>
		/// Obtient/Définit l'information sur la saison courante
		/// </summary>
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

		public MainViewModel() {
			this.CreateAfficherUCCommand();
			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			this.CreateCreerDatabaseCommand();

			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				this.NotificationChangementSaisonCourante
			);
		}

		public bool CanExecuteAfficherUCCommand(string pCodeUC) {
			return (ViewModelLocator.Context != null);
		}

		#region création des commandes
		private void CreateAboutBoxCommand() {
			this.AboutBoxCommand = new RelayCommand(
				this.ExecuteAboutBoxCommand
			);
		}

		private void CreateChangerDataSourceCommand() {
			this.ChangerDataSourceCommand = new RelayCommand(
				this.ExecuteChangerDataSourceCommand
			);
		}

		private void CreateAfficherUCCommand() {
			this.AfficherUCCommand = new RelayCommand<string>(
				this.ExecuteAfficherUCCommand, 
				this.CanExecuteAfficherUCCommand
			);
		}

		private void CreateCreerDatabaseCommand() {
			this.CreerDatabaseCommand = new RelayCommand(
				this.ExecuteCreerDatabaseCommand
			);
		}
		#endregion

		#region exécution des commandes
		public void ExecuteAboutBoxCommand() {
			Messenger.Default.Send<NotificationMessageAboutBox>(
				new NotificationMessageAboutBox()
			);
		}

		public void ExecuteAfficherUCCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(pCodeUC)
			);
		}

		public void ExecuteChangerDataSourceCommand() {
			NotificationMessageActionFileDialog<string> message = 
				new NotificationMessageActionFileDialog<string>(
					TypesNotification.OpenFileDialog, 
					MainRessources.ExtensionBase, 
					string.Empty,
					this.ChangeDataSource
				);
			Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
		}

		public void ExecuteCreerDatabaseCommand() {
			NotificationMessageActionFileDialog<string> message =
				new NotificationMessageActionFileDialog<string>(
					TypesNotification.SaveFileDialog,
					MainRessources.ExtensionBase,
					string.Empty,
					this.CreerDatabase
				);

			Messenger.Default.Send<NotificationMessageActionFileDialog<string>>(message);
		}
		#endregion

		#region méthodes privees
		private void NotificationChangementSaisonCourante(NotificationMessage<Saison> msg) {
			if (msg.Notification.Equals(TypesNotification.ChangementSaisonCourante)) {
				this.InfosSaisonCourante = msg.Content.ToShortString();
			}
		}

		private void CreerDatabase(string pFilePath) {
			try {
				if (!string.IsNullOrWhiteSpace(pFilePath)) {
					GenerateurDatabase lGen = new GenerateurDatabase(pFilePath);
					lGen.CreateDatabase();

					// une fois la nouvelle base créée, on l'ouvre
					this.ChangeDataSource(pFilePath);
				}
			}
			catch (Exception exception) {
				NotificationMessageUtilisateur message = new NotificationMessageUtilisateur(
					TypesNotification.Erreur,
					exception.Message
				);
				Messenger.Default.Send<NotificationMessageUtilisateur>(message);
			}
		}

		private void ChangeDataSource(string pFilePath) {
			try {
				if (!string.IsNullOrWhiteSpace(pFilePath)) {
					ViewModelLocator.Context = new Entities(EntitySQLiteHelper.GetConnectionString(pFilePath));
					this.InfosDataSource = EntitySQLiteHelper.GetFilePathFromContext(ViewModelLocator.Context);
					this.InfosSaisonCourante = SaisonDao.GetInstance(ViewModelLocator.Context).ReadSaisonCourante().ToShortString();
					this.ExecuteAfficherUCCommand(CodesUC.ConsultationInfosClub);
				}
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
		#endregion
	}
}