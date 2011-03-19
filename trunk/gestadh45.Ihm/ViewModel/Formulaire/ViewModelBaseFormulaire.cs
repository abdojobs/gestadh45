using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Text;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class ViewModelBaseFormulaire : ViewModelBase
	{
		private bool mEstEdition;

		protected List<string> mErreurs;

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

		/// <summary>
		/// Obtient la liste des erreurs sous forme d'une chaine unique
		/// </summary>
		public string ChaineErreurs {
			get {
				StringBuilder lSb = new StringBuilder();

				foreach (string lErreur in this.mErreurs) {
					lSb.Append(lErreur + "\r\n");
				}

				return lSb.ToString();
			}
		}
	}
}
