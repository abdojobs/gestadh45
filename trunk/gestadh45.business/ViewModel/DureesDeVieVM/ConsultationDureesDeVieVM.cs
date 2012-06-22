using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.DureesDeVieVM
{
	public class ConsultationDureesDeVieVM : VMConsultationBase
	{
		#region DureesDeVie
		private IOrderedEnumerable<DureeDeVie> _dureesDeVie;

		/// <summary>
		/// Obtient/Définit la liste des durrées de vie
		/// </summary>
		public IOrderedEnumerable<DureeDeVie> DureesDeVie {
			get { return this._dureesDeVie; }
			set {
				if (this._dureesDeVie != value) {
					this._dureesDeVie = value;
					this.RaisePropertyChanged(() => this.DureesDeVie);
				}
			}
		}
		#endregion

		#region SelectedDureeDeVie
		private DureeDeVie _selectedDureeDeVie;

		/// <summary>
		/// Obtient/Définit la durée de vie sélectionnée
		/// </summary>
		public DureeDeVie SelectedDureeDeVie {
			get { return this._selectedDureeDeVie; }
			set {
				if (this._selectedDureeDeVie != value) {
					this._selectedDureeDeVie = value;
					this.RaisePropertyChanged(() => this.SelectedDureeDeVie);
				}
			}
		}
		#endregion

		#region repositories
		private Repository<DureeDeVie> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationDureesDeVieVM() {
			this._repoMain = new Repository<DureeDeVie>(this._context);
			this.PopulateDureesDeVie();
		}
		#endregion

		private void PopulateDureesDeVie() {
			this.DureesDeVie = this._repoMain.GetAll().OrderBy(d => d.Libelle);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is DureeDeVie) {
				this.SelectedDureeDeVie = selectedItem as DureeDeVie;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedDureeDeVie != null;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedDureeDeVie != null) {
				this._repoMain.Delete(this.SelectedDureeDeVie);
				this._repoMain.Save();
				this.PopulateDureesDeVie();
				this.SelectedDureeDeVie = this.DureesDeVie.FirstOrDefault();

				this.ShowUserNotification(ResDureesDeVie.InfoDureeDeVieSupprimee);
			}
		}
		#endregion

		#region EditCommand
		public override bool CanExecuteEditCommand() {
			return this.SelectedDureeDeVie != null;
		}

		public override void ExecuteEditCommand() {
			if (this.SelectedDureeDeVie != null) {
				Messenger.Default.Send<NMShowUC<DureeDeVie>>(
					new NMShowUC<DureeDeVie>(CodesUC.FormulaireDureeDeVie, this.SelectedDureeDeVie)
				);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireDureeDeVie);
		}
		#endregion
	}
}
