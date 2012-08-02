using System;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireSaisieVerificationsVM : VMFormulaireBase
	{
		#region CurrentCampagneVerification
		private CampagneVerification _currentCampagneVerification;

		/// <summary>
		/// Obtient/Définit la campagne de vérification courante
		/// </summary>
		public CampagneVerification CurrentCampagneVerification {
			get { return this._currentCampagneVerification; }
			set {
				if (this._currentCampagneVerification != value) {
					this._currentCampagneVerification = value;
					this.RaisePropertyChanged(() => this.CurrentCampagneVerification);
				}
			}
		}
		#endregion


		#region SelectedVerification
		private Verification _selectedVerification;

		/// <summary>
		/// Obtient/Définit la vérification sélectionnée
		/// </summary>
		public Verification SelectedVerification {
			get { return this._selectedVerification; }
			set {
				if (this._selectedVerification != value) {
					this._selectedVerification = value;
					this.RaisePropertyChanged(() => this.SelectedVerification);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<CampagneVerification> _repoCampagneVerification;
		#endregion

		#region Constructeur
		public FormulaireSaisieVerificationsVM(Guid idCampagne) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this.CreateRepositories();
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
		}

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
			this._repoCampagneVerification.Save();

			base.ExecuteSaveCommand();
		}
		#endregion
	}
}
