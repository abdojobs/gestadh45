using System.ComponentModel;
using System.Windows.Data;
using gestadh45.dal;

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
	}
}
