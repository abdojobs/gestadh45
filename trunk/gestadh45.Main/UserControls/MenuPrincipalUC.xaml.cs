using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gestadh45.Main.UserControls
{
	/// <summary>
	/// Logique d'interaction pour MenuPrincipalUC.xaml
	/// </summary>
	public partial class MenuPrincipalUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit la commande de création de base
		/// </summary>
		public ICommand CmdCreerDatabase {
			get {
				return (ICommand)GetValue(CmdCreerDatabaseProperty);
			}

			set {
				SetValue(CmdCreerDatabaseProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande de changement de datasource
		/// </summary>
		public ICommand CmdChangerDataSource {
			get {
				return (ICommand)GetValue(CmdChangerDataSourceProperty);
			}

			set {
				SetValue(CmdChangerDataSourceProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande d'affichage d'UC
		/// </summary>
		public ICommand CmdAfficherUC {
			get {
				return (ICommand)GetValue(CmdAfficherUCProperty);
			}

			set {
				SetValue(CmdAfficherUCProperty, value);
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
		public static DependencyProperty CmdCreerDatabaseProperty = DependencyProperty.Register(
			"CmdCreerDatabase",
			typeof(ICommand),
			typeof(MenuPrincipalUC)
		);

		public static DependencyProperty CmdChangerDataSourceProperty = DependencyProperty.Register(
			"CmdChangerDataSource",
			typeof(ICommand),
			typeof(MenuPrincipalUC)
		);

		public static DependencyProperty CmdAfficherUCProperty = DependencyProperty.Register(
			"CmdAfficherUC",
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
