
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using gestadh45.dal;
namespace gestadh45.Ihm.ViewModel.Stats
{
	public class StatsSaisonCouranteUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
		private decimal _totalCotisations;
		private int _nbAdherents;
		#endregion
		
		#region properties
		/// <summary>
		/// Gets or sets the total cotisations.
		/// </summary>
		/// <value>
		/// The total cotisations.
		/// </value>
		public decimal TotalCotisations {
			get { return this._totalCotisations; }
			set {
				if (this._totalCotisations != value) {
					this._totalCotisations = value;
					this.RaisePropertyChanged(() => this.TotalCotisations);
				}
			}
		}

		/// <summary>
		/// Gets or sets the nb adherents.
		/// </summary>
		/// <value>
		/// The nb adherents.
		/// </value>
		public int NbAdherents {
			get { return this._nbAdherents; }
			set {
				if (this._nbAdherents != value) {
					this._nbAdherents = value;
					this.RaisePropertyChanged(() => this.NbAdherents);
				}
			}
		}
		#endregion

		#region constructors
		public StatsSaisonCouranteUCViewModel() {
			this.CreateCalculerStatsCommand();

			this.CalculerStats();
		}
		#endregion

		#region CalculerStatsCommand
		public ICommand CalculerStatsCommand { get; set; }

		private void CreateCalculerStatsCommand() {
			this.CalculerStatsCommand = new RelayCommand(
				this.ExecuteCalculerStatsCommand, 
				this.CanExecuteCalculerStatsCommand);
		}

		public bool CanExecuteCalculerStatsCommand() {
			return true;
		}

		public void ExecuteCalculerStatsCommand() {
			this.CalculerStats();
		}
		#endregion

		#region private methods
		/// <summary>
		/// Recalcule toutes les stats
		/// </summary>
		private void CalculerStats() {
			var inscriptions = ViewModelLocator.DaoInscription.ListSaisonCourante();

			// calcul du total des cotisations
			this.TotalCotisations = this.CalculerTotalCotisations(inscriptions);

			// nb adherents
			this.NbAdherents = inscriptions.Count;
		}


		/// <summary>
		/// Calculers le total cotisations.
		/// </summary>
		/// <param name="pInscriptions">The inscriptions.</param>
		/// <returns>Total cotisations</returns>
		private decimal CalculerTotalCotisations(IList<Inscription> pInscriptions) {
			decimal total = 0;

			foreach (Inscription inscription in pInscriptions) {
				total += inscription.Cotisation ?? 0;
			}

			return total;
		}
		#endregion
	}
}
