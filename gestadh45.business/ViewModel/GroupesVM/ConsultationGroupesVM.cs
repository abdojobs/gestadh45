using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.dal;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;

namespace gestadh45.business.ViewModel.GroupesVM
{
	public class ConsultationGroupesVM : VMConsultationBase
	{
		#region Groupes
		private IOrderedEnumerable<Groupe> _groupes;

		/// <summary>
		/// Obtient/Définit la liste des groupes
		/// </summary>
		public IOrderedEnumerable<Groupe> Groupes {
			get { return this._groupes; }
			set {
				if (this._groupes != value) {
					this._groupes = value;
					this.RaisePropertyChanged(() => this.Groupes);
				}
			}
		}
		#endregion

		#region SelectedGroupe
		private Groupe _selectedGroupe;

		/// <summary>
		/// Obtient/Définit le groupe sélectionné
		/// </summary>
		public Groupe SelectedGroupe {
			get { return this._selectedGroupe; }
			set {
				if (this._selectedGroupe != value) {
					this._selectedGroupe = value;
					this.RaisePropertyChanged(() => this.SelectedGroupe);
				}
			}
		}
		#endregion

		#region Repositories
		private Repository<Groupe> repoMain;
		private Repository<InfosClub> repoInfosClub;
		#endregion

		#region Constructeur
		public ConsultationGroupesVM() {
			this.repoMain = new Repository<Groupe>(this._context);
			this.repoInfosClub = new Repository<InfosClub>(this._context);
			this.PopulateGroupes();
			this.CreateGenererDocumentsGroupeCommand();
		}
		#endregion

		private void PopulateGroupes() {
			// on se limite aux groupes de la saison courante
			this.Groupes = this.repoMain.GetAll().Where(g => g.Saison.EstSaisonCourante).OrderBy(g => g.JourSemaine.Numero).ThenBy(g => g.HeureDebut);
		}

		private void CreateCommandes() {
			this.CreateGenererDocumentsGroupeCommand();
		}

		private string GetDocumentFileName(string codeDocument, Inscription ins) {
			if (codeDocument.Equals(CodesDocument.AttestationPDF)) {
				return string.Format(ResDocuments.AttestationPDFFileName, ins.Adherent.ToString());
			}
			else {
				return string.Format(ResDocuments.InscriptionPDFFileName, ins.Adherent.ToString());
			}
		}

		private void GenererDocumentsCallBack(string folderPath, string codeDocument) {
			if (!string.IsNullOrWhiteSpace(folderPath)) {
				foreach (Inscription ins in this.SelectedGroupe.Inscriptions) {
					var gen = new GenerateurDocumentPDF(
						ServiceDocumentAdapter.InscriptionToDonneesDocument(this.repoInfosClub.GetFirst(), ins),
						string.Concat(folderPath, @"\", this.GetDocumentFileName(codeDocument, ins))
					);

					gen.CreerDocument(codeDocument);
				}

				this.ShowUserNotification(ResGroupes.InfosDocumentsGeneres);
			}
		}

		#region ShowDetailsCommand
		public override void ExecuteShowDetailsCommand(object selectedItem) {
			if (selectedItem is Adherent) {
				this.SelectedGroupe = selectedItem as Groupe;
			}
		}
		#endregion

		#region DeleteCommand
		public override bool CanExecuteDeleteCommand() {
			// On ne peut supprimer un groupe que si il n'est lié à aucune inscription
			return this.SelectedGroupe != null
				&& this.SelectedGroupe.Inscriptions.Count == 0;
		}

		public override void ExecuteDeleteCommand() {
			if (this.SelectedGroupe != null) {
				this.repoMain.Delete(this.SelectedGroupe);
				this.repoMain.Save();

				this.PopulateGroupes();
				this.SelectedGroupe = this.Groupes.FirstOrDefault();
				this.ShowUserNotification(ResGroupes.InfoGroupeSupprime);
			}
		}
		#endregion

		#region CreateCommand
		public override void ExecuteCreateCommand() {
			Messenger.Default.Send<NMShowUC>(new NMShowUC(CodesUC.FormulaireGroupe));
		}
		#endregion

		#region GenererDocumentGroupesCommand
		public ICommand GenererDocumentsGroupeCommand { get; set; }

		private void CreateGenererDocumentsGroupeCommand() {
			this.GenererDocumentsGroupeCommand = new RelayCommand<string>(
				this.ExecuteGenererDocumentsGroupeCommand,
				this.CanExecuteGenererDocumentsGroupeCommand
			);
		}

		public bool CanExecuteGenererDocumentsGroupeCommand(string codeDocument) {
			return this.SelectedGroupe != null;
		}

		public void ExecuteGenererDocumentsGroupeCommand(string codeDocument) {
			if (this.SelectedGroupe != null) {
				// recuperation du dossier d'enregistrement et passage au callback qui s'occupe de la génération a proprement parler
				Messenger.Default.Send<NMActionFolderDialog<string>>(
					new NMActionFolderDialog<string>(
						callback =>
						{
							this.GenererDocumentsCallBack(callback, codeDocument);
						}
					)
				);			
			}
		}
		#endregion
	}
}
