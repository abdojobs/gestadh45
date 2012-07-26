using System;
using System.Collections.Generic;
using gestadh45.dal;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.business.ViewModel.MainScreenVM
{
	public class FormulaireInitialisationDatabaseVM : VMFormulaireBase
	{

		#region CurrentInfosClub
		private InfosClub _currentInfosClub;

		/// <summary>
		/// Obtient/Définit l'objet InfosClub courant
		/// </summary>
		public InfosClub CurrentInfosClub {
			get { return this._currentInfosClub; }
			set {
				if (this._currentInfosClub != value) {
					this._currentInfosClub = value;
					this.RaisePropertyChanged(() => this.CurrentInfosClub);
				}
			}
		}
		#endregion
				
		
		#region Repository
		private Repository<InfosClub> _repoInfosClub;
		#endregion

		#region constructeur
		public FormulaireInitialisationDatabaseVM() {
			this.UCParentCode = CodesUC.ConsultationInfosClub;
			this.IsEditMode = false;
			this._repoInfosClub = new Repository<InfosClub>(this._context);

			// on supprime toute trace d'infos club existant (i il y en a)
			this.CleanInfosClub();

			this.CurrentInfosClub = new InfosClub();
		}
		#endregion

		private void CleanInfosClub() {
			if (this._repoInfosClub.GetAll().Count > 0) {
				this._repoInfosClub.Delete(this._repoInfosClub.GetFirst());
				this._repoInfosClub.Save();
			}
		}

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentInfosClub.Nom)) {
				errors.Add(ResMainScreen.ErrNomClubObligatoire);
			}
			
			return errors.Count == 0;
		}

		#region CancelCommand
		public override void ExecuteCancelCommand() {
			Messenger.Default.Send(new NMCloseApplication());
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentInfosClub.ID = Guid.NewGuid();
				this._repoInfosClub.Add(this.CurrentInfosClub);
				this._repoInfosClub.Save();

				Messenger.Default.Send(new NMMainMenuState(true));
			
				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
