using System.Windows;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls
{
	/// <summary>
	/// Logique d'interaction pour AffichageNotificationUC.xaml
	/// </summary>
	public partial class AffichageNotificationUC : UserControl
	{
		/// <summary>
		/// Obtient/Définit la notification à afficher
		/// </summary>
		public string Notification {
			get {
				return (string)GetValue(NotificationProperty);
			}

			set {
				SetValue(NotificationProperty, value);
			}
		}
		
		public AffichageNotificationUC() {
			InitializeComponent();
		}

		public static DependencyProperty NotificationProperty = DependencyProperty.Register(
			"Notification",
			typeof(string),
			typeof(AffichageNotificationUC)
		);
	}
}
