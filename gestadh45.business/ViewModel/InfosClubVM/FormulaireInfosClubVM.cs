using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.InfosClubVM
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
		private IOrderedEnumerable<Ville> _villes;

		/// <summary>
		/// Obitent/Définit la liste des villes
		/// </summary>
		public IOrderedEnumerable<Ville> Villes {
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
			this.PopulatesVilles();

			Messenger.Default.Register<NMRefreshDatas>(this, m => this.PopulatesVilles());
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
				this.repoMain.Save();
				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		private void PopulatesVilles() {
			this.Villes = this.repoVille.GetAll().OrderBy((v) => v.Libelle);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				errors.Add(ResInfosClub.ErrNomObligatoire);
			}

			if (this.InfosClub.Adresse == null || string.IsNullOrWhiteSpace(this.InfosClub.Adresse)) {
				errors.Add(ResInfosClub.ErrAdresseObligatoire);
			}

			if (this.InfosClub.Adresse != null && this.InfosClub.Ville == null) {
				errors.Add(ResInfosClub.ErrVilleObligatoire);
			}

			return errors.Count == 0;
		}
	}
}
