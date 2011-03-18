using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class ViewModelBaseFormulaire : ViewModelBase
	{
		private bool mAfficherErreurs;
		private bool mEstEdition;

		protected ViewModelBaseFormulaire() {
		}

		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand<string>(
				new Action<string>(this, this.ExecuteAnnulerCommand)
			);
		}

		public virtual void ExecuteAnnulerCommand(string pCodeUc) {
			Messenger.Default.Send<NotificationMessage<string>>(new NotificationMessage<string>(pCodeUc, "ChangementUserControl"));
		}

		public bool AfficherErreurs {
			get {
				return this.mAfficherErreurs;
			}
			set {
				if (this.mAfficherErreurs != value) {
					this.mAfficherErreurs = value;
					this.RaisePropertyChanged("AfficherErreurs");
				}
			}
		}

		public ICommand AnnulerCommand { get; set; }

		public bool EstEdition {
			get {
				return this.mEstEdition;
			}
			set {
				if (this.mEstEdition != value) {
					this.mEstEdition = value;
					this.RaisePropertyChanged("EstEdition");
				}
			}
		}
	}
}
