using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;
using System.Collections.Generic;

namespace gestadh45.business.ViewModel.OutilsVM
{
	public class NettoyageCNILVM : VMUCBase
	{

		#region OldAdherents
		private IOrderedEnumerable<Adherent> _oldAdherents;
		public IOrderedEnumerable<Adherent> OldAdherents {
			get { return this._oldAdherents; }
			set {
				if (this._oldAdherents != value) {
					this._oldAdherents = value;
					this.RaisePropertyChanged(() => this.OldAdherents);
				}
			}
		}
		#endregion				
		
		#region Repositories
		private Repository<Adherent> _repoAdherents;
		private Repository<Inscription> _repoInscriptions;
		#endregion

		#region Constructor
		public NettoyageCNILVM() {
			this._repoAdherents = new Repository<Adherent>(this._context);
			this._repoInscriptions = new Repository<Inscription>(this._context);

			this.CreateGetOldInscriptionsCommand();
			this.CreateCleanDatasCommand();
		}
		#endregion

		#region GetOldAdherentsCommand
		public ICommand GetOldAdherentsCommand { get; set; }

		private void CreateGetOldInscriptionsCommand() {
			this.GetOldAdherentsCommand = new RelayCommand(
				this.ExecuteGetOldAdherentsCommand,
				this.CanExecuteGetOldAdherentsCommand
			);
		}

		public bool CanExecuteGetOldAdherentsCommand() {
			return true;
		}

		public void ExecuteGetOldAdherentsCommand() {
			// on récupére tous les adhérents qui n'ont pas d'inscription sur la saison courante
			this.OldAdherents = this._repoAdherents.GetAll()
				.Where(a => a.Inscriptions.Count(i => i.Groupe.Saison.EstSaisonCourante) == 0)
				.OrderBy(a => a.Nom)
				.ThenBy(a => a.Prenom
			);
		}
		#endregion

		#region CleanDatasCommand
		public ICommand CleanDatasCommand { get; set; }

		private void CreateCleanDatasCommand() {
			this.CleanDatasCommand = new RelayCommand(
				this.ExecuteCleanDatasCommand,
				this.CanExecuteCleanDatasCommand
			);
		}

		public bool CanExecuteCleanDatasCommand() {
			return this.OldAdherents != null && this.OldAdherents.Count() > 0;
		}

		public void ExecuteCleanDatasCommand() {
			Messenger.Default.Send(
				new NMAskConfirmationDialog<bool>(
					this.CleanDatas, 
					string.Format(ResNettoyageCNIL.ConfirmationNettoyage, this.OldAdherents.Count())
				)
			);
		}

		private void CleanDatas(bool doClean) {
			foreach (var adh in this.OldAdherents) {
				foreach (var ins in adh.Inscriptions) {
					this._repoInscriptions.Delete(ins);
				}

				this._repoAdherents.Delete(adh);
			}

			this._repoAdherents.Save();
		}
		#endregion
	}
}
