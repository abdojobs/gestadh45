using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
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

		#region StatutsVerification
		private IOrderedEnumerable<StatutVerification> _statutsVerifications;

		/// <summary>
		/// Obtient/Définit la liste des statuts de vérification
		/// </summary>
		public IOrderedEnumerable<StatutVerification> StatutsVerification {
			get { return this._statutsVerifications; }
			set {
				if (this._statutsVerifications != value) {
					this._statutsVerifications = value;
					this.RaisePropertyChanged(() => this.StatutsVerification);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<CampagneVerification> _repoCampagneVerification;
		private Repository<StatutVerification> _repoStatutsVerification;
		#endregion

		#region Constructeur
		public FormulaireSaisieVerificationsVM(Guid idCampagne) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this.CreateRepositories();
			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagne);
			this.PopulateCombos();

			this.CreateValidateCommand();
		}
		#endregion

		private void CreateRepositories() {
			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
			this._repoStatutsVerification = new Repository<StatutVerification>(this._context);
		}

		private void PopulateCombos() {			
			this.StatutsVerification = this._repoStatutsVerification.GetAll().OrderBy(s => s.EstDefaut).ThenBy(s => s.Libelle);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			foreach (Verification verif in this.CurrentCampagneVerification.Verifications) {
				if (verif.StatutVerification.EstCommentaireObligatoire && string.IsNullOrWhiteSpace(verif.Commentaire)) {
					errors.Add(string.Format(ResCampagnesVerification.ErrCommentaireObligatoire, verif.Equipement.Libelle));
				}
			}

			return errors.Count == 0;
		}

		#region CancelCommand
		public override void ExecuteCancelCommand() {
			if (IsEditMode) {
				this._repoCampagneVerification.Reload(this.CurrentCampagneVerification);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
				this._repoCampagneVerification.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
		#region ValidateCommand
		public ICommand ValidateCommand { get; set; }

		private void CreateValidateCommand() {
			this.ValidateCommand = new RelayCommand(
				this.ExecuteValidateCommand,
				this.CanExecuteValidateCommand
			);
		}

		public bool CanExecuteValidateCommand() {
			// On ne peut valider une campagne que si toutes ses vérifications ont été saisies (i.e. : aucune vérif n'a le statut par défaut)
			return this.CurrentCampagneVerification.Verifications.Count(v => v.StatutVerification.EstDefaut) == 0;
		}

		public void ExecuteValidateCommand() {
			this.CurrentCampagneVerification.EstValidee = true;
			this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
			this._repoCampagneVerification.Save();

			Messenger.Default.Send(new NMShowUC(CodesUC.ConsultationCampagnesVerification));
		}
		#endregion
	}
}
