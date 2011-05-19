using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gestadh45.Main.UserControls
{
	/// <summary>
	/// Logique d'interaction pour AffichageNotificationUC.xaml
	/// </summary>
	public partial class AffichageNotificationUC : UserControl
	{
		/// <summary>
		/// Obtient/Définit le message à afficher
		/// </summary>
		public string Message {
			get {
				return (string)GetValue(MessageProperty); 
			}

			set {
				SetValue(MessageProperty, value);
			}
		}

		/// <summary>
		/// Obtient/Définit la couleur du texte
		/// </summary>
		public Brush CouleurTexte {
			get {
				return (Brush)GetValue(CouleurTexteProperty);
			}

			set {
				SetValue(CouleurTexteProperty, value);
			}
		}
		
		public AffichageNotificationUC() {
			InitializeComponent();
		}

		public static DependencyProperty MessageProperty = DependencyProperty.Register(
			"Message",
			typeof(string),
			typeof(AffichageNotificationUC)
		);

		public static DependencyProperty CouleurTexteProperty = DependencyProperty.Register(
			"CouleurTexte",
			typeof(Brush),
			typeof(AffichageNotificationUC)
		);
	}
}
