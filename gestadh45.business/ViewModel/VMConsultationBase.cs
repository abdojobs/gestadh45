using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.business.ViewModel
{
	public abstract class VMConsultationBase : VMUCBase
	{
		public VMConsultationBase() {
			this.CreateCreateCommand();
			this.CreateEditCommand();
			this.CreateDeleteCommand();
			this.CreateShowDetailsCommand();
			this.CreateFilterCommand();
		}
		
		#region CreateCommand
		public ICommand CreateCommand { get; set; }

		private void CreateCreateCommand() {
			this.CreateCommand = new RelayCommand(
				this.ExecuteCreateCommand, 
				this.CanExecuteCreateCommand
			);
		}

		public virtual bool CanExecuteCreateCommand() {
			// la création est autorisée dans tous les cas
			return true;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteCreateCommand() { }
		#endregion

		#region EditCommand
		public ICommand EditCommand { get; set; }
		
		private void CreateEditCommand() {
			this.EditCommand = new RelayCommand(
				this.ExecuteEditCommand, 
				this.CanExecuteEditCommand
			);
		}

		public virtual bool CanExecuteEditCommand() {
			// l'édition est interdite par défaut (à redéfinir si besoin dans les classes filles)
			return false;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteEditCommand() { }
		#endregion

		#region DeleteCommand
		public ICommand DeleteCommand { get; set; }

		private void CreateDeleteCommand() {
			this.DeleteCommand = new RelayCommand(
				this.ExecuteDeleteCommand, 
				this.CanExecuteDeleteCommand
			);
		}

		public virtual bool CanExecuteDeleteCommand() {
			// la suppression est autorisée par défaut (à redéfinir si besoin dans les classes filles)
			return true;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteDeleteCommand() { }
		#endregion

		#region ShowDetailsCommand
		public ICommand ShowDetailsCommand { get; set; }

		private void CreateShowDetailsCommand() {
			this.ShowDetailsCommand = new RelayCommand<object>(
				this.ExecuteShowDetailsCommand, 
				this.CanExecuteShowDetailsCommand
			);
		}

		public virtual bool CanExecuteShowDetailsCommand(object selectedItem) {
			// l'affichage des détails est autorisé par défaut (à redéfinir si besoin dans les classes filles)
			return true;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteShowDetailsCommand(object selectedItem) { }
		#endregion

		#region FilterCommand
		public ICommand FilterCommand { get; set; }

		private void CreateFilterCommand() {
			this.FilterCommand = new RelayCommand<string>(
				this.ExecuteFilterCommand, 
				this.CanExecuteFilterCommand
			);
		}

		public virtual bool CanExecuteFilterCommand(string filtre) {
			// le filtrage est autorisé par défaut (à redéfinir si besoin dans les classes filles)
			return true;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteFilterCommand(string filtre) { }
		#endregion
	}
}
