using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel
{
	public class MainViewModel : VMApplicationBase
	{
		#region InfosSaisonCourante
		private string _infosSaisonCourante;

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
		#endregion

		#region UserNotification
		private string _userNotification;

		/// <summary>
		/// Obtient/Définit la notification à afficher à l'utilisateur
		/// </summary>
		public string UserNotification {
			get { return this._userNotification; }
			set {
				if (this._userNotification != value) {
					this._userNotification = value;
					this.RaisePropertyChanged(() => this.UserNotification);
				}
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel() {
			this.CreateAboutBoxCommand();
			this.CreateCloseCommand();

			Messenger.Default.Register<NMShowInfosSaisonCourante>(
				this,
				(msg) => this.UpdateInfosSaisonCourante(msg.Content)
			);

			Messenger.Default.Register<NMUserNotification>(
				this,
				(msg) => this.UpdateUserNotification(msg.Text)
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
			Messenger.Default.Send<NMShowAboutBox>(
				new NMShowAboutBox()
			);
		}
		#endregion

		#region CloseCommand
		public ICommand CloseCommand { get; set; }

		private void CreateCloseCommand() {
			this.CloseCommand = new RelayCommand(
				this.ExecuteCloseCommand, 
				this.CanExecuteCloseCommand
			);
		}

		public bool CanExecuteCloseCommand() {
			return true;
		}

		public void ExecuteCloseCommand() {
			Messenger.Default.Send(new NMCloseApplication());
		}
		#endregion

		private void UpdateInfosSaisonCourante(Saison saison) {
			this.InfosSaisonCourante = saison.ToShortString();
		}

		private void UpdateUserNotification(string notification) {
			this.UserNotification = notification;
		}
	}
}