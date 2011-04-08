using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public abstract class ViewModelBaseConsultation : ViewModelBaseUC
	{
		public ICommand CreerCommand { get; set; }
		public ICommand EditerCommand { get; set; }
		public ICommand SupprimerCommand { get; set; }
		
		protected ViewModelBaseConsultation() {
			this.CreateCreerCommand();
			this.CreateEditerCommand();
			this.CreateSupprimerCommand();
		}

		protected void CreateCreerCommand() {
			this.CreerCommand = new RelayCommand(
				this.ExecuteCreerCommand
			);
		}

		protected void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand(
				this.ExecuteEditerCommand,
				this.CanExecuteEditerCommand
			);
		}

		protected void CreateSupprimerCommand() {
			this.SupprimerCommand = new RelayCommand(
				this.ExecuteSupprimerCommand,
				this.CanExecuteSupprimerCommand
			);
		}

		public virtual bool CanExecuteEditerCommand() {
			return false;
		}

		public virtual bool CanExecuteSupprimerCommand() {
			return true;
		}

		public virtual void ExecuteCreerCommand() { }
		public virtual void ExecuteEditerCommand() { }
		public virtual void ExecuteSupprimerCommand() { }
	}
}
