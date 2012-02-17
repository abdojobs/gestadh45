using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using gestadh45.dal;

namespace gestadh45.business.ViewModel.Formulaire
{
	public class FormulaireInfosClubVM : VMFormulaireBase
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

		#region Villes
		private ICollection<Ville> _villes;

		/// <summary>
		/// Obitent/Définit la liste des villes
		/// </summary>
		public ICollection<Ville> Villes {
			get { return this._villes; }
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<InfosClub> repoMain;
		private Repository<Ville> repoVille;
		#endregion

		public FormulaireInfosClubVM() {
			this.UCParentCode = CodesUC.ConsultationInfosClub;

			this.repoMain = new Repository<InfosClub>(this._context);
			this.repoVille = new Repository<Ville>(this._context);

			this.InfosClub = repoMain.GetFirst();
			this.Villes = repoVille.GetAll();
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie, il faut s'assurer de rafraîchir l'objet InfosClub avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			this.repoMain.Reload(this.InfosClub);

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			List<string> errors = new List<string>();
			
			if (this.CheckFormValidity(errors)) {
				this.repoMain.Edit(this.InfosClub);
				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
