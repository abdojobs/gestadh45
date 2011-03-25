﻿using System.Windows;
using gestadh45.Ihm.SpecialMessages;
using Microsoft.Win32;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			InitializeComponent();

			// TODO s'abonner aux messages
			Messenger.Default.Register<NotificationMessageUtilisateur>(
				this, 
				this.AfficherNotificationUtilisateur
			);

			Messenger.Default.Register<DialogMessageConfirmation>(
				this,
				this.AfficherDialogConfirmation
			);

			Messenger.Default.Register<NotificationMessageActionFileDialog<string>>(
				this,
				this.AfficherFileDialog
			);

			Messenger.Default.Register<NotificationMessageChangementUC>(
				this,
				this.ChangerUC
			);
		}

		private void AfficherAboutBox() {
			// TODO télécharge about box et ajouter au projet
			//new WPFAboutBox(this).ShowDialog();
		}

		private void AfficherNotificationUtilisateur(NotificationMessageUtilisateur pMessage) {
			string lTitre = string.Empty;
			
			switch (pMessage.Notification) {
				case TypesNotification.Erreur:
					lTitre = ResMessages.TitreErreur;
					break;

				case TypesNotification.Information:
					lTitre = ResMessages.TitreInformation;
					break;
			}

			MessageBox.Show(pMessage.Message, lTitre);
		}

		private void AfficherDialogConfirmation(DialogMessageConfirmation pDialog) {
			var lResult = MessageBox.Show(pDialog.Content, pDialog.Caption, pDialog.Button);
			pDialog.ProcessCallback(lResult);
		}

		private void AfficherFileDialog(NotificationMessageActionFileDialog<string> pMessage) {
			string lFileName = null;

			FileDialog lDialog;

			switch (pMessage.Notification) {
				case TypesNotification.OpenFileDialog:
					lDialog = new OpenFileDialog();
					break;

				case TypesNotification.SaveFileDialog:
				default:
					lDialog = new SaveFileDialog
					{
						FileName = pMessage.NomFichier
					};
					break;
			}

			lDialog.Filter = string.Format("fichiers.{0} (*{0})|*{0}", pMessage.ExtensionFichier);
			lDialog.RestoreDirectory = true;

			if ((bool)lDialog.ShowDialog()) {
				lFileName = lDialog.FileName;
			}

			pMessage.Execute(lFileName);
		}

		private void ChangerUC(NotificationMessageChangementUC pMessage) {
			// TODO implémenter
			
			//switch (pMessage.CodeUC) {

			//}
		}
	}
}
