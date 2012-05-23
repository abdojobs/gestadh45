using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ViewModel.AdherentsVM;
using gestadh45.dal;

namespace gestadh45.wpf.UserControls.AdherentsUC
{
	/// <summary>
	/// Logique d'interaction pour ConsultationAdherentsUC.xaml
	/// </summary>
	public partial class ConsultationAdherentsUC : UserControl
	{
		public ConsultationAdherentsUC() {
			InitializeComponent();

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
		}

		public ConsultationAdherentsUC(Adherent adherent) {
			InitializeComponent();

			var vm = this.DataContext as ConsultationAdherentsVM;
			vm.SelectedAdherent = adherent;

			Messenger.Default.Register<NMClearFilter>(this, msg => this.ClearFilter());
		}

		private void ClearFilter() {
			this.tbxFiltre.Clear();
		}
	}
}
