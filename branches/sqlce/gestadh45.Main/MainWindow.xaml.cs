﻿using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Consultation;
using gestadh45.Ihm.ViewModel.Formulaire;
using gestadh45.Main.UserControls.Consultation;
using gestadh45.Main.UserControls.Formulaire;
using gestadh45.Model;
using Microsoft.Win32;
using Transitionals.Transitions;
using forms = System.Windows.Forms;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			InitializeComponent();
			
			Messenger.Default.Register<DialogMessageConfirmation>(
				this,
				this.AfficherDialogConfirmation
			);

			Messenger.Default.Register<NotificationMessageActionFileDialog<string>>(
				this,
				this.AfficherFileDialog
			);

			Messenger.Default.Register<NotificationMessageActionFolderDialog<string>>(
				this,
				this.AfficherFolderDialog
			);

			Messenger.Default.Register<MsgAfficherUC>(
				this,
				(msg) => this.AfficherUC(msg.Notification)
			);

			Messenger.Default.Register<MsgAfficherUC<Adherent>>(
			    this,
			    (msg) => this.AfficherUCAvecParametre(msg.Notification, msg.Element)
			);

			Messenger.Default.Register<MsgAfficherUC<Inscription>>(
				this,
				(msg) => this.AfficherUCAvecParametre(msg.Notification, msg.Element)
			);

			Messenger.Default.Register<NotificationMessageOuvertureFenetre>(
				this,
				this.OuvrirFenetreUC
			);

			Messenger.Default.Register<NotificationMessageAboutBox>(
				this,
				this.AfficherAboutBox
			);

			Messenger.Default.Register<NotificationMessageConsultationExtractions>(
				this,
				this.OuvrirFenetreUC
			);

			Messenger.Default.Register<NotificationMessageTransition>(
				this,
				this.ModifierTransition
			);
		}

		private void AfficherAboutBox(NotificationMessageAboutBox pMessage) {
			new WPFAboutBox(this).ShowDialog();
		}

		private void AfficherDialogConfirmation(DialogMessageConfirmation pDialog) {
			var lResult = MessageBox.Show(pDialog.Content, pDialog.Caption, pDialog.Button);
			pDialog.ProcessCallback(lResult);
		}

		private void AfficherFolderDialog(NotificationMessageActionFolderDialog<string> pMessage) {
			forms.FolderBrowserDialog lBrowser = new forms.FolderBrowserDialog();
			lBrowser.ShowNewFolderButton = true;

			string lFolder = null;
			if (lBrowser.ShowDialog() == forms.DialogResult.OK) {
				lFolder = lBrowser.SelectedPath;
			}

			pMessage.Execute(lFolder);
		}

		private void AfficherFileDialog(NotificationMessageActionFileDialog<string> pMessage) {
			string lFileName = null;

			FileDialog lDialog;

			if (pMessage.Notification.Equals(TypesNotification.OpenFileDialog)) {
				lDialog = new OpenFileDialog();
			}
			else {
				lDialog = new SaveFileDialog
				{
					FileName = pMessage.NomFichier
				};
			}

			lDialog.Filter = string.Format("fichiers {0} (*{0})|*{0}", pMessage.ExtensionFichier);
			lDialog.RestoreDirectory = true;

			if ((bool)lDialog.ShowDialog()) {
				lFileName = lDialog.FileName;
			}

			pMessage.Execute(lFileName);
		}

		private void AfficherUC(string pCodeUC) {
			switch (pCodeUC) {
				case CodesUC.FormulaireInfosClub:
					this.contenu.Content = new FormulaireInfosClubUC();
					break;

				case CodesUC.ConsultationSaisons:
					this.contenu.Content = new ConsultationSaisonsUC();
					break;

				case CodesUC.FormulaireSaison:
					this.contenu.Content = new FormulaireSaisonUC();
					break;

				case CodesUC.ConsultationVilles:
					this.contenu.Content = new ConsultationVillesUC();
					break;

				case CodesUC.FormulaireVille:
					this.contenu.Content = new FormulaireVilleUC();
					break;

				case CodesUC.ConsultationAdherents:
					this.contenu.Content = new ConsultationAdherentsUC();
					break;

				case CodesUC.FormulaireAdherent:
					this.contenu.Content = new FormulaireAdherentUC();
					break;

				case CodesUC.ConsultationInscriptions:
					this.contenu.Content = new ConsultationInscriptionsUC();
					break;

				case CodesUC.FormulaireInscription:
					this.contenu.Content = new FormulaireInscriptionUC();
					break;

				case CodesUC.ConsultationGroupes:
					this.contenu.Content = new ConsultationGroupesUC();
					break;

				case CodesUC.FormulaireGroupe:
					this.contenu.Content = new FormulaireGroupeUC();
					break;

				case CodesUC.GraphsSaisonCourante:
					this.contenu.Content = new GraphsSaisonCouranteUC();
					break;

				case CodesUC.ConsultationInfosClub:
				default:
					this.contenu.Content = new ConsultationInfosClubUC();
					break;
			}
		}

		private void AfficherUCAvecParametre(string pCodeUC, object pObjetUC) {
			if (pObjetUC is Inscription) {
				this.contenu.Content = new FormulaireInscriptionUC((Inscription)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireAdherent)) {
				this.contenu.Content = new FormulaireAdherentUC((Adherent)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireInscription)) {
				this.contenu.Content = new FormulaireInscriptionUC((Adherent)pObjetUC);
			}
		}

		private void OuvrirFenetreUC(string pCodeUC) {

		}

		private void OuvrirFenetreUC(NotificationMessageOuvertureFenetre pMessage) {
			if (pMessage.CodeUC.Equals(CodesUC.FormulaireVille)) {
				FormulaireVilleUC lUC = new FormulaireVilleUC();
				((FormulaireVilleUCViewModel)lUC.DataContext).ModeFenetre = true;
				UCWindow lWindow = new UCWindow(lUC);
				lWindow.ShowDialog();
			}
		}

		private void OuvrirFenetreUC(NotificationMessageConsultationExtractions pMessage) {
			ConsultationExtractionsUC lUC = new ConsultationExtractionsUC();
			((ConsultationExtractionsUCViewModel)lUC.DataContext).ModeFenetre = true;
			((ConsultationExtractionsUCViewModel)lUC.DataContext).ResultatExtraction = pMessage.ResultatExtraction;
			UCWindow lWindow = new UCWindow(lUC);
			lWindow.ShowDialog();
		}

		private void ModifierTransition(NotificationMessageTransition msg) {
			((TranslateTransition)this.contenu.Transition).StartPoint = msg.SensTransition;
		}
	}
}