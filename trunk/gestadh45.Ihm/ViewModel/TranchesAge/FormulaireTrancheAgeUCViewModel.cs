
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;
namespace gestadh45.Ihm.ViewModel.TranchesAge
{
	public class FormulaireTrancheAgeUCViewModel : ViewModelBaseFormulaire
	{
		#region private fields
		private TrancheAge _trancheAge;
		#endregion

		#region properties
		/// <summary>
		/// Obtient/Définit la tranche d'âge du formulaire
		/// </summary>
		public TrancheAge TrancheAge {
			get { return this._trancheAge; }
			set {
				if (this._trancheAge != value) {
					this._trancheAge = value;
					this.RaisePropertyChanged(() => this.TrancheAge);
				}
			}
		}
		#endregion

		#region constructor
		public FormulaireTrancheAgeUCViewModel() {
			this.TrancheAge = new TrancheAge();
			this.CodeUCOrigine = CodesUC.ConsultationTranchesAge;
		}
		#endregion

		#region EnregistrerCommand
		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie() && !ViewModelLocator.DaoTrancheAge.Exists(this.TrancheAge)) {
				ViewModelLocator.DaoTrancheAge.Create(this.TrancheAge);

				base.ExecuteEnregistrerCommand();

				var msg = new MsgSelectionElement<TrancheAge>(this.TrancheAge);
				Messenger.Default.Send(msg);
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}
		#endregion

		#region override methods
		protected override bool VerifierSaisie() {
			List<string> erreurs = new List<string>();

			// on vérifie que l'age de début soit positif
			if (this.TrancheAge.AgeInf < 0) {
				erreurs.Add(ResErreurs.TrancheAge_AgeInfPositif);
			}

			// on vérifie que l'âge de fin soit positif
			if (this.TrancheAge.AgeSup < 0) {
				erreurs.Add(ResErreurs.TrancheAge_AgeSupPositif);
			}
			
			// on vérifie que l'âge de fin soit supérieur à l'âge de début
			if (erreurs.Count == 0 && this.TrancheAge.AgeSup < this.TrancheAge.AgeInf) {
				erreurs.Add(ResErreurs.TrancheAge_OrdreAges);
			}

			// on vérifie que la tranche n'existe pas déjà
			if (erreurs.Count == 0 && ViewModelLocator.DaoTrancheAge.Exists(this.TrancheAge)) {
				erreurs.Add(ResErreurs.TrancheAge_Existe);
			}

			this.Erreurs = new List<string>(erreurs);
			return this.Erreurs.Count == 0;
		}
		#endregion
	}
}
