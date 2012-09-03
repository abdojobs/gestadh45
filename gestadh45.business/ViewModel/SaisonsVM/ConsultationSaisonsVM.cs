using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.SaisonsVM
{
	public class ConsultationSaisonsVM : GenericConsultationVM<Saison>
	{
		public ConsultationSaisonsVM() : base() {
			this.CreateSetSaisonCouranteCommand();
		}

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireSaison));
		}
		#endregion

		#region SetSaisonCouranteCommand
		public ICommand SetSaisonCouranteCommand { get; set; }
		
		private void CreateSetSaisonCouranteCommand() {
			this.SetSaisonCouranteCommand = new RelayCommand<Saison>(
				this.ExecuteSetSaisonCouranteCommand,
				this.CanExecuteSetSaisonCouranteCommand
				);
		}

		public bool CanExecuteSetSaisonCouranteCommand(Saison saison) {
			return saison != null && !saison.EstSaisonCourante;
		}

		public void ExecuteSetSaisonCouranteCommand(Saison saison) {
			if (saison != null) {
				// on récupère l'ancienne saison courante et on lui retire l'attribut
				Saison oldSaisonCourante = this._repoMain.GetAll().FirstOrDefault((s)=>s.EstSaisonCourante);
				oldSaisonCourante.EstSaisonCourante = false;
				this._repoMain.Edit(oldSaisonCourante);

				// on positionne l'attribut sur la saison sélectionnée
				saison.EstSaisonCourante = true;

				// on enregistre les changements
				this._repoMain.Save();

				// on rafraichit la liste des saisons
				this.PopulateItems();
					
				// on rafraîchit l'affichage des détails de la saison
				this.SelectedItem = null;
				this.SelectedItem = saison;

				// on notifie l'utilisateur
				Messenger.Default.Send<NMUserNotification>(new NMUserNotification(ResSaisons.InfoSetSaisonCourante));
			}
		}
		#endregion
	}
}
