using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ViewModelBaseConsultation : ViewModelBase
	{
		protected ViewModelBaseConsultation() {
		}

		protected void CreateCreerCommand() {
			this.CreerCommand = new RelayCommand(
				this.ExecuteCreerCommand
			);
		}

		public virtual void ExecuteCreerCommand() { }

		public ICommand CreerCommand { get; set; }
	}
}
