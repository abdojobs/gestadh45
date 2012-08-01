using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.LocalisationVM
{
	public class ConsultationLocalisationsVM : VMConsultationBase
	{
		#region Localisations
		private IOrderedEnumerable<Localisation> _localisations;

		/// <summary>
		/// Obtient/Définit la liste des localisations
		/// </summary>
		public IOrderedEnumerable<Localisation> Localisations {
			get { return this._localisations; }
			set {
				if (this._localisations != value) {
					this._localisations = value;
					this.RaisePropertyChanged(() => this.Localisations);
				}
			}
		}
		#endregion


		#region SelectedLocalisation
		private Localisation _selectedLocalisation;

		/// <summary>
		/// Obtient/Définit la localisation sélectionnée
		/// </summary>
		public Localisation SelectedLocalisation {
			get { return this._selectedLocalisation; }
			set {
				if (this._selectedLocalisation != value) {
					this._selectedLocalisation = value;
					this.RaisePropertyChanged(() => this.SelectedLocalisation);
				}
			}
		}
		#endregion
				

		#region Repositories
		private Repository<Localisation> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationLocalisationsVM() {
			this._repoMain = new Repository<Localisation>(this._context);
			this.PopulateLocalisations();
		}
		#endregion

		private void PopulateLocalisations() {
			this.Localisations = this._repoMain.GetAll().OrderBy(l => l.Libelle);
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Localisation) {
				this.SelectedLocalisation = selectedItem as Localisation;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedLocalisation != null && this.SelectedLocalisation.Equipements.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedLocalisation != null) {
				this._repoMain.Delete(this.SelectedLocalisation);
				this._repoMain.Save();
				this.PopulateLocalisations();
				this.SelectedLocalisation = this.Localisations.FirstOrDefault();

				this.ShowUserNotification(ResLocalisation.InfoSuppressionLocalisation);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireLocalisation));
		}
		#endregion
	}
}
