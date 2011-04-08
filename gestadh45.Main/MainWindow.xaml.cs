using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.Ihm;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Main.UserControls.Consultation;
using gestadh45.Main.UserControls.Formulaire;
using gestadh45.Model;
using Microsoft.Win32;
using gestadh45.Ihm.ViewModel.Formulaire;

namespace gestadh45.Main
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			InitializeComponent();

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

			lDialog.Filter = string.Format("fichiers {0} (*{0})|*{0}", pMessage.ExtensionFichier);
			lDialog.RestoreDirectory = true;

			if ((bool)lDialog.ShowDialog()) {
				lFileName = lDialog.FileName;
			}

			pMessage.Execute(lFileName);
		}

		private void ChangerUC(NotificationMessageChangementUC pMessage) {
			switch (pMessage.CodeUC) {
				case CodesUC.ConsultationInfosClub:
				default:
					this.contenu.Child = new ConsultationInfosClubUC();
					break;

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
			}
		}

		private void ChangerUCAvecParametre(NotificationMessageChangementUC<Adherent> pMessage) {
			switch (pMessage.CodeUC) {
				case CodesUC.FormulaireAdherent:
					this.contenu.Child = new FormulaireAdherentUC((Adherent)pMessage.Element);
					break;

				case CodesUC.FormulaireInscription:
					this.contenu.Child = new FormulaireInscriptionUC((Adherent)pMessage.Element);
					break;
			}
		}

		private void ChangerUCAvecParametre(NotificationMessageChangementUC<Inscription> pMessage) {
			this.contenu.Child = new FormulaireInscriptionUC((Inscription)pMessage.Element);
		}

		private void OuvrirFenetreUC(NotificationMessageOuvertureFenetre pMessage) {
			switch (pMessage.CodeUC) {
				case CodesUC.FormulaireVille:
					FormulaireVilleUC lUC = new FormulaireVilleUC();
					((FormulaireVilleUCViewModel)lUC.DataContext).ModeFenetre = true;
					UCWindow lWindow = new UCWindow(lUC);
					lWindow.ShowDialog();
					break;
			}
		}
	}
}
