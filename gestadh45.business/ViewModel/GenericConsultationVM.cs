using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel
{
	public abstract class GenericConsultationVM<T> : VMUCBase where T : class, new()
	{

		#region Items
		private IOrderedEnumerable<T> _items;
		public IOrderedEnumerable<T> Items {
			get { return this._items; }
			set {
				if (this._items != value) {
					this._items = value;
					this.RaisePropertyChanged(() => this.Items);
				}
			}
		}
		#endregion


		#region SelectedItem
		private T _selectedItem;
		public T SelectedItem {
			get { return this._selectedItem; }
			set {
				if (this._selectedItem == null || !this._selectedItem.Equals(value)) {
					this._selectedItem = value;
					this.RaisePropertyChanged(() => this.SelectedItem);
				}
			}
		}
		#endregion

		#region Repositories
		protected Repository<T> _repoMain;
		#endregion

		#region Constructor
		public GenericConsultationVM() {
			this._repoMain = new Repository<T>(this._context);
			this.PopulateItems();

			this.CreateCreateCommand();
			this.CreateEditCommand();
			this.CreateDeleteCommand();
			this.CreateShowDetailsCommand();
			this.CreateFilterCommand();
		}
		#endregion

		protected void PopulateItems(string filter = null) {
			if (!string.IsNullOrEmpty(filter)) {
				this.Items = this._repoMain.GetAll().Where(i => i.ToString().ToUpperInvariant().Contains(filter.ToUpperInvariant())).OrderBy(i => i.ToString());
			}
			else {
				this.Items = this._repoMain.GetAll().OrderBy(i => i.ToString());
				Messenger.Default.Send(new NMClearFilter());
			}
		}

		#region ShowDetailsCommand

		#endregion

		#region CreateCommand
		public ICommand CreateCommand { get; set; }

		private void CreateCreateCommand() {
			this.CreateCommand = new RelayCommand(
				this.ExecuteCreateCommand,
				this.CanExecuteCreateCommand
			);
		}

		public virtual bool CanExecuteCreateCommand() {
			// la création est autorisée dans tous les cas
			return true;
		}

		public virtual void ExecuteCreateCommand() { }
		#endregion

		#region EditCommand
		public ICommand EditCommand { get; set; }

		private void CreateEditCommand() {
			this.EditCommand = new RelayCommand(
				this.ExecuteEditCommand,
				this.CanExecuteEditCommand
			);
		}

		public virtual bool CanExecuteEditCommand() {
			// l'édition est interdite par défaut (à redéfinir si besoin dans les classes filles)
			return false;
		}

		// A redéfinir dans les classes filles...
		public virtual void ExecuteEditCommand() { }
		#endregion

		#region DeleteCommand
		public ICommand DeleteCommand { get; set; }

		private void CreateDeleteCommand() {
			this.DeleteCommand = new RelayCommand(
				this.ExecuteDeleteCommand,
				this.CanExecuteDeleteCommand
			);
		}

		public virtual bool CanExecuteDeleteCommand() {
			return this.SelectedItem != null;
		}

		public virtual void ExecuteDeleteCommand() {
			if (this.SelectedItem != null) {
				var libelle = this.SelectedItem.ToString();

				this._repoMain.Delete(this.SelectedItem);
				this._repoMain.Save();
				this.PopulateItems();
				this.SelectedItem = this.Items.FirstOrDefault();

				this.ShowUserNotification(string.Format("{0} supprimé", libelle));
			}
		}
		#endregion

		#region ShowDetailsCommand
		public ICommand ShowDetailsCommand { get; set; }

		private void CreateShowDetailsCommand() {
			this.ShowDetailsCommand = new RelayCommand<object>(
				this.ExecuteShowDetailsCommand,
				this.CanExecuteShowDetailsCommand
			);
		}

		public virtual bool CanExecuteShowDetailsCommand(object selectedItem) {
			return true;
		}

		public virtual void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem != null && selectedItem is T) {
				this.SelectedItem = (T)selectedItem;
			}
		}
		#endregion

		#region FilterCommand
		public ICommand FilterCommand { get; set; }

		private void CreateFilterCommand() {
			this.FilterCommand = new RelayCommand<string>(
				this.ExecuteFilterCommand,
				this.CanExecuteFilterCommand
			);
		}

		public virtual bool CanExecuteFilterCommand(string filtre) {
			return true;
		}

		public virtual void ExecuteFilterCommand(string filtre) {
			this.PopulateItems(filtre);
		}
		#endregion
	}
}
