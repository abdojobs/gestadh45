using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.VillesVM
{
	public class ConsultationVillesVM : VMConsultationBase
	{
		#region Villes
		private IOrderedEnumerable<Ville> _villes;

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public IOrderedEnumerable<Ville> Villes {
			get { return this._villes; }
			set {
				if (this._villes != value) {
					this._villes = value;
					this.RaisePropertyChanged(() => this.Villes);
				}
			}
		}
		#endregion

		#region SelectedVille
		private Ville _selectedVille;

		/// <summary>
		/// Obtient/Définit la ville sélectionnée
		/// </summary>
		public Ville SelectedVille {
			get { return this._selectedVille; }
			set {
				if (this._selectedVille != value) {
					this._selectedVille = value;
					this.RaisePropertyChanged(() => this.SelectedVille);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<Ville> repoMain;
		#endregion

		public ConsultationVillesVM() {
			this.repoMain = new Repository<Ville>(this._context);
			this.PopulateVilles();
		}

		private void PopulateVilles() {
			this.Villes = this.repoMain.GetAll().OrderBy((v) => v.Libelle);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Ville) {
				this.SelectedVille = selectedItem as Ville;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedVille != null && this.SelectedVille.Adherents.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedVille != null) {
				this.repoMain.Delete(this.SelectedVille);
				this.repoMain.Save();
				this.PopulateVilles();
				this.SelectedVille = this.Villes.FirstOrDefault();

				this.ShowUserNotification(ResVilles.InfoVilleSupprimee);
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
