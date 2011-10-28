using gestadh45.dal;

namespace gestadh45.Ihm.ViewModel.InfosClubs
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
			this.InfosClub = ViewModelLocator.DaoInfosClub.Read();
			ViewModelLocator.DaoInfosClub.Refresh(this.InfosClub);

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
