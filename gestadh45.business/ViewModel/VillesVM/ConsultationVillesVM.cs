using System.ComponentModel;
using System.Windows.Data;
using gestadh45.dal;
using System.Collections;
using System.Collections.Generic;

namespace gestadh45.business.ViewModel.VillesVM
{
	public class ConsultationVillesVM : VMConsultationBase
	{
		#region Villes
		private ICollectionView _villes;

		/// <summary>
		/// Obtient/Définit la liste des villes
		/// </summary>
		public ICollectionView Villes {
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
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.repoMain.GetAll());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Ville) {
				this.SelectedVille = (Ville)selectedItem;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedVille != null && this.SelectedVille.Adherents.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			// TODO travail en cours ici
			this.repoMain.Delete(this.SelectedVille);
			this.repoMain.Save();
			this.PopulateVilles();
			this.SelectedVille = (Ville)this.Villes.CurrentItem;
		}
		#endregion
	}
}
