using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using gestadh45.dal;

namespace gestadh45.Main.UserControls.Adherents
{
	/// <summary>
	/// Logique d'interaction pour AdherentIdentiteUC.xaml
	/// </summary>
	public partial class AdherentIdentiteUC : UserControl
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

		public AdherentIdentiteUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty AdherentProperty = DependencyProperty.Register(
			"Adherent",
			typeof(Adherent),
			typeof(AdherentIdentiteUC)
		);
		#endregion
	}
}
