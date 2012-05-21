using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.EquipementsVM
{
	public class ConsultationEquipementsVM : VMConsultationBase
	{
		#region Equipements
		private IOrderedEnumerable<Equipement> _equipements;

		/// <summary>
		/// Gets or sets the equipements.
		/// </summary>
		/// <value>
		/// The equipements.
		/// </value>
		public IOrderedEnumerable<Equipement> Equipements {
			get {
				return this._equipements;
			}

			set {
				if (this._equipements != value) {
					this._equipements = value;
					this.RaisePropertyChanged(() => this.Equipements);
				}
			}
		}
		#endregion

		#region SelectedEquipement
		private Equipement _selectedEquipement;

		/// <summary>
		/// Gets or sets the selected equipement.
		/// </summary>
		/// <value>
		/// The selected equipement.
		/// </value>
		public Equipement SelectedEquipement {
			get {
				return this._selectedEquipement;
			}

			set {
				if (this._selectedEquipement != value) {
					this._selectedEquipement = value;
					this.RaisePropertyChanged(() => this.SelectedEquipement);
				}
			}
		}
		#endregion

		#region MasquerRebut
		private bool _masquerRebut;

		/// <summary>
		/// Get or sets the MasquerRebut flag
		/// </summary>
		public bool MasquerRebut {
			get { return this._masquerRebut; }
			set {
				if (this._masquerRebut != value) {
					this._masquerRebut = value;
					this.RaisePropertyChanged(() => this.MasquerRebut);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<Equipement> _repoMain;
		#endregion

		#region Constructeur
		public ConsultationEquipementsVM() {
			this.MasquerRebut = true;
			this._repoMain = new Repository<Equipement>(this._context);
			this.PopulateEquipements();
			this.CreateDupliquerCommand();
			this.CreateMasquerRebutCommand();
		}
		#endregion

		private void PopulateEquipements(string filtre = null) {
			this.Equipements = this._repoMain.GetAll().OrderBy(e => e.ToString());

			// pour masquer les équipement au rebut, on filtre sur l'absence d'une date de mise au rebut
			if (this.MasquerRebut) {
				this.Equipements = this.Equipements.Where(e => !e.DateMiseAuRebut.HasValue).OrderBy(e => e.ToString());
			}

			// gestion du filtre
			if (!string.IsNullOrEmpty(filtre)) {
				this.Equipements = this.Equipements.Where(e => e.ToString().ToUpperInvariant().Contains(filtre.ToUpperInvariant())).OrderBy(e => e.ToString());
			}
			else {
				Messenger.Default.Send(new NMClearFilter());
			}
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Adherent) {
				this.SelectedEquipement = selectedItem as Equipement;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			return this.SelectedEquipement != null;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedEquipement != null) {
				this._repoMain.Delete(this.SelectedEquipement);
				this._repoMain.Save();

				this.PopulateEquipements();
				this.SelectedEquipement = this.Equipements.FirstOrDefault();
				this.ShowUserNotification(ResEquipements.InfoEquipementSupprime);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireEquipement));
		}
		#endregion

		#region EditCommand
		public override bool CanExecuteEditCommand() {
			return this.SelectedEquipement != null;
		}

		public override void ExecuteEditCommand() {
			if (this.SelectedEquipement != null) {
				Messenger.Default.Send<NMShowUC<Equipement>>(
					new NMShowUC<Equipement>(CodesUC.FormulaireEquipement, this.SelectedEquipement)
				);
			}
		}
		#endregion

		#region FilterCommand
		public override void ExecuteFilterCommand(string filtre) {
			this.PopulateEquipements(filtre);
		}
		#endregion

		#region DupliquerCommand
		public ICommand DupliquerCommand {
			get;
			set;
		}

		private void CreateDupliquerCommand() {
			this.DupliquerCommand = new RelayCommand(
				this.ExecuteDupliquerCommand,
				this.CanExecuteDupliquerCommand
			);
		}

		public bool CanExecuteDupliquerCommand() {
			return this.SelectedEquipement != null;
		}

		public void ExecuteDupliquerCommand() {
			var newEquipement = this.SelectedEquipement.Clone() as Equipement;

			newEquipement.Numero += " (copie)";
			newEquipement.Commentaire = "Copie de " + this.SelectedEquipement.ToString();

			this._repoMain.Add(newEquipement);
			this._repoMain.Save();

			this.PopulateEquipements();
			this.SelectedEquipement = this.Equipements.FirstOrDefault();
			this.ShowUserNotification(ResEquipements.InfoEquipementDuplique);
		}
		#endregion

		#region MasquerRebutCommand
		public ICommand MasquerRebutCommand { get; set; }

		private void CreateMasquerRebutCommand() {
			this.MasquerRebutCommand = new RelayCommand<object>(
				this.ExecuteMasquerRebutCommand,
				this.CanExecuteMasquerRebutCommand
			);
		}

		public bool CanExecuteMasquerRebutCommand(object masquerRebut) {
			return true;
		}

		public void ExecuteMasquerRebutCommand(object masquerRebut) {
			this.MasquerRebut = (bool)masquerRebut;
			this.PopulateEquipements(null);
		}
		#endregion
	}
}
