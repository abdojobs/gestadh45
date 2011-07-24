using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInfosClubUCViewModel : ViewModelBaseConsultation
	{
		private InfosClub mInfosClub;

		private InfosClubDao mDaoInfosClub;

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
			this.mDaoInfosClub = new InfosClubDao(ViewModelLocator.DataSource);
			this.InfosClub = this.mDaoInfosClub.Read(0);
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
