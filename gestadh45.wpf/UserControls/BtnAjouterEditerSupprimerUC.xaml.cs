using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gestadh45.wpf.UserControls
{
	/// <summary>
	/// Logique d'interaction pour BtnAjouterEditerSupprimerUC.xaml
	/// </summary>
	public partial class BtnAjouterEditerSupprimerUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit l'orientation de l'UC
		/// </summary>
		public Orientation OrientationUC {
			get {
				return (Orientation)GetValue(OrientationUCProperty);
			}

			set {
				SetValue(OrientationUCProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la visibilité du bouton Ajouter
		/// </summary>
		public Visibility BtnAjouterVisibilite {
			get {
				return (Visibility)GetValue(BtnAjouterVisibiliteProperty);
			}

			set {
				SetValue(BtnAjouterVisibiliteProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la visibilité du bouton Modifier
		/// </summary>
		public Visibility BtnModifierVisibilite {
			get {
				return (Visibility)GetValue(BtnModifierVisibiliteProperty);
			}

			set {
				SetValue(BtnModifierVisibiliteProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la visibilité du bouton Supprimer
		/// </summary>
		public Visibility BtnSupprimerVisibilite {
			get {
				return (Visibility)GetValue(BtnSupprimerVisibiliteProperty);
			}

			set {
				SetValue(BtnSupprimerVisibiliteProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande d'ajout
		/// </summary>
		public ICommand CmdAjouter {
			get {
				return (ICommand)GetValue(CmdAjouterProperty);
			}

			set {
				SetValue(CmdAjouterProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande de modification
		/// </summary>
		public ICommand CmdModifier {
			get {
				return (ICommand)GetValue(CmdModifierProperty);
			}

			set {
				SetValue(CmdModifierProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la commande de suppression
		/// </summary>
		public ICommand CmdSupprimer {
			get {
				return (ICommand)GetValue(CmdSupprimerProperty);
			}

			set {
				SetValue(CmdSupprimerProperty, value);
			}
		}
		#endregion
		
		public BtnAjouterEditerSupprimerUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty OrientationUCProperty = DependencyProperty.Register(
			"OrientationUC",
			typeof(Orientation),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty BtnAjouterVisibiliteProperty = DependencyProperty.Register(
			"BtnAjouterVisibilite",
			typeof(Visibility),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty BtnModifierVisibiliteProperty = DependencyProperty.Register(
			"BtnModifierVisibilite",
			typeof(Visibility),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty BtnSupprimerVisibiliteProperty = DependencyProperty.Register(
			"BtnSupprimerVisibilite",
			typeof(Visibility),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty CmdAjouterProperty = DependencyProperty.Register(
			"CmdAjouter",
			typeof(ICommand),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty CmdModifierProperty = DependencyProperty.Register(
			"CmdModifier",
			typeof(ICommand),
			typeof(BtnAjouterEditerSupprimerUC)
		);

		public static DependencyProperty CmdSupprimerProperty = DependencyProperty.Register(
			"CmdSupprimer",
			typeof(ICommand),
			typeof(BtnAjouterEditerSupprimerUC)
		);
		#endregion
	}
}
