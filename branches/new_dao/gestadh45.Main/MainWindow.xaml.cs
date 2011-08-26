using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Villes;
using gestadh45.Main.UserControls.Adherents;
using gestadh45.Main.UserControls.Groupes;
using gestadh45.Main.UserControls.InformationsClub;
using gestadh45.Main.UserControls.Inscriptions;
using gestadh45.Main.UserControls.ParametresApplication;
using gestadh45.Main.UserControls.Saisons;
using gestadh45.Main.UserControls.Stats;
using gestadh45.Main.UserControls.Villes;
using gestadh45.model;
using Microsoft.Win32;
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
			    (msg) => this.AfficherUCAvecParametre(msg.Notification, msg.Element, msg.ModeOuverture)
			);

			Messenger.Default.Register<MsgAfficherUC<Inscription>>(
				this,
				(msg) => this.AfficherUCAvecParametre(msg.Notification, msg.Element, msg.ModeOuverture)
			);

			Messenger.Default.Register<NotificationMessageOuvertureFenetre>(
				this,
				(msg) => this.OuvrirFenetreUC(msg.CodeUC)
			);

			Messenger.Default.Register<NotificationMessageAboutBox>(
				this,
				(msg) => this.AfficherAboutBox()
			);
		}

		private void AfficherAboutBox() {
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
					this.contenu.Child = new FormulaireInfosClubUC();
					break;

				case CodesUC.ConsultationParamsApplication:
					this.contenu.Child = new ConsultationParamsApplicationUC();
					break;

				case CodesUC.FormulaireParamsApplication:
					this.contenu.Child = new FormulaireParamsApplicationUC();
					break;

				case CodesUC.ConsultationSaisons:
					this.contenu.Child = new ConsultationSaisonsUC();
					break;

				case CodesUC.FormulaireSaison:
					this.contenu.Child = new FormulaireSaisonUC();
					break;

				case CodesUC.ConsultationVilles:
					this.contenu.Child = new ConsultationVillesUC();
					break;

				case CodesUC.FormulaireVille:
					this.contenu.Child = new FormulaireVilleUC();
					break;

				case CodesUC.ConsultationAdherents:
					this.contenu.Child = new ConsultationAdherentsUC();
					break;

				case CodesUC.FormulaireAdherent:
					this.contenu.Child = new FormulaireAdherentUC();
					break;

				case CodesUC.ConsultationInscriptions:
					this.contenu.Child = new ConsultationInscriptionsUC();
					break;

				case CodesUC.FormulaireInscription:
					this.contenu.Child = new FormulaireInscriptionUC();
					break;

				case CodesUC.ConsultationGroupes:
					this.contenu.Child = new ConsultationGroupesUC();
					break;

				case CodesUC.FormulaireGroupe:
					this.contenu.Child = new FormulaireGroupeUC();
					break;

				case CodesUC.GraphsSaisonCourante:
					this.contenu.Child = new GraphsSaisonCouranteUC();
					break;

				case CodesUC.ConsultationInfosClub:
				default:
					this.contenu.Child = new ConsultationInfosClubUC();
					break;
			}
		}

		private void AfficherUCAvecParametre(string pCodeUC, object pObjetUC, MsgAfficherUC.TypeOuverture pMode) {
			if (pObjetUC is Inscription) {	// édition inscription
				this.contenu.Child = new FormulaireInscriptionUC((Inscription)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireAdherent)) {	// édition ou duplication adhérent (en fonction de pMode)
				this.contenu.Child = new FormulaireAdherentUC((Adherent)pObjetUC, pMode);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireInscription)) { // inscription adhérent
				this.contenu.Child = new FormulaireInscriptionUC((Adherent)pObjetUC);
			}
		}

		private void OuvrirFenetreUC(string pCodeUC) {
			if (pCodeUC.Equals(CodesUC.FormulaireVille)) {
				FormulaireVilleUC lUC = new FormulaireVilleUC();
				((FormulaireVilleUCViewModel)lUC.DataContext).ModeFenetre = true;
				UCWindow lWindow = new UCWindow(lUC);
				lWindow.ShowDialog();
			}
		}
	}
}
