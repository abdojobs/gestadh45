using System.Windows;
using System.Windows.Media;
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

			Messenger.Default.Register<NotificationMessageIhm>(
				this,
				this.AfficherNotificationsIhm
			);
			
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

			Messenger.Default.Register<NotificationMessageChangementUC>(
				this,
				this.ChangerUC
			);

			Messenger.Default.Register<NotificationMessageChangementUC<Adherent>>(
			    this,
			    this.ChangerUCAvecParametre
			);

			Messenger.Default.Register<NotificationMessageChangementUC<Inscription>>(
				this,
				this.ChangerUCAvecParametre
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

		private void ChangerUC(NotificationMessageChangementUC pMessage) {
			this.RazUCNotification();

			if (pMessage.CodeUC.Equals(CodesUC.ConsultationInfosClub)) {
				this.contenu.Content = new ConsultationInfosClubUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireInfosClub)) {
				this.contenu.Content = new FormulaireInfosClubUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.ConsultationSaisons)) {
				this.contenu.Content = new ConsultationSaisonsUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireSaison)) {
				this.contenu.Content = new FormulaireSaisonUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.ConsultationVilles)) {
				this.contenu.Content = new ConsultationVillesUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireVille)) {
				this.contenu.Content = new FormulaireVilleUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.ConsultationAdherents)) {
				this.contenu.Content = new ConsultationAdherentsUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireAdherent)) {
				this.contenu.Content = new FormulaireAdherentUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.ConsultationInscriptions)) {
				this.contenu.Content = new ConsultationInscriptionsUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireInscription)) {
				this.contenu.Content = new FormulaireInscriptionUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.ConsultationGroupes)) {
				this.contenu.Content = new ConsultationGroupesUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireGroupe)) {
				this.contenu.Content = new FormulaireGroupeUC();
			}
			else if (pMessage.CodeUC.Equals(CodesUC.GraphsSaisonCourante)) {
				this.contenu.Content = new GraphsSaisonCouranteUC();
			}
			else {
				this.contenu.Content = new ConsultationInfosClubUC();
			}
		}

		private void ChangerUCAvecParametre(NotificationMessageChangementUC<Adherent> pMessage) {
			this.RazUCNotification();

			if (pMessage.CodeUC.Equals(CodesUC.FormulaireAdherent)) {
				this.contenu.Content = new FormulaireAdherentUC((Adherent)pMessage.Element);
			}
			else if (pMessage.CodeUC.Equals(CodesUC.FormulaireInscription)) {
				this.contenu.Content = new FormulaireInscriptionUC((Adherent)pMessage.Element);
			}
			else {
				this.contenu.Content = new ConsultationInfosClubUC();
			}
		}

		private void ChangerUCAvecParametre(NotificationMessageChangementUC<Inscription> pMessage) {
			this.RazUCNotification();

			this.contenu.Content = new FormulaireInscriptionUC((Inscription)pMessage.Element);
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

		private void AfficherNotificationsIhm(NotificationMessageIhm msg) {
			if (msg.TypeNotificationIhm.Equals(TypesNotification.Information)) {
				this.UCNotifications.CouleurTexte = Brushes.Blue;
			}
			else if (msg.Notification.Equals(TypesNotification.Erreur)) {
				this.UCNotifications.CouleurTexte = Brushes.Red;
			}

			this.UCNotifications.Message = msg.Notification;
		}

		private void RazUCNotification() {
			this.UCNotifications.Message = string.Empty;
		}
	}
}
