using System.Windows;
using System.Windows.Controls;
using gestadh45.dal;

namespace gestadh45.Main.UserControls.Adherents
{
	/// <summary>
	/// Logique d'interaction pour AdherentContactUC.xaml
	/// </summary>
	public partial class AdherentContactUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit l'adhérent
		/// </summary>
		public Adherent Adherent {
			get {
				return (Adherent)GetValue(AdherentProperty);
			}

			set {
				SetValue(AdherentProperty, value);
			}
		}
		#endregion

		public AdherentContactUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty AdherentProperty = DependencyProperty.Register(
			"Adherent",
			typeof(Adherent),
			typeof(AdherentContactUC)
		);
		#endregion
	}
}
