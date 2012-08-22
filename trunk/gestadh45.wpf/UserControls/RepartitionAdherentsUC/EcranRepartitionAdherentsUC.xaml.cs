using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.wpf.UserControls.RepartitionAdherentsUC
{
	/// <summary>
	/// Logique d'interaction pour RepartitionAdherentsUC.xaml
	/// </summary>
	public partial class EcranRepartitionAdherentsUC : UserControl
	{
		public EcranRepartitionAdherentsUC() {
			InitializeComponent();
		}

		private void dgTranchesEffectif_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
			var pd = e.PropertyDescriptor as PropertyDescriptor;
			var displayAttrib = pd.Attributes[typeof(DisplayAttribute)] as DisplayAttribute;

			if (displayAttrib != null) {
				e.Column.Header = ResReports.ResourceManager.GetString(displayAttrib.Name);
			}
		}
	}
}
