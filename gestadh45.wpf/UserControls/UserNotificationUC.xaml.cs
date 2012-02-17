using System.Windows;
using System.Windows.Controls;

namespace gestadh45.wpf.UserControls
{
	public partial class UserNotificationUC : UserControl
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

		public UserNotificationUC() {
			InitializeComponent();
		}

		public static DependencyProperty NotificationProperty = DependencyProperty.Register(
			"Notification",
			typeof(string),
			typeof(UserNotificationUC)
		);
	}
}
