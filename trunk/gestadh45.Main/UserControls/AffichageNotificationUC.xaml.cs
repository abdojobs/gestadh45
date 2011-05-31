using System.Collections.Generic;
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
		/// Obtient/Définit la liste des notifications à afficher
		/// </summary>
		public List<NotificationIhm> Notifications {
			get {
				return (List<NotificationIhm>)GetValue(NotificationsProperty);
			}

			set {
				SetValue(NotificationsProperty, value);
			}
		}
		
		public AffichageNotificationUC() {
			InitializeComponent();
		}

		public static DependencyProperty NotificationsProperty = DependencyProperty.Register(
			"Notifications",
			typeof(List<NotificationIhm>),
			typeof(AffichageNotificationUC)
		);
	}
}
