using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.dao;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.service.Database;

namespace gestadh45.Ihm.ViewModel
{
	public class MainViewModel : ViewModelBaseApplication
	{
		private string mInfosDataSource;
		private string mInfosSaisonCourante;
		private NotificationIhm mNotificationIhm;

		private IDaoFactory _daoFactory;
		
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
		/// Obtient/Définit la liste notification à afficher sur l'IHM
		/// </summary>
		public NotificationIhm NotificationIhm {
			get { 
				return this.mNotificationIhm; 
			}

			set {
				if (this.mNotificationIhm != value) {
					this.mNotificationIhm = value;
					this.RaisePropertyChanged(() => this.NotificationIhm);
				}
			}
		}

		public MainViewModel() {
			this._daoFactory = new DaoFactory();	
			this.mNotificationIhm = new NotificationIhm();

			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			this.CreateCreerDatabaseCommand();
			this.CreateCloseCommand();

			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				this.MajInfosSaisonCourante
			);

			Messenger.Default.Register<MsgNotificationIhm>(
				this,
				(msg) => this.MajNotificationsIhm(msg.Contenu)
			);
		}

		#region AboutBoxCommand
		public ICommand AboutBoxCommand { get; set; }

		private void CreateAboutBoxCommand() {
			this.AboutBoxCommand = new RelayCommand(
				this.ExecuteAboutBoxCommand
			);
		}

		public void ExecuteAboutBoxCommand() {
			Messenger.Default.Send<NotificationMessageAboutBox>(
				new NotificationMessageAboutBox()
			);
		}
		#endregion

		#region ChangerDataSourceCommand
		public ICommand ChangerDataSourceCommand { get; set; }

		private void CreateChangerDataSourceCommand() {
			this.ChangerDataSourceCommand = new RelayCommand(
				this.ExecuteChangerDataSourceCommand
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
		#endregion

		#region CreerDatabaseCommand
		public ICommand CreerDatabaseCommand { get; set; }

		private void CreateCreerDatabaseCommand() {
			this.CreerDatabaseCommand = new RelayCommand(
				this.ExecuteCreerDatabaseCommand
			);
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

		#region CloseCommand
		public ICommand CloseCommand { get; set; }

		private void CreateCloseCommand() {
			this.CloseCommand = new RelayCommand(this.ExecuteCloseCommand);
		}

		public void ExecuteCloseCommand() {
			Messenger.Default.Send(new MsgClose());
		}
		#endregion

		#region gestion des notifications IHM
		private void MajNotificationsIhm(NotificationIhm pNotification) {
			this.NotificationIhm = pNotification;		
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

					// DAO
					this.InitialisationDaos();

					this.InfosSaisonCourante = ViewModelLocator.DaoSaison.ReadSaisonCourante().ToShortString();
					this.ExecuteAfficherUCCommand(CodesUC.ConsultationInfosClub);

					this.AfficherInformationIhm(MainRessources.NotificationOuvertureBase + pFilePath);
				}
			}
			catch (Exception exception) {
				this.AfficherErreurIhm(exception.Message);
			}
		}

		private void InitialisationDaos() {
			ViewModelLocator.DaoAdherent = this._daoFactory.GetAdherentDao();
			ViewModelLocator.DaoGroupe = this._daoFactory.GetGroupeDao();
			ViewModelLocator.DaoInfosClub = this._daoFactory.GetInfosClubDao();
			ViewModelLocator.DaoInscription = this._daoFactory.GetInscriptionDao();
			ViewModelLocator.DaoJourSemaine = this._daoFactory.GetJourSemaineDao();
			ViewModelLocator.DaoSaison = this._daoFactory.GetSaisonDao();
			ViewModelLocator.DaoSexe = this._daoFactory.GetSexeDao();
			ViewModelLocator.DaoStatutInscription = this._daoFactory.GetStatutInscriptionDao();
			ViewModelLocator.DaoTrancheAge = this._daoFactory.GetTrancheAgeDao();
			ViewModelLocator.DaoVille = this._daoFactory.GetVilleDao();
		}
		#endregion
	}
}
