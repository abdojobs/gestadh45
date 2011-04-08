using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInfosClubUCViewModel : ViewModelBaseConsultation
	{
		private InfosClub mInfosClub;

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
			this.InfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();
			InfosClubDao.GetInstance(ViewModelLocator.Context).Refresh(this.InfosClub);

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
