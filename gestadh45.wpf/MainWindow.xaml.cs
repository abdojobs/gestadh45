using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
using gestadh45.wpf.UserControls.AdherentsUC;
using gestadh45.wpf.UserControls.InfosClubs;
using gestadh45.wpf.UserControls.InscriptionsUC;
using gestadh45.wpf.UserControls.Saisons;
using gestadh45.wpf.UserControls.Villes;
using gestadh45.wpf.UserControls.GroupesUC;

namespace gestadh45.wpf
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			InitializeComponent();

			// Abonnement aux messages
			Messenger.Default.Register<NMCloseApplication>(this, (msg) => this.Exit());
			Messenger.Default.Register<NMShowUC>(this, (msg) => this.ShowUC(msg.CodeUC));
			Messenger.Default.Register<NMOpenWindow>(this, (msg) => this.OpenWindowUC(msg.CodeUC));
			Messenger.Default.Register<NMShowUC<Adherent>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowUC<Inscription>>(this, (msg) => this.ShowUCWithParameters(msg.CodeUC, msg.Content));
			Messenger.Default.Register<NMShowAboutBox>(this, (msg) => this.ShowAboutBox());
		}

		private void ShowAboutBox() {
			// TODO afficher aboutbox
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
		}

		private void OpenWindowUC(string codeUC) {
			// TODO ouvrir UC dans une fenetre
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

				default:
					userControl = new ConsultationInfosClubUC();
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
	}
}
