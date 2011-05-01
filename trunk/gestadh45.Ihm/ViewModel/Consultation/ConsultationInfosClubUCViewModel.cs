using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInfosClubUCViewModel : ViewModelBaseConsultation
	{
		private InfosClub mInfosClub;

		private IInfosClubDao mDaoInfosClub;

		/// <summary>
		/// Obtient/Définit l'objet à afficher
		/// </summary>
		public InfosClub InfosClub {
			get {
				return this.mInfosClub;
			}
			set {
				if (this.mInfosClub != value) {
					this.mInfosClub = value;
					this.RaisePropertyChanged("InfosClub");
				}
			}
		}

		public ConsultationInfosClubUCViewModel() {
			this.mDaoInfosClub = this.mDaoFactory.GetInfosClubDao();

			this.InfosClub = this.mDaoInfosClub.Read();
			this.mDaoInfosClub.Refresh(this.InfosClub);

			this.CreateEditerCommand();
		}

		public override bool CanExecuteEditerCommand() {
			return true;
		}

		public override void ExecuteEditerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireInfosClub)
			);
		}
	}
}
