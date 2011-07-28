﻿using System.Windows.Controls;
using gestadh45.Ihm.ViewModel.Formulaire;
using gestadh45.model;

namespace gestadh45.Main.UserControls.Formulaire
{
	/// <summary>
	/// Logique d'interaction pour FormulaireInscriptionUC.xaml
	/// </summary>
	public partial class FormulaireInscriptionUC : UserControl
	{
		public FormulaireInscriptionUC() {
			InitializeComponent();
		}

		public FormulaireInscriptionUC(Inscription pInscription) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel lVm = base.DataContext as FormulaireInscriptionUCViewModel;
			lVm.EstEdition = true;
			lVm.SetInscription(pInscription);
		}

		public FormulaireInscriptionUC(Adherent pAdherent) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel lVm = base.DataContext as FormulaireInscriptionUCViewModel;
			lVm.EstEdition = false;
			lVm.SetAdherent(pAdherent);
		}
	}
}
