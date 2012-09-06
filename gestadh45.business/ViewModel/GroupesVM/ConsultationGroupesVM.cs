using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.business.ServicesAdapters;
using gestadh45.dal;
using gestadh45.services.Documents;
using gestadh45.services.Documents.Templates;
using gestadh45.services.Reporting;
using gestadh45.services.Reporting.Templates;
using gestadh45.services.VCards;
using System.Collections.Generic;

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
			this.CreateGenererVCardsGroupeDistinctCommand();
			this.CreateGenererVCardsGroupeUniqueCommand();
			this.CreateExtraireEmailsCommand();
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

		private void GenererDocumentsCallBack(string folderPath, string codeDocument) {
			if (!string.IsNullOrWhiteSpace(folderPath)) {
				foreach (Inscription ins in this.SelectedGroupe.Inscriptions.Where(i => i.StatutInscription.Ordre != 3)) {
					var gen = new GenerateurDocumentPDF(
						ServiceDocumentAdapter.InscriptionToDonneesDocument(this.repoInfosClub.GetFirst(), ins),
						string.Concat(folderPath, @"\", this.GetDocumentFileName(codeDocument, ins))
					);

					gen.CreerDocument(codeDocument);
				}

				this.ShowUserNotification(ResGroupes.InfosDocumentsGeneres);
			}
		}
		#endregion

		#region GenererVCardsGroupeDistinctCommand
		public ICommand GenererVCardsGroupeDistinctCommand {
			get;
			set;
		}

		private void CreateGenererVCardsGroupeDistinctCommand() {
			this.GenererVCardsGroupeDistinctCommand = new RelayCommand(
				this.ExecuteGenererVCardsGroupeDistinctCommand,
				this.CanExecuteGenererVCardsGroupeDistinctCommand
			);
		}

		public bool CanExecuteGenererVCardsGroupeDistinctCommand() {
			return this.SelectedGroupe != null;
		}

		public void ExecuteGenererVCardsGroupeDistinctCommand() {
			if (this.SelectedGroupe != null) {
				// recuperation du chemin d'enregistrement et passage au callback qui s'occupe de la génération a proprement parler
				Messenger.Default.Send<NMActionFolderDialog<string>>(
					new NMActionFolderDialog<string>(
						callback =>
						{
							this.GenererVCardGroupeDistinctCallBack(callback);
						}
					)
				);
			}
		}

		private void GenererVCardGroupeDistinctCallBack(string savePath) {
			if (!string.IsNullOrWhiteSpace(savePath)) {
				foreach (Inscription ins in this.SelectedGroupe.Inscriptions.Where(ins => ins.StatutInscription.Ordre != 3)) {
					var gen = new VcardGenerator21(ins.Adherent.Prenom, ins.Adherent.Nom);

					gen.AddEmailInternet(ins.Adherent.Mail1);
					gen.AddTelWork(ins.Adherent.Telephone1);
					gen.AddOrganization(ins.Groupe.ToString());

					var fileName = string.Concat(savePath, "/", ins.Adherent.ToString(), ResVCards.ExtensionVcf);

					using (var sw = new StreamWriter(fileName)) {
						sw.Write(gen.GetVCard());
					}
				}

				this.ShowUserNotification(string.Format(ResGroupes.InfosVCardsDistinctGenerees, this.SelectedGroupe.Inscriptions.Count().ToString()));
			}
		}
		#endregion

		#region GenererVCardsGroupeUniqueCommand
		public ICommand GenererVCardsGroupeUniqueCommand {
			get;
			set;
		}

		private void CreateGenererVCardsGroupeUniqueCommand() {
			this.GenererVCardsGroupeUniqueCommand = new RelayCommand(
				this.ExecuteGenererVCardsGroupeUniqueCommand,
				this.CanExecuteGenererVCardsGroupeUniqueCommand
			);
		}

		public bool CanExecuteGenererVCardsGroupeUniqueCommand() {
			return this.SelectedGroupe != null;
		}

		public void ExecuteGenererVCardsGroupeUniqueCommand() {
			if (this.SelectedGroupe != null) {
				// recuperation du chemin d'enregistrement et passage au callback qui s'occupe de la génération a proprement parler
				Messenger.Default.Send<NMActionFolderDialog<string>>(
					new NMActionFolderDialog<string>(
						callback =>
						{
							this.GenererVCardsGroupeUniqueCallBack(callback);
						}
					)
				);
			}
		}

		private void GenererVCardsGroupeUniqueCallBack(string savePath) {
			if (!string.IsNullOrWhiteSpace(savePath)) {
				var sb = new StringBuilder();

				foreach (Inscription ins in this.SelectedGroupe.Inscriptions.Where(ins => ins.StatutInscription.Ordre != 3)) {
					var gen = new VcardGenerator21(ins.Adherent.Prenom, ins.Adherent.Nom);

					gen.AddEmailInternet(ins.Adherent.Mail1);
					gen.AddTelWork(ins.Adherent.Telephone1);
					gen.AddOrganization(ins.Groupe.ToString());

					sb.AppendLine(gen.GetVCard());
				}

				var fileName = string.Concat(savePath, "/", this.SelectedGroupe.Libelle, ResVCards.ExtensionVcf);

				using (var sw = new StreamWriter(fileName)) {
					sw.Write(sb.ToString());
				}

				this.ShowUserNotification(string.Format(ResGroupes.InfosVCardsUniqueGenerees, this.SelectedGroupe.Inscriptions.Count().ToString()));
			}
		}
		#endregion

		#region ReportCommand
		public override bool CanExecuteReportCommand(string codeReport) {
			return this.SelectedGroupe != null;
		}

		public override void ExecuteReportCommand(string codeReport) {
			switch (codeReport) {
				case CodesReport.ListeAdherents:
					Messenger.Default.Send(
						new NMActionFileDialog<string>(
							ResCommon.ExtensionExcel, 
							string.Format(ResGroupes.NomFichierRapportListeAdherents, this.SelectedGroupe.Libelle), 
							this.GenerateReportListeAdherentsGroupe
						)
					);
					break;

				default:
					break;
			}
		}

		private void GenerateReportListeAdherentsGroupe(string nomFichier) {
			if (nomFichier != null) {
				var gen = new ReportGenerator<ReportListeAdherents>(
						ServiceReportingAdapter.GroupeToReportListeAdherents(this.SelectedGroupe),
						nomFichier
					);

				gen.SetTitle(ResGroupes.TitreRapportListeAdherents);
				gen.SetSubTitle(string.Format(ResGroupes.SousTitreRapportListeAdherents, this.SelectedGroupe.Inscriptions.Count()));
				gen.GenerateExcelReport();

				this.ShowUserNotification(string.Format(ResCommon.InfoRapportGenere, nomFichier));
			}
		}
		#endregion

		#region ExtraireEmailsCommand
		public ICommand ExtraireEmailsCommand { get; set; }

		private void CreateExtraireEmailsCommand() {
			this.ExtraireEmailsCommand = new RelayCommand(
				this.ExecuteExtraireEmailsCommand,
				this.CanExecuteExtraireEmailsCommand
			);
		}

		public bool CanExecuteExtraireEmailsCommand() {
			return this.SelectedGroupe != null;
		}

		public void ExecuteExtraireEmailsCommand() {
			var listeMails = new List<string>();
			
			foreach (var ins in this.SelectedGroupe.Inscriptions.Where(i => i.StatutInscription.Ordre != 3)) {
				listeMails.Add(ins.Adherent.Mail1);
			}

			var chaineMails = string.Join(",", listeMails);

			this.ShowUserNotification(chaineMails);
		}
		#endregion
	}
}
