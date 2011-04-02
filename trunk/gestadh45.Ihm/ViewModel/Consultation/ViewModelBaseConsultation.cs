using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public abstract class ViewModelBaseConsultation : ViewModelBase
	{
		protected ViewModelBaseConsultation() {
		}

		protected void CreateCreerCommand() {
			this.CreerCommand = new RelayCommand(
				this.ExecuteCreerCommand
			);
		}

		public abstract void ExecuteCreerCommand();

		public ICommand CreerCommand { get; set; }
	}
}
