using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
		/// Obtient/Définit le header
		/// </summary>
		public string Header {
			get {
				return (string)GetValue(HeaderProperty);
			}

			set {
				SetValue(HeaderProperty, value);
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

		public static DependencyProperty HeaderProperty = DependencyProperty.Register(
			"Header",
			typeof(string),
			typeof(AffichageNotificationUC)
		);
	}
}
