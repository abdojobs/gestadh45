using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelBaseUC : ViewModelBaseApplication
	{
		public ICommand AnnulerCommand { get; set; }

		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de cet élément
		/// </summary>
		public string CodeUCOrigine { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'élément est dans sa propre fenêtre (True) ou dans l'écran principal (False)
		/// </summary>
		public bool ModeFenetre { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		public ViewModelBaseUC() {
			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;
			this.CreateAnnulerCommand();
		}

		#region AnnulerCommand
		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand(
				this.ExecuteAnnulerCommand
			);
		}

		public virtual void ExecuteAnnulerCommand() {
			this.RazNotificationIhm();

			if (this.ModeFenetre) {
				Messenger.Default.Send<NotificationMessageFermetureFenetre>(
					new NotificationMessageFermetureFenetre()
				);
			}
			else {
				this.AfficherEcran(this.CodeUCOrigine);
			}
		}
		#endregion
	}
}
