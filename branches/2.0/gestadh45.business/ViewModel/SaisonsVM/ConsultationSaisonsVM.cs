using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.SaisonsVM
{
	public class ConsultationSaisonsVM : VMConsultationBase
	{
		#region Saisons
		private IOrderedEnumerable<Saison> _saisons;

		/// <summary>
		/// Obtient/Définit la liste des saisons
		/// </summary>
		public IOrderedEnumerable<Saison> Saisons {
			get { return this._saisons; }
			set {
				if (this._saisons != value) {
					this._saisons = value;
					this.RaisePropertyChanged(() => this.Saisons);
				}
			}
		}
		#endregion

		#region SelectedSaison
		private Saison _selectedSaison;

		/// <summary>
		/// Obtient/Définit la saison sélectionnée
		/// </summary>
		public Saison SelectedSaison {
			get { return this._selectedSaison; }
			set {
				if (this._selectedSaison != value) {
					this._selectedSaison = value;
					this.RaisePropertyChanged(() => this.SelectedSaison);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<Saison> repoMain;
		#endregion

		public ConsultationSaisonsVM() {
			this.repoMain = new Repository<Saison>(this._context);
			this.PopulatesSaisons();
		}

		private void PopulatesSaisons() {
			this.Saisons = this.repoMain.GetAll().OrderBy((s) => s.AnneeDebut);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Saison) {
				this.SelectedSaison = (Saison)selectedItem;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedSaison != null && this.SelectedSaison.Groupes.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedSaison != null) {
				this.repoMain.Delete(this.SelectedSaison);
				this.repoMain.Save();
				this.PopulatesSaisons();
				this.SelectedSaison = this.Saisons.FirstOrDefault();
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireVille));
		}
		#endregion
	}
}
