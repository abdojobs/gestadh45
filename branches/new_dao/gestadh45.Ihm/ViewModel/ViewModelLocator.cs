﻿using gestadh45.Ihm.ViewModel.Adherents;
using gestadh45.Ihm.ViewModel.Groupes;
using gestadh45.Ihm.ViewModel.InformationsCLub;
using gestadh45.Ihm.ViewModel.Inscriptions;
using gestadh45.Ihm.ViewModel.ParametresApplication;
using gestadh45.Ihm.ViewModel.Saisons;
using gestadh45.Ihm.ViewModel.Stats;
using gestadh45.Ihm.ViewModel.Villes;

namespace gestadh45.Ihm.ViewModel
{
	public class ViewModelLocator
	{
		public static string DataSource = null;

		public ViewModelLocator() {
			if (_main == null) {
				_main = new MainViewModel();
			}
		}

		public static void Cleanup() {
			_main.Cleanup();
			_main = null;
		}

		#region MainView
		private static MainViewModel _main;

		public static MainViewModel MainStatic {
			get {
				if (_main == null) {
					_main = new MainViewModel();
				}
				return _main;
			}
		}
		#endregion

		#region ConsultationInfosClub
		private static ConsultationInfosClubUCViewModel _consultationInfosClubVM;

		public static ConsultationInfosClubUCViewModel ConsultationInfosClubVMStatic {
			get {
				if (_consultationInfosClubVM != null) {
					_consultationInfosClubVM.Cleanup();
					_consultationInfosClubVM = null;
				}

				_consultationInfosClubVM = new ConsultationInfosClubUCViewModel();

				return _consultationInfosClubVM;
			}
		}
		#endregion

		#region FormulaireInfosClub
		private static FormulaireInfosClubUCViewModel _formulaireInfosClubVM;

		public static FormulaireInfosClubUCViewModel FormulaireInfosClubVMStatic {
			get {
				if (_formulaireInfosClubVM != null) {
					_formulaireInfosClubVM.Cleanup();
					_formulaireInfosClubVM = null;
				}

				_formulaireInfosClubVM = new FormulaireInfosClubUCViewModel();

				return _formulaireInfosClubVM;
			}
		}
		#endregion

		#region ConsultationParamsApplication
		private static ConsultationParamsApplicationViewModel _consultationParamsApplicationVM;

		public static ConsultationParamsApplicationViewModel ConsultationParamsApplicationVMStatic {
			get {
				if (_consultationParamsApplicationVM != null) {
					_consultationParamsApplicationVM.Cleanup();
					_consultationParamsApplicationVM = null;
				}

				_consultationParamsApplicationVM = new ConsultationParamsApplicationViewModel();

				return _consultationParamsApplicationVM;
			}
		}
		#endregion

		#region FormulaireParamsApplication
		private static FormulaireParamsApplicationUCViewModel _formulaireParamsApplicationVM;

		public static FormulaireParamsApplicationUCViewModel FormulaireParamsApplicationVMStatic {
			get {
				if (_formulaireParamsApplicationVM != null) {
					_formulaireParamsApplicationVM.Cleanup();
					_formulaireParamsApplicationVM = null;
				}

				_formulaireParamsApplicationVM = new FormulaireParamsApplicationUCViewModel();

				return _formulaireParamsApplicationVM;
			}
		}
		#endregion

		#region ConsultationSaisons
		private static ConsultationSaisonsUCViewModel _consultationSaisonsVM;

		public static ConsultationSaisonsUCViewModel ConsultationSaisonsVMStatic {
			get {
				if (_consultationSaisonsVM != null) {
					_consultationSaisonsVM.Cleanup();
					_consultationSaisonsVM = null;
				}

				_consultationSaisonsVM = new ConsultationSaisonsUCViewModel();

				return _consultationSaisonsVM;
			}
		}
		#endregion

		#region FormulaireSaison
		private static FormulaireSaisonUCViewModel _formulaireSaisonVM;

		public static FormulaireSaisonUCViewModel FormulaireSaisonVMStatic {
			get {
				if (_formulaireSaisonVM != null) {
					_formulaireSaisonVM.Cleanup();
					_formulaireSaisonVM = null;
				}

				_formulaireSaisonVM = new FormulaireSaisonUCViewModel();

				return _formulaireSaisonVM;
			}
		}
		#endregion

		#region ConsultationVilles
		private static ConsultationVillesUCViewModel _consultationVillesVM;

		public static ConsultationVillesUCViewModel ConsultationVillesVMStatic {
			get {
				if (_consultationVillesVM != null) {
					_consultationVillesVM.Cleanup();
					_consultationVillesVM = null;
				}

				_consultationVillesVM = new ConsultationVillesUCViewModel();

				return _consultationVillesVM;
			}
		}
		#endregion

		#region FormulaireVille
		private static FormulaireVilleUCViewModel _formulaireVilleVM;

		public static FormulaireVilleUCViewModel FormulaireVilleVMStatic {
			get {
				if (_formulaireVilleVM != null) {
					_formulaireVilleVM.Cleanup();
					_formulaireVilleVM = null;
				}

				_formulaireVilleVM = new FormulaireVilleUCViewModel();

				return _formulaireVilleVM;
			}
		}
		#endregion

		#region ConsultationAdherents
		private static ConsultationAdherentsUCViewModel _consultationAdherentsVM;

		public static ConsultationAdherentsUCViewModel ConsultationAdherentsVMStatic {
			get {
				if (_consultationAdherentsVM != null) {
					_consultationAdherentsVM.Cleanup();
					_consultationAdherentsVM = null;
				}

				_consultationAdherentsVM = new ConsultationAdherentsUCViewModel();

				return _consultationAdherentsVM;
			}
		}
		#endregion

		#region FormulaireAdherent
		private static FormulaireAdherentUCViewModel _formulaireAdherentVM;

		public static FormulaireAdherentUCViewModel FormulaireAdherentVMStatic {
			get {
				if (_formulaireAdherentVM != null) {
					_formulaireAdherentVM.Cleanup();
					_formulaireAdherentVM = null;
				}

				_formulaireAdherentVM = new FormulaireAdherentUCViewModel();

				return _formulaireAdherentVM;
			}
		}
		#endregion

		#region ConsultationsInscriptions
		private static ConsultationInscriptionsUCViewModel _consultationInscriptionsVM;

		public static ConsultationInscriptionsUCViewModel ConsultationInscriptionsVMStatic {
			get {
				if (_consultationInscriptionsVM != null) {
					_consultationInscriptionsVM.Cleanup();
					_consultationInscriptionsVM = null;
				}

				_consultationInscriptionsVM = new ConsultationInscriptionsUCViewModel();

				return _consultationInscriptionsVM;
			}
		}
		#endregion

		#region FormulaireInscription
		private static FormulaireInscriptionUCViewModel _formulaireInscriptionVM;

		public static FormulaireInscriptionUCViewModel FormulaireInscriptionVMStatic {
			get {
				if (_formulaireInscriptionVM != null) {
					_formulaireInscriptionVM.Cleanup();
					_formulaireInscriptionVM = null;
				}

				_formulaireInscriptionVM = new FormulaireInscriptionUCViewModel();

				return _formulaireInscriptionVM;
			}
		}
		#endregion
		
		#region ConsultationGroupes
		private static ConsultationGroupesUCViewModel _consultationGroupesVM;

		public static ConsultationGroupesUCViewModel ConsultationGroupesVMStatic {
			get {
				if (_consultationGroupesVM != null) {
					_consultationGroupesVM.Cleanup();
					_consultationGroupesVM = null;
				}

				_consultationGroupesVM = new ConsultationGroupesUCViewModel();

				return _consultationGroupesVM;
			}
		}
		#endregion

		#region FormulaireGroupe
		private static FormulaireGroupeUCViewModel _formulaireGroupeVM;

		public static FormulaireGroupeUCViewModel FormulaireGroupeVMStatic {
			get {
				if (_formulaireGroupeVM != null) {
					_formulaireGroupeVM.Cleanup();
					_formulaireGroupeVM = null;
				}

				_formulaireGroupeVM = new FormulaireGroupeUCViewModel();

				return _formulaireGroupeVM;
			}
		}
		#endregion

		#region GraphsSaisonCourante
		private static GraphsSaisonCouranteUCViewModel _graphsSaisonCouranteVM;

		public static GraphsSaisonCouranteUCViewModel GraphsSaisonCouranteVMStatic {
			get {
				if (_graphsSaisonCouranteVM != null) {
					_graphsSaisonCouranteVM.Cleanup();
					_graphsSaisonCouranteVM = null;
				}

				_graphsSaisonCouranteVM = new GraphsSaisonCouranteUCViewModel();

				return _graphsSaisonCouranteVM;
			}
		}
		#endregion
	}
}