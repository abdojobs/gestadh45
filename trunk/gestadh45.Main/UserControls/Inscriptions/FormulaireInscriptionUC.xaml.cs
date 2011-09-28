﻿using System.Windows.Controls;
using gestadh45.dal;
using gestadh45.Ihm.ViewModel.Inscriptions;

namespace gestadh45.Main.UserControls.Inscriptions
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
			lVm.Inscription = pInscription;
			lVm.EstEdition = true;
		}

		public FormulaireInscriptionUC(Adherent pAdherent) {
			this.InitializeComponent();
			FormulaireInscriptionUCViewModel lVm = base.DataContext as FormulaireInscriptionUCViewModel;
			Inscription inscription = new Inscription();
			inscription.Adherent = pAdherent;
			lVm.Inscription = inscription;
		}
	}
}