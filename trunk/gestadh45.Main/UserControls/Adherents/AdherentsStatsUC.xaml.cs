using System.Windows;
using System.Windows.Controls;

namespace gestadh45.Main.UserControls.Adherents
{
	/// <summary>
	/// Logique d'interaction pour AdherentStats.xaml
	/// </summary>
	public partial class AdherentsStatsUC : UserControl
	{
		#region proprietes
		/// <summary>
		/// Obtient/Définit le nombre d'adhérents
		/// </summary>
		public int NbAdherents {
			get {
				return (int)GetValue(NbAdherentsProperty);
			}

			set {
				SetValue(NbAdherentsProperty, value);
			}
		}
		#endregion

		public AdherentsStatsUC() {
			InitializeComponent();
		}

		#region Dependency Properties
		public static DependencyProperty NbAdherentsProperty = DependencyProperty.Register(
			"NbAdherents",
			typeof(int),
			typeof(AdherentsStatsUC)
		);
		#endregion
	}
}
