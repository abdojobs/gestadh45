using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gestadh45.wpf.UserControls
{
	/// <summary>
	/// Logique d'interaction pour MenuPrincipalUC.xaml
	/// </summary>
	public partial class MenuPrincipalUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit la commande d'affichage d'UC
		/// </summary>
		public ICommand CmdShowUC {
			get {
				return (ICommand)GetValue(CmdShowUCProperty);
			}

			set {
				SetValue(CmdShowUCProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande d'affichage de l'about box
		/// </summary>
		public ICommand CmdAboutBox {
			get {
				return (ICommand)GetValue(CmdAboutBoxProperty);
			}

			set {
				SetValue(CmdAboutBoxProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande de fermeture de l'application
		/// </summary>
		public ICommand CmdClose {
			get {
				return (ICommand)GetValue(CmdCloseProperty);
			}

			set {
				SetValue(CmdCloseProperty, value);
			}
		}
		#endregion

		public MenuPrincipalUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty CmdShowUCProperty = DependencyProperty.Register(
			"CmdShowUC",
			typeof(ICommand),
			typeof(MenuPrincipalUC)
		);

		public static DependencyProperty CmdAboutBoxProperty = DependencyProperty.Register(
			"CmdAboutBox",
			typeof(ICommand),
			typeof(MenuPrincipalUC)
		);

		public static DependencyProperty CmdCloseProperty = DependencyProperty.Register(
			"CmdClose",
			typeof(ICommand),
			typeof(MenuPrincipalUC)
		);
		#endregion
	}
}
