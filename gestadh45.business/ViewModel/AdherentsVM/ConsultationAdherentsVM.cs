using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.AdherentsVM
{
	public class ConsultationAdherentsVM : VMConsultationBase
	{
		#region Adherents
		private IOrderedEnumerable<Adherent> _adherents;

		/// <summary>
		/// Obtient/Définit la liste des adhérents
		/// </summary>
		public IOrderedEnumerable<Adherent> Adherents {
			get { return this._adherents; }
			set {
				if (this._adherents != value) {
					this._adherents = value;
					this.RaisePropertyChanged(() => this.Adherents);
				}
			}
		}
		#endregion

		#region SelectedAdherent
		private Adherent _selectedAdherent;

		/// <summary>
		/// Obtient/Définit l'adhérent sélectionné
		/// </summary>
		public Adherent SelectedAdherent {
			get { return this._selectedAdherent; }
			set {
				if (this._selectedAdherent != value) {
					this._selectedAdherent = value;
					this.RaisePropertyChanged(() => this.SelectedAdherent);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Adherent> repoMain;
		#endregion

		#region Constructeur
		public ConsultationAdherentsVM() {
			this.repoMain = new Repository<Adherent>(this._context);

			this.CreateInscrireCommand();
			this.CreateDupliquerCommand();

			this.PopulateAdherents();
		}
		#endregion

		private void PopulateAdherents(string filtre = null) {
			if (!string.IsNullOrEmpty(filtre)) {
				this.Adherents = this.repoMain.GetAll().Where(a => a.ToString().ToUpperInvariant().Contains(filtre.ToUpperInvariant())).OrderBy(a => a.ToString());
			}
			else {
				this.Adherents = this.repoMain.GetAll().OrderBy(a => a.ToString());
				Messenger.Default.Send(new NMClearFilter());
			}
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Adherent) {
				this.SelectedAdherent = selectedItem as Adherent;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedAdherent != null
				&& this.SelectedAdherent.Inscriptions.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedAdherent != null) {
				this.repoMain.Delete(this.SelectedAdherent);
				this.repoMain.Save();

				this.PopulateAdherents();
				this.SelectedAdherent = this.Adherents.FirstOrDefault();
				this.ShowUserNotification(ResAdherents.InfosAdherentSupprime);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireAdherent));
		}
		#endregion

		#region EditCommand
		public override bool CanExecuteEditCommand() {
			return this.SelectedAdherent != null;
		}

		public override void ExecuteEditCommand() {
			if (this.SelectedAdherent != null) {
				Messenger.Default.Send<NMShowUC<Adherent>>(
					new NMShowUC<Adherent>(CodesUC.FormulaireAdherent, this.SelectedAdherent)
				);
			}
		}
		#endregion

		#region FilterCommand
		public override void ExecuteFilterCommand(string filtre) {
			this.PopulateAdherents(filtre);
		}
		#endregion

		#region InscrireCommand
		public ICommand InscrireCommand { get; set; }

		private void CreateInscrireCommand() {
			this.InscrireCommand = new RelayCommand(
				this.ExecuteInscrireCommand,
				this.CanExecuteInscrireCommand
			);
		}

		public bool CanExecuteInscrireCommand() {
			// la commande Inscrire est acctivée seulement si un adhérent est sélectionné 
			// ET qu'il n'a encore aucune inscription sur la saison courante
			return this.SelectedAdherent != null 
				&& this.SelectedAdherent.Inscriptions.Where(i => i.Groupe.Saison.EstSaisonCourante).Count() == 0;
		}

		public void ExecuteInscrireCommand() {
			Messenger.Default.Send<NMShowUC<Adherent>>(
				new NMShowUC<Adherent>(CodesUC.FormulaireInscription, this.SelectedAdherent)
			);
		}
		#endregion

		#region DupliquerCommand
		public ICommand DupliquerCommand { get; set; }

		private void CreateDupliquerCommand() {
			this.DupliquerCommand = new RelayCommand(
				this.ExecuteDupliquerCommand,
				this.CanExecuteDupliquerCommand
			);
		}

		public bool CanExecuteDupliquerCommand() {
			return this.SelectedAdherent != null;
		}

		public void ExecuteDupliquerCommand() {
			var newAdherent = this.SelectedAdherent.Clone() as Adherent;

			newAdherent.ID = Guid.NewGuid();
			newAdherent.Prenom += " (copie)";
			newAdherent.Commentaire = "Copie de " + this.SelectedAdherent.ToString();

			this.repoMain.Add(newAdherent);
			this.repoMain.Save();

			this.PopulateAdherents();
			this.SelectedAdherent = this.Adherents.FirstOrDefault();
			this.ShowUserNotification(ResAdherents.InfosAdherentDuplique);
		}
		#endregion
	}
}
