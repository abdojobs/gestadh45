using System.Windows;
using System.Windows.Controls;
using gestadh45.Ihm.ObjetsIhm;

namespace gestadh45.Main.UserControls
{
	/// <summary>
	/// Logique d'interaction pour AffichageNotificationUC.xaml
	/// </summary>
	public partial class AffichageNotificationUC : UserControl
	{
		/// <summary>
		/// Obtient/Définit la notification à afficher
		/// </summary>
		public NotificationIhm Notification {
			get {
				return (NotificationIhm)GetValue(NotificationProperty);
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
			typeof(NotificationIhm),
			typeof(AffichageNotificationUC)
		);
	}
}
