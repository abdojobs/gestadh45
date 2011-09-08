using gestadh45.dal;
using gestadh45.dao;

namespace gestadh45.Ihm.ViewModel.InfosClubs
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
			base.ExecuteEditerCommand();

			this.AfficherEcran(CodesUC.FormulaireInfosClub);
		}
	}
}
