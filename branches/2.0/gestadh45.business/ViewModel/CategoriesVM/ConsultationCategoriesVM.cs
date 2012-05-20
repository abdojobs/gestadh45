using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.CategoriesVM
{
	public class ConsultationCategoriesVM : VMConsultationBase
	{
		#region Categories
		private IOrderedEnumerable<Categorie> _categorie;

		/// <summary>
		/// Gets or sets the categories.
		/// </summary>
		/// <value>
		/// The categories.
		/// </value>
		public IOrderedEnumerable<Categorie> Categories {
			get {
				return this._categorie;
			}

			set {
				if (this._categorie != value) {
					this._categorie = value;
					this.RaisePropertyChanged(() => this.Categories);
				}
			}
		}
		#endregion

		#region SelectedCategorie
		private Categorie _selectedCategorie;

		/// <summary>
		/// Gets or sets the selected categorie.
		/// </summary>
		/// <value>
		/// The selected categorie.
		/// </value>
		public Categorie SelectedCategorie {
			get {
				return this._selectedCategorie;
			}

			set {
				if (this._selectedCategorie != value) {
					this._selectedCategorie = value;
					this.RaisePropertyChanged(() => this.SelectedCategorie);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<Categorie> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationCategoriesVM() {
			this._repoMain = new Repository<Categorie>(this._context);
			this.PopulateCategories();
		}
		#endregion

		private void PopulateCategories() {
			this.Categories = this._repoMain.GetAll().OrderBy(c => c.ToString());
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Categorie) {
				this.SelectedCategorie = selectedItem as Categorie;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedCategorie != null && this.SelectedCategorie.Equipements.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedCategorie != null) {
				this._repoMain.Delete(this.SelectedCategorie);
				this._repoMain.Save();
				this.PopulateCategories();
				this.SelectedCategorie = this.Categories.FirstOrDefault();

				this.ShowUserNotification(ResCategories.InfoCategorieSupprimee);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireCategorie));
		}
		#endregion
	}
}
