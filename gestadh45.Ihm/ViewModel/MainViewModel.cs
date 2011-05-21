using System;
using System.Windows.Input;
using System.Windows.Media;
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

		private ISaisonDao mDaoSaison;

		private string mInfosDataSource;
		private string mInfosSaisonCourante;
		private string mNotificationsIhm;
		private SolidColorBrush mCouleurNotificationsIhm;

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
		/// Obtient/Définit les notifications à afficher sur l'IHM
		/// </summary>
		public string NotificationsIhm {
			get { return this.mNotificationsIhm; }
			set {
				if (this.mNotificationsIhm != value) {
					this.mNotificationsIhm = value;
					this.RaisePropertyChanged(() => this.NotificationsIhm);
				}
			}
		}

		/// <summary>
		/// Obtient/Définit la couleur des notifications IHM
		/// </summary>
		public SolidColorBrush CouleurNotificationsIhm {
			get { return this.mCouleurNotificationsIhm; }
			set {
				if (this.mCouleurNotificationsIhm != value) {
					this.mCouleurNotificationsIhm = value;
					this.RaisePropertyChanged(() => this.CouleurNotificationsIhm);
				}
			}
		}

		public MainViewModel() {
			this.mDaoSaison = this.mDaoFactory.GetSaisonDao();

			this.CreateAfficherUCCommand();
			this.CreateChangerDataSourceCommand();
			this.CreateAboutBoxCommand();
			this.CreateCreerDatabaseCommand();

			Messenger.Default.Register<NotificationMessage<Saison>>(
				this, 
				this.MajInfosSaisonCourante
			);

			Messenger.Default.Register<NotificationMessageIhm>(
				this,
				this.MajNotificationsIhm
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
			this.NotificationsIhm = string.Empty;

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
		private void MajInfosSaisonCourante(NotificationMessage<Saison> msg) {
			if (msg.Notification.Equals(TypesNotification.ChangementSaisonCourante)) {
				this.InfosSaisonCourante = msg.Content.ToShortString();
			}
		}

		private void MajNotificationsIhm(NotificationMessageIhm msg) {
			this.NotificationsIhm = msg.Notification;

			switch (msg.TypeNotificationIhm) {
				case TypesNotification.Erreur:
					this.CouleurNotificationsIhm = Brushes.Red;
					break;

				case TypesNotification.Information:
					this.CouleurNotificationsIhm = Brushes.Blue;
					break;

				default:
					this.CouleurNotificationsIhm = Brushes.Black;
					break;
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
				}
			}
			catch (Exception exception) {
				this.AfficherErreurIhm(exception.Message);
			}
		}
		#endregion
	}
}
