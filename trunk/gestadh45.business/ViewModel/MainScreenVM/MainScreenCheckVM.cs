using System.Linq;
using gestadh45.dal;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.business.ViewModel.MainScreenVM
{
	public class MainScreenCheckVM : VMUCBase
	{
		#region Repositories
		private Repository<InfosClub> _repoInfosClub;
		#endregion

		#region Constructeurs
		public MainScreenCheckVM() {

		}
		#endregion

		public void DoCheck() {
			

			this._repoInfosClub = new Repository<InfosClub>(this._context);

			if (this.CheckDatabase()) {
				Messenger.Default.Send(new NMMainMenuState(true));
				this.ExecuteShowUCCommand(CodesUC.ConsultationInfosClub);
			}
			else {
				Messenger.Default.Send(new NMMainMenuState(false));
				this.ExecuteShowUCCommand(CodesUC.FormulaireInitialisationDatabase);
			}
		}

		/// <summary>
		/// Vérifie la présence de infos obligatoires dans la BDD (ID infos club et Nom club)
		/// </summary>
		/// <returns>True si les infos sont présentes, False sinon</returns>
		private bool CheckDatabase() {
			var result = false;
			var infosClub = this._repoInfosClub.GetFirst();

			result = infosClub != null;
			result = result && infosClub.ID != null;
			result = result && !string.IsNullOrWhiteSpace(infosClub.Nom);

			return result;
		}
	}
}
