using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.model;
using gestadh45.service.Database;

namespace gestadh45.Ihm.ViewModel
{
	public class MainViewModel : ViewModelBaseApplication
	{
		public ICommand AboutBoxCommand { get; set; }
		public ICommand AfficherUCCommand { get; internal set; }
		public ICommand ChangerDataSourceCommand { get; internal set; }
		public ICommand CreerDatabaseCommand { get; set; }

		private ISaisonDao _daoSaison;

		private string _infosDataSource;
		private string _infosSaisonCourante;
		private NotificationIhm _notificationIhm;
		
		/// <summary>
		/// Obtient/Définit l'information sur le datasource actuel
		/// </summary>
		public string InfosDataSource {
			get {
				return this._infosDataSource;
			}
			set {
				if (this._infosDataSource != value) {
					this._infosDataSource = value;
					this.RaisePropertyChanged(() => this.InfosDataSource);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit l'information sur la saison courante
		/// </summary>
		public string InfosSaisonCourante {
			get {
				return this._infosSaisonCourante;
			}
			set {
				if (this._infosSaisonCourante != value) {
					this._infosSaisonCourante = value;
					this.RaisePropertyChanged(() => this.InfosSaisonCourante);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la liste notification à afficher sur l'IHM
		/// </summary>
		public NotificationIhm NotificationIhm {
			get { 
				return this._notificationIhm; 
			}

			set {
				if (this._notificationIhm != value) {
					this._notificationIhm = value;
					this.RaisePropertyChanged(() => this.NotificationIhm);
				}
			}
		}

		public MainViewModel() {
			this._notificationIhm = new NotificationIhm();

			this.CreateAfficherUCCommand();
			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			this.CreateCreerDatabaseCommand();

			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				(msg)=> this.MajInfosSaisonCourante(msg.Content)
			);

			Messenger.Default.Register<MsgNotificationIhm>(
				this,
				(msg) => this.MajNotificationsIhm(msg.Contenu)
			);
		}

		public bool CanExecuteAfficherUCCommand(string pCodeUC) {
			return !string.IsNullOrWhiteSpace(this.InfosDataSource);
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
		private void MajNotificationsIhm(NotificationIhm pNotification) {
			this.NotificationIhm = pNotification;		
		}
		#endregion

		#region méthodes privees
		private void MajInfosSaisonCourante(Saison pSaison) {
			this.InfosSaisonCourante = pSaison.ToShortString();
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
					ViewModelLocator.DataSource = pFilePath;

					this.InfosDataSource = pFilePath;
					this._daoSaison = DaoFactory.GetSaisonDao(pFilePath);
					this.InfosSaisonCourante = this._daoSaison.ReadSaisonCourante().ToShortString();
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
