using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.InscriptionsVM
{
	public class ConsultationInscriptionsVM : VMConsultationBase
	{
		#region Inscriptions
		private IOrderedEnumerable<Inscription> _inscriptions;

		/// <summary>
		/// Obtient/Définit la liste des inscriptions
		/// </summary>
		public IOrderedEnumerable<Inscription> Inscriptions {
			get { return this._inscriptions; }
			set {
				if (this._inscriptions != value) {
					this._inscriptions = value;
					this.RaisePropertyChanged(() => this.Inscriptions);
				}
			}
		}
		#endregion

		#region SelectedInscription
		private Inscription _selectedInscription;

		/// <summary>
		/// Obtient/Définit l'inscription sélectionnée
		/// </summary>
		public Inscription SelectedInscription {
			get { return this._selectedInscription; }
			set {
				if (this._selectedInscription != value) {
					this._selectedInscription = value;
					this.RaisePropertyChanged(()=>this.SelectedInscription);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Inscription> repoMain;
		#endregion

		#region Constructeur
		public ConsultationInscriptionsVM() {
			this.repoMain = new Repository<Inscription>(this._context);

			this.PopulateInscriptions();
			this.CreateCommands();
		}
		#endregion

		private void PopulateInscriptions(string filtre = null) {
			// on se limite aux inscriptions de la saison courante
			var ins = this.repoMain.GetAll().Where(i => i.Groupe.Saison.EstSaisonCourante);

			if (!string.IsNullOrEmpty(filtre)) {
				ins = ins.Where(i => i.ToString().ToUpperInvariant().Contains(filtre.ToUpperInvariant()));
			}
			else {
				Messenger.Default.Send(new NMClearFilter());
			}

			this.Inscriptions = ins.OrderBy(i => i.ToString());
		}

		private void CreateCommands() {
			this.CreateGenererDocumentCommand();
			this.CreateGenererVCardCommand();
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Inscription) {
				this.SelectedInscription = selectedItem as Inscription;
			}
		}
		#endregion

		#region DeleteCommand
		public override void ExecuteDeleteCommand() {
			if (this.SelectedInscription != null) {
				this.repoMain.Delete(this.SelectedInscription);
				this.repoMain.Save();

				this.PopulateInscriptions();
				this.SelectedInscription = this.Inscriptions.FirstOrDefault();
				this.ShowUserNotification(ResInscriptions.InfosInscriptionSupprimee);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireInscription));
		}
		#endregion

		#region EditCommand
		public override bool CanExecuteEditCommand() {
			return this.SelectedInscription != null;
		}

		public override void ExecuteEditCommand() {
			if (this.SelectedInscription != null) {
				Messenger.Default.Send<NMShowUC<Inscription>>(
					new NMShowUC<Inscription>(CodesUC.FormulaireInscription, this.SelectedInscription)
				);
			}
		}
		#endregion

		#region FilterCommand
		public override void ExecuteFilterCommand(string filtre) {
			this.PopulateInscriptions(filtre);
		}
		#endregion

		#region GenererDocumentCommand
		public ICommand GenererDocumentCommand { get; set; }

		private void CreateGenererDocumentCommand() {
			this.GenererDocumentCommand = new RelayCommand<string>(
				this.ExecuteGenererDocumentCommand,
				this.CanExecuteGenererDocumentCommand
			);
		}

		public bool CanExecuteGenererDocumentCommand(string codeDocument) {
			return this.SelectedInscription != null;
		}

		public void ExecuteGenererDocumentCommand(string codeDocument) {
			if (this.SelectedInscription != null) {
				// TODO implémenter

				this.ShowUserNotification(ResInscriptions.InfosDocumentGenere);
			}
		}
		#endregion

		#region GenererVCardCommand
		public ICommand GenererVCardCommand { get; set; }

		private void CreateGenererVCardCommand() {
			this.GenererVCardCommand = new RelayCommand(
				this.ExecuteGenererVCardCommand,
				this.CanExecuteGenererVCardCommand
			);
		}

		public bool CanExecuteGenererVCardCommand() {
			return this.SelectedInscription != null;
		}

		public void ExecuteGenererVCardCommand() {
			if (this.SelectedInscription != null) {
				// TODO implémenter

				this.ShowUserNotification(ResInscriptions.InfosVCardGeneree);
			}
		}
		#endregion
	}
}
