using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.business.ViewModel
{
	public abstract class VMFormulaireBase : VMUCBase
	{
		#region IsEditMode
		private bool _isEditMode;
		
		/// <summary>
		/// Obtient/Définit un booléen indiquant si on est en mode édition (True) ou création (False)
		/// </summary>
		public bool IsEditMode {
			get { return this._isEditMode; }
			set {
				if (this._isEditMode != value) {
					this._isEditMode = value;
					this.RaisePropertyChanged(() => this.IsEditMode);
				}
			}
		}
		#endregion

		#region Errors
		private List<string> _errors;

		/// <summary>
		/// Obtient/Définit la liste des erreurs du formulaire
		/// </summary>
		public List<string> Errors {
			get { return this._errors; }
			set {
				if (this._errors != value) {
					this._errors = value;
					this.RaisePropertyChanged(() => this.Errors);
				}
			}
		}
		#endregion

		public VMFormulaireBase() {
			this.CreateSaveCommand();
			this.CreateCancelCommand();
		}

		protected virtual bool CheckFormValidity(List<string> errors) {
			// par défaut le formulaire est considéré comme valide (à redéfinir si besoin dans les classes filles)
			return true;
		}

		protected virtual bool CurrentElementExists() {
			// par défaut l'élément courant du formulaire n'existe pas déjà en BDD
			return false;
		}

		/// <summary>
		/// Prépare les valeurs du formulaire avant la vérification et l'enregistrement. (à redéfinit dans les classes filles)
		/// </summary>
		protected virtual void PrepareValuesForTreatment() { }

		#region SaveCommand
		public ICommand SaveCommand { get; set; }

		private void CreateSaveCommand() {
			this.SaveCommand = new RelayCommand(
				this.ExecuteSaveCommand, 
				this.CanExecuteSaveCommand
			);
		}

		public virtual bool CanExecuteSaveCommand() {
			// par défaut l'enregistrement est autorisé (à redéfinir si besoin dans les classes filles)
			return true;
		}

		/// <summary>
		/// Execute la commande d'enregistrement. En cas de surcharge, penser à appeler le code de la classe mère (base.ExecuteSaveCommand())
		/// </summary>
		public virtual void ExecuteSaveCommand() {
			this.ClearUserNotifications();

			if (this.IsWindowMode) {
				Messenger.Default.Send(new NMCloseWindow());
			}
			else {
				this.ShowUC(this.UCParentCode);
			}
		}
		#endregion

		#region CancelCommand
		public ICommand CancelCommand { get; set; }

		private void CreateCancelCommand() {
			this.CancelCommand = new RelayCommand(
				this.ExecuteCancelCommand,
				this.CanExecuteCancelCommand
			);
		}

		public virtual bool CanExecuteCancelCommand() {
			// par défaut l'annulation est autorisée (à redéfinir si besoin dans les classes filles)
			return true;
		}

		/// <summary>
		/// Execute la commande d'annulation. En cas de surcharge, penser à appeler le code de la classe mère (base.ExecuteCancelCommand())
		/// </summary>
		public virtual void ExecuteCancelCommand() {
			this.ClearUserNotifications();

			if (this.IsWindowMode) {
				Messenger.Default.Send(new NMCloseWindow());
			}
			else {
				this.ShowUC(this.UCParentCode);
			}
		}
		#endregion

		#region PopupVilleCommand
		public ICommand PopupVilleCommand { get; set; }

		private void CreatePopupVilleCommand() {
			this.PopupVilleCommand = new RelayCommand(
				this.ExecutePopupVilleCommand, 
				this.CanExecutePopupVilleCommand
			);
		}

		public virtual bool CanExecutePopupVilleCommand() {
			return true;
		}

		public virtual void ExecutePopupVilleCommand() {
			this.ExecuteOpenWindowCommand(CodesUC.FormulaireVille);
		}
		#endregion
	}
}
