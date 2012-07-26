using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gestadh45.business.ViewModel.MainScreenVM;

namespace gestadh45.wpf.UserControls.MainScreenUC
{
	/// <summary>
	/// Logique d'interaction pour MainScreenUC.xaml
	/// </summary>
	public partial class MainScreenCheckUC : UserControl
	{
		public MainScreenCheckUC() {
			InitializeComponent();			
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e) {
			((this.DataContext) as MainScreenCheckVM).DoCheck();
		}
	}
}
