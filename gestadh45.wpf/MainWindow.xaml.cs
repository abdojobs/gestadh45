﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel;
using gestadh45.dal;
using gestadh45.wpf.UserControls.AdherentsUC;
using gestadh45.wpf.UserControls.CampagnesVerificationUC;
using gestadh45.wpf.UserControls.CategoriesUC;
using gestadh45.wpf.UserControls.DureesDeVieUC;
using gestadh45.wpf.UserControls.EquipementsUC;
using gestadh45.wpf.UserControls.GroupesUC;
using gestadh45.wpf.UserControls.InfosClubs;
using gestadh45.wpf.UserControls.InscriptionsUC;
using gestadh45.wpf.UserControls.LocalisationUC;
using gestadh45.wpf.UserControls.MainScreenUC;
using gestadh45.wpf.UserControls.MarquesUC;
using gestadh45.wpf.UserControls.ModelesUC;
using gestadh45.wpf.UserControls.OutilsUC;
using gestadh45.wpf.UserControls.Saisons;
using gestadh45.wpf.UserControls.TranchesAgeUC;
using gestadh45.wpf.UserControls.Villes;
using Microsoft.Win32;
using Forms = System.Windows.Forms;

namespace gestadh45.wpf
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			// Ensure the current culture passed into bindings 
			// is the OS culture. By default, WPF uses en-US 
			// as the culture, regardless of the system settings.
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement), 
				new FrameworkPropertyMetadata(
					XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
				)
			);


			InitializeComponent();

			// Abonnement aux messages
			Messenger.Default.Register<NMCloseApplication>(this, (msg) => this.Exit());
			Messenger.Default.Register<NMShowUC>(this, (msg) => this.ShowUC(msg.CodeUC));
			Messenger.Default.Register<NMOpenWindow>(this, (msg) => this.OpenWindowUC(msg.CodeUC, msg.ParentGuid));
			Messenger.Default.Register<NMShowUC<Adherent>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<Inscription>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<Equipement>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<DureeDeVie>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<CampagneVerification>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowAboutBox>(this, (msg) => this.ShowAboutBox());
			Messenger.Default.Register<NMAskConfirmationDialog<bool>>(this, msg => this.ShowConfirmationDialog(msg.Execute, msg.Text));

			// Abonnement aux messages pour les dialogues
			Messenger.Default.Register<NMActionFileDialog<string>>(
				this, 
				(msg) => this.ShowSaveFileDialog(msg.ExtensionFichier, msg.NomFichier, msg.Execute)
			);

			Messenger.Default.Register<NMActionFolderDialog<string>>(
				this,
				(msg) => this.ShowFolderBrowserDialog(msg.Execute)
			);

			this.ShowUC(CodesUC.MainScreenCheck);
		}

		private void ShowAboutBox() {
			var about = new AboutBox(this);
			about.ShowDialog();
		}

		private void ShowUC(string codeUC) {
			this.contenu.Child = this.GetUCFromCode(codeUC);
		}

		private void ShowUCWithParameters(string codeUC, object objetUC) {
			if (codeUC.Equals(CodesUC.FormulaireAdherent) && objetUC is Adherent) {
				this.contenu.Child = new FormulaireAdherentUC(objetUC as Adherent);
			}
			else if (codeUC.Equals(CodesUC.FormulaireInscription) && objetUC is Inscription) {
				this.contenu.Child = new FormulaireInscriptionUC(objetUC as Inscription);
			}
			else if (codeUC.Equals(CodesUC.FormulaireInscription) && objetUC is Adherent) {
				this.contenu.Child = new FormulaireInscriptionUC(objetUC as Adherent);
			}
			else if (codeUC.Equals(CodesUC.ConsultationAdherents) && objetUC is Adherent) {
				this.contenu.Child = new ConsultationAdherentsUC(objetUC as Adherent);
			}
			else if (codeUC.Equals(CodesUC.FormulaireEquipement) && objetUC is Equipement) {
				this.contenu.Child = new FormulaireEquipementUC(objetUC as Equipement);
			}
			else if (codeUC.Equals(CodesUC.FormulaireDureeDeVie) && objetUC is DureeDeVie) {
			    this.contenu.Child = new FormulaireDureeDeVieUC(objetUC as DureeDeVie);
			}
			else if (codeUC.Equals(CodesUC.FormulaireSaisieCampagneVerification) && objetUC is CampagneVerification) {
				this.contenu.Child = new FormulaireSaisieVerificationsUC(objetUC as CampagneVerification);
			}
		}

		private void OpenWindowUC(string codeUC, Guid parentGuid) {
			var uc = this.GetUCFromCode(codeUC);

			((VMUCBase)uc.DataContext).IsWindowMode = true;
			((VMUCBase)uc.DataContext).UCParentGuid = parentGuid;

			UCWindow window = new UCWindow(uc);
			window.ShowDialog();
		}

		/// <summary>
		/// Renvoit une instance d'un UC à partir de son code
		/// </summary>
		/// <param name="codeUC">Code de l'UC</param>
		/// <returns>Instance de l'UC</returns>
		private UserControl GetUCFromCode(string codeUC) {
			UserControl userControl;
			
			switch (codeUC) {
				case CodesUC.ConsultationInfosClub:
					userControl = new ConsultationInfosClubUC();
					break;

				case CodesUC.FormulaireInfosClub:
					userControl = new FormulaireInfosClubUC();
					break;

				case CodesUC.ConsultationVilles:
					userControl = new ConsultationVillesUC();
					break;

				case CodesUC.FormulaireVille:
					userControl = new FormulaireVilleUC();
					break;

				case CodesUC.ConsultationSaisons:
					userControl = new ConsultationSaisonsUC();
					break;

				case CodesUC.FormulaireSaison:
					userControl = new FormulaireSaisonUC();
					break;

				case CodesUC.ConsultationAdherents:
					userControl = new ConsultationAdherentsUC();
					break;

				case CodesUC.FormulaireAdherent:
					userControl = new FormulaireAdherentUC();
					break;

				case CodesUC.ConsultationInscriptions:
					userControl = new ConsultationInscriptionsUC();
					break;

				case CodesUC.FormulaireInscription:
					userControl = new FormulaireInscriptionUC();
					break;

				case CodesUC.ConsultationGroupes:
					userControl = new ConsultationGroupesUC();
					break;

				case CodesUC.FormulaireGroupe:
					userControl = new FormulaireGroupeUC();
					break;

				case CodesUC.ConsultationTranchesAge:
					userControl = new ConsultationTranchesAgeUC();
					break;

				case CodesUC.FormulaireTrancheAge:
					userControl = new FormulaireTrancheAgeUC();
					break;

				case CodesUC.ConsultationMarques:
					userControl = new ConsultationMarquesUC();
					break;

				case CodesUC.FormulaireMarque:
					userControl = new FormulaireMarqueUC();
					break;

				case CodesUC.ConsultationEquipements:
					userControl = new ConsultationEquipementsUC();
					break;

				case CodesUC.FormulaireEquipement:
					userControl = new FormulaireEquipementUC();
					break;

				case CodesUC.ConsultationCategories:
					userControl = new ConsultationCategoriesUC();
					break;

				case CodesUC.FormulaireCategorie:
					userControl = new FormulaireCategorieUC();
					break;

				case CodesUC.ConsultationDureesDeVie:
					userControl = new ConsultationDureesDeVieUC();
					break;

				case CodesUC.FormulaireDureeDeVie:
					userControl = new FormulaireDureeDeVieUC();
					break;

				case CodesUC.FormulaireInitialisationDatabase:
					userControl = new FormulaireInitialisationDatabaseUC();
					break;

				case CodesUC.ConsultationModeles:
					userControl = new ConsultationModelesUC();
					break;

				case CodesUC.FormulaireModele:
					userControl = new FormulaireModeleUC();
					break;

				case CodesUC.ConsultationLocalisations:
					userControl = new ConsultationLocalisationsUC();
					break;

				case CodesUC.FormulaireLocalisation:
					userControl = new FormulaireLocalisationUC();
					break;

				case CodesUC.ConsultationCampagnesVerification:
					userControl = new ConsultationCampagnesVerificationUC();
					break;

				case CodesUC.FormulaireCreationCampagneVerification:
					userControl = new FormulaireCreationCampagneVerificationUC();
					break;

				case CodesUC.EcranStatistiques:
					userControl = new GraphiquesUC();
					break;

				case CodesUC.EcranReporting:
					userControl = new ReportingUC();
					break;

				case CodesUC.NettoyageCNIL:
					userControl = new NettoyageCNILUC();
					break;

				case CodesUC.MainScreenCheck:
				default:
					userControl = new MainScreenCheckUC();
					break;
			}

			return userControl;
		}

		/// <summary>
		/// Quitte l'application
		/// </summary>
		private void Exit() {
			this.Close();
		}
		
		private void ShowSaveFileDialog(string extensionFichier, string nomFichier, Action<string> callback) {
			SaveFileDialog dialog = new SaveFileDialog()
			{
				FileName = nomFichier
			};

			dialog.Filter = string.Format(Properties.Resources.FileDialogFilter, extensionFichier);
			dialog.RestoreDirectory = true;

			string filePath = (dialog.ShowDialog().Value) ? dialog.FileName : null;

			callback(filePath);
		}

		private void ShowFolderBrowserDialog(Action<string> callback) {
			Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;

			string selectedFolder = (dialog.ShowDialog() == Forms.DialogResult.OK) ? dialog.SelectedPath : null;

			callback(selectedFolder);
		}

		private void ShowConfirmationDialog(Action<bool> callback, string text) {
			var response = MessageBox.Show(text, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

			callback(response == MessageBoxResult.Yes);
		}
	}
}
