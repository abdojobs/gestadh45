﻿using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationInfosClubUCViewModel : ViewModelBaseConsultation
	{
		private InfosClub mInfosClub;

		public ConsultationInfosClubUCViewModel() {
			this.InfosClub = InfosClubDao.GetInstance(ViewModelLocator.Context).Read();
			InfosClubDao.GetInstance(ViewModelLocator.Context).Refresh(this.InfosClub);

			this.CreateEditerCommand();
		}

		protected void CreateEditerCommand() {
			this.EditerCommand = new RelayCommand<string>(
				this.ExecuteEditerCommand
			);
		}

		public void ExecuteEditerCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(pCodeUC)
			);
		}

		public ICommand EditerCommand { get; set; }

		public InfosClub InfosClub {
			get {
				return this.mInfosClub;
			}
			set {
				if (this.mInfosClub != value) {
					this.mInfosClub = value;
					this.RaisePropertyChanged("InfosClub");
				}
			}
		}

		/// <summary>
		/// Ne devrait jamais être appellée
		/// </summary>
		public override void ExecuteCreerCommand() {
			Messenger.Default.Send<NotificationMessageChangementUC>(
				new NotificationMessageChangementUC(CodesUC.FormulaireInfosClub)
			);
		}
	}
}