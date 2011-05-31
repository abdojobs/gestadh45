﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;
using gestadh45.service.Database;
using System.Collections.Generic;

namespace gestadh45.Ihm.ViewModel
{
	public class MainViewModel : ViewModelBaseApplication
	{
		public ICommand AboutBoxCommand { get; set; }
		public ICommand AfficherUCCommand { get; internal set; }
		public ICommand ChangerDataSourceCommand { get; internal set; }
		public ICommand CreerDatabaseCommand { get; set; }

		private ISaisonDao mDaoSaison;

		private string mInfosDataSource;
		private string mInfosSaisonCourante;
		private List<NotificationIhm> mNotificationsIhm;
		
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
					this.RaisePropertyChanged(() => this.InfosDataSource);
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
					this.RaisePropertyChanged(() => this.InfosSaisonCourante);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste des notification à afficher sur l'IHM
		/// </summary>
		public List<NotificationIhm> NotificationsIhm {
			get { 
				return this.mNotificationsIhm; 
			}

			set {
				if (this.mNotificationsIhm != value) {
					this.mNotificationsIhm = value;
					this.RaisePropertyChanged(() => this.NotificationsIhm);
				}
			}
		}

		public MainViewModel() {
			this.mDaoSaison = this.mDaoFactory.GetSaisonDao();
			this.mNotificationsIhm = new List<NotificationIhm>();

			this.CreateAfficherUCCommand();
			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			this.CreateCreerDatabaseCommand();

			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				this.MajInfosSaisonCourante
			);

			Messenger.Default.Register<MsgNotificationIhm>(
				this,
				(msg) => this.MajNotificationsIhm(msg.Contenu)
			);
		}

		public bool CanExecuteAfficherUCCommand(string pCodeUC) {
			return (ObjectContextManager.Context != null);
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
			this.RazNotificationIhm();
			this.AfficherEcran(pCodeUC);
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

		#region gestion des notifications IHM
		private void MajNotificationsIhm(List<NotificationIhm> pNotifications) {
			this.NotificationsIhm = pNotifications;		
		}
		#endregion

		#region méthodes privees
		private void MajInfosSaisonCourante(NotificationMessage<Saison> msg) {
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
				this.AfficherErreurIhm(exception.Message);
			}
		}

		private void ChangeDataSource(string pFilePath) {
			try {
				if (!string.IsNullOrWhiteSpace(pFilePath)) {
					ObjectContextManager.CreateContext(EntitySQLiteHelper.GetConnectionString(pFilePath));

					this.InfosDataSource = EntitySQLiteHelper.GetFilePathFromContext(ObjectContextManager.Context);
					this.InfosSaisonCourante = this.mDaoSaison.ReadSaisonCourante().ToShortString();
					this.ExecuteAfficherUCCommand(CodesUC.ConsultationInfosClub);

					this.AfficherInformationIhm(MainRessources.NotificationOuvertureBase + pFilePath);
				}
			}
			catch (Exception exception) {
				this.AfficherErreurIhm(exception.Message);
			}
		}
		#endregion
	}
}
