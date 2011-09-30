using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Common;
using gestadh45.Ihm.ViewModel.Villes;
using gestadh45.Main.UserControls.Adherents;
using gestadh45.Main.UserControls.Common;
using gestadh45.Main.UserControls.Groupes;
using gestadh45.Main.UserControls.InfosClubs;
using gestadh45.Main.UserControls.Inscriptions;
using gestadh45.Main.UserControls.Saisons;
using gestadh45.Main.UserControls.Stats;
using gestadh45.Main.UserControls.Tools;
using gestadh45.Main.UserControls.TranchesAge;
using gestadh45.Main.UserControls.Villes;
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
					this.contenu.Child = new FormulaireInfosClubUC();
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

				case CodesUC.StatsSaisonCourante:
					this.contenu.Child = new StatsSaisonCouranteUC();
					break;

				case CodesUC.FicheEffectif:
					this.contenu.Child = new FicheEffectifUC();
					break;

				case CodesUC.RepartitionEffectif:
					this.contenu.Child = new RepartitionEffectifUC();
					break;

				case CodesUC.ConsultationTranchesAge:
					this.contenu.Child = new ConsultationTranchesAgeUC();
					break;

				case CodesUC.FormulaireTrancheAge:
					this.contenu.Child = new FormulaireTrancheAgeUC();
					break;

				case CodesUC.ConsultationInfosClub:
				default:
					this.contenu.Child = new ConsultationInfosClubUC();
					break;
			}
		}

		private void AfficherUCAvecParametre(string pCodeUC, object pObjetUC) {
			if (pObjetUC is Inscription) {
				this.contenu.Child = new FormulaireInscriptionUC((Inscription)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireAdherent)) {
				this.contenu.Child = new FormulaireAdherentUC((Adherent)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.FormulaireInscription)) {
				this.contenu.Child = new FormulaireInscriptionUC((Adherent)pObjetUC);
			}
			else if (pObjetUC is Adherent && pCodeUC.Equals(CodesUC.ConsultationAdherents)) {
				this.contenu.Child = new ConsultationAdherentsUC((Adherent)pObjetUC);
			}
		}

		private void OuvrirFenetreUC(NotificationMessageOuvertureFenetre pMessage) {
			if (pMessage.CodeUC.Equals(CodesUC.FormulaireVille)) {
				FormulaireVilleUC lUC = new FormulaireVilleUC();
				((FormulaireVilleUCViewModel)lUC.DataContext).ModeFenetre = true;
				UCWindow lWindow = new UCWindow(lUC);
				lWindow.ShowDialog();
			}
		}
	}
}
