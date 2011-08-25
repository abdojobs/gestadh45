using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInfosClubUCViewModel : ViewModelBaseConsultation
	{
		private InfosClub _infosClub;

		private IInfosClubDao _daoInfosClub;

		/// <summary>
		/// Obtient/Définit l'objet à afficher
		/// </summary>
		public InfosClub InfosClub {
			get {
				return this._infosClub;
			}
			set {
				if (this._infosClub != value) {
					this._infosClub = value;
					this.RaisePropertyChanged(()=>this.InfosClub);
				}
			}
		}

		public ConsultationInfosClubUCViewModel() {
			this._daoInfosClub = DaoFactory.GetInfosClubDao(ViewModelLocator.DataSource);
			this.InfosClub = this._daoInfosClub.Read();
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
