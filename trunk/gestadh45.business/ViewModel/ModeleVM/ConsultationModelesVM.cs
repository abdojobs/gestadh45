using System.Linq;
using gestadh45.dal;
using gestadh45.business.PersonalizedMsg;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.ViewModel.ModeleVM
{
	public class ConsultationModelesVM : VMConsultationBase
	{

		#region Modeles
		private IOrderedEnumerable<Modele> _modeles;

		/// <summary>
		/// Obtient/Définit la liste des modeles
		/// </summary>
		public IOrderedEnumerable<Modele> Modeles {
			get { return this._modeles; }
			set {
				if (this._modeles != value) {
					this._modeles = value;
					this.RaisePropertyChanged(() => this.Modeles);
				}
			}
		}
		#endregion


		#region SelectedModele
		private Modele _selectedModele;

		/// <summary>
		/// Obtient/Définit le modèle sélectionné
		/// </summary>
		public Modele SelectedModele {
			get { return this._selectedModele; }
			set {
				if (this._selectedModele != value) {
					this._selectedModele = value;
					this.RaisePropertyChanged(() => this.SelectedModele);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<Modele> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationModelesVM() {
			this._repoMain = new Repository<Modele>(this._context);
			this.PopulateModeles();
		}
		#endregion

		private void PopulateModeles() {
			this.Modeles = this._repoMain.GetAll().OrderBy(m => m.Nom);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Modele) {
				this.SelectedModele = selectedItem as Modele;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedModele != null && this.SelectedModele.Equipements.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedModele != null) {
				this._repoMain.Delete(this.SelectedModele);
				this._repoMain.Save();
				this.PopulateModeles();
				this.SelectedModele = this.Modeles.FirstOrDefault();

				this.ShowUserNotification(ResModeles.InfoModeleSupprime);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireModele));
		}
		#endregion
	}
}
