using gestadh45.dal;

namespace gestadh45.business.ViewModel.InfosClubVM
{
	public class ConsultationInfosClubVM : VMConsultationBase
	{
		#region InfosClub
		private InfosClub _infosClub;

		/// <summary>
		/// Obtient/Définit l'objet contenant les informations du club
		/// </summary>
		public InfosClub InfosClub {
			get { return this._infosClub; }
			set {
				if (this._infosClub != value) {
					this._infosClub = value;
					this.RaisePropertyChanged(() => this.InfosClub);
				}
			}
		}
		#endregion

		public ConsultationInfosClubVM() {
			var repoMain = new Repository<InfosClub>(this._context);

			this.InfosClub = repoMain.GetFirst();

			if (this.InfosClub == null) {
				this.ShowUserNotification(ResInfosClub.ErrTableInfosClubVide);
			}
		}

		public override bool CanExecuteEditCommand() {
			return true;
		}

		public override void ExecuteEditCommand() {
			base.ExecuteEditCommand();
			this.ShowUC(CodesUC.FormulaireInfosClub);
		}
	}
}
