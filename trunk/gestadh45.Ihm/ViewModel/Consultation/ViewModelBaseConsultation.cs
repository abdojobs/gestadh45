using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ViewModelBaseConsultation : ViewModelBase
	{
		protected ViewModelBaseConsultation() {
		}

		protected void CreateCreerCommand() {
			this.CreerCommand = new RelayCommand<string>(
				this.ExecuteCreerCommand
			);
		}

		public void ExecuteCreerCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(pCodeUC, "ChangementUserControl"));
		}

		public ICommand CreerCommand { get; set; }
	}
}
