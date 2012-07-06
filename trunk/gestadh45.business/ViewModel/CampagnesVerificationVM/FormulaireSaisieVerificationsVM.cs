using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.dal;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.business.ViewModel.CampagnesVerificationVM
{
	public class FormulaireSaisieVerificationsVM : VMFormulaireBase
	{
		#region CurrentCampagneVerification
		private CampagneVerification _currentCampagneVerification;

		/// <summary>
		/// Gets or sets the current campagne verification.
		/// </summary>
		/// <value>
		/// The current campagne verification.
		/// </value>
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

		#region Statutsverification
		private IOrderedEnumerable<StatutVerification> _statutsVerification;

		public IOrderedEnumerable<StatutVerification> StatutsVerification {
			get { return this._statutsVerification; }
			set {
				if (this._statutsVerification != value) {
					this._statutsVerification = value;
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
		public FormulaireSaisieVerificationsVM(Guid idCampagneVerification) {
			this.UCParentCode = CodesUC.ConsultationCampagnesVerification;
			this.IsEditMode = true;

			this._repoCampagneVerification = new Repository<CampagneVerification>(this._context);
			this._repoStatutsVerification = new Repository<StatutVerification>(this._context);

			this.CreateClotureCommand();

			this.StatutsVerification = this._repoStatutsVerification.GetAll().OrderBy(s => !s.EstDefaut).ThenBy(s => s.Libelle);
			this.CurrentCampagneVerification = this._repoCampagneVerification.GetByKey(idCampagneVerification);
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (this.CurrentCampagneVerification.Verifications.Count(v => v.StatutVerification.EstDefaut) > 0) {
				errors.Add(ResCampagnesVerification.ErrEquipementNonVerifie);
			}
			
			return errors.Count == 0;
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet courant avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (this.IsEditMode) {
				this._repoCampagneVerification.Reload(this.CurrentCampagneVerification);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			this._repoCampagneVerification.Edit(this.CurrentCampagneVerification);
			this._repoCampagneVerification.Save();
			
			base.ExecuteSaveCommand();
		}
		#endregion

		#region ClotureCommand
		public ICommand ClotureCommand { get; set; }

		private void CreateClotureCommand() {
			this.ClotureCommand = new RelayCommand(
				this.ExecuteClotureCommand,
				this.CanExecuteClotureCommand
			);
		}

		public bool CanExecuteClotureCommand() {
			return true;
		}

		public void ExecuteClotureCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {
				this.CurrentCampagneVerification.EstFermee = true;
				this.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
