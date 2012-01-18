using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Tools.Export;
using GPToolkit.CSV;
using GPToolkit.Vcard;

namespace gestadh45.Ihm.ViewModel.Tools
{
	public class ExportUCViewModel : ViewModelBaseConsultation
	{
		#region private fields
		private ICollectionView _encodages;
		private EncodageIhm _encodage;

		private ICollectionView _groupes;
		private Groupe _groupe;
		#endregion

		#region properties
		/// <summary>
		/// Obtient la liste des encodages disponibles
		/// </summary>
		public ICollectionView Encodages {
			get {
				return this._encodages;
			}
		}

		/// <summary>
		/// Obtient/Définit l'encodage sélectionné
		/// </summary>
		public EncodageIhm Encodage {
			get { return this._encodage; }
			set {
				if (this._encodage != value) {
					this._encodage = value;
					this.RaisePropertyChanged(() => this.Encodage);
				}
			}
		}

		/// <summary>
		/// Obtient la liste des groupes disponibles
		/// </summary>
		public ICollectionView Groupes {
			get {
				return this._groupes;
			}
		}

		/// <summary>
		/// Obtient/Définit le groupe sélectionné
		/// </summary>
		public Groupe Groupe {
			get { return this._groupe; }
			set {
				if (this._groupe != value) {
					this._groupe = value;
					this.RaisePropertyChanged(() => this.Groupe);
				}
			}
		}
		#endregion

		#region constructors
		/// <summary>
		/// Constructeur
		/// </summary>
		public ExportUCViewModel() {
			this.CreateExtractionVCardGroupCommand();
			this.CreateExtractionVCardSaisonCommand();

			this.CreateExtractionCSVGroupCommand();
			this.CreateExtractionCSVSaisonCommand();

			this.InitialiseEncodages();
			this.InitialisationGroupes();
		}
		#endregion

		#region ExtractionVCardSaisonCommand
		public ICommand ExtractionVCardSaisonCommand { get; set; }

		private void CreateExtractionVCardSaisonCommand() {
			this.ExtractionVCardSaisonCommand = new RelayCommand<bool?>(
				this.ExecuteExtractionVCardSaisonCommand,
				this.CanExecuteExtractionVCardSaisonCommand
			);

		}

		public bool CanExecuteExtractionVCardSaisonCommand(bool? pFichierUnique) {
			return this.Encodage != null;
		}

		public void ExecuteExtractionVCardSaisonCommand(bool? pFichierUnique) {
			if (this.Encodage != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							bool fichierUnique = pFichierUnique ?? false;

							if (fichierUnique) {
								this.GenererVCardFichierUnique(callbackmessage, ViewModelLocator.DaoInscription.ListSaisonCourante(), ViewModelLocator.DaoSaison.ReadSaisonCourante().ToShortString());
							}
							else {
								this.GenererVCard(callbackmessage, ViewModelLocator.DaoInscription.ListSaisonCourante());
							}
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region ExtractionVCardGroupCommand
		public ICommand ExtractionVCardGroupCommand { get; set; }

		private void CreateExtractionVCardGroupCommand() {
			this.ExtractionVCardGroupCommand = new RelayCommand<bool?>(
				this.ExecuteExtractionVCardGroupCommand,
				this.CanExecuteExtractionVCardGroupCommand
			);
		}

		public bool CanExecuteExtractionVCardGroupCommand(bool? pFichierUnique) {
			return this.Encodage != null && this.Groupe != null;
		}

		public void ExecuteExtractionVCardGroupCommand(bool? pFichierUnique) {
			if (this.Encodage != null &&  this.Groupe != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							bool fichierUnique = pFichierUnique ?? false;

							if (fichierUnique) {
								this.GenererVCardFichierUnique(callbackmessage, this.Groupe.Inscriptions, this.Groupe.Libelle);
							}
							else {
								this.GenererVCard(callbackmessage, this.Groupe.Inscriptions);
							}
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region ExtractionCSVSaisonCommand
		public ICommand ExtractionCSVSaisonCommand { get; set; }

		private void CreateExtractionCSVSaisonCommand() {
			this.ExtractionCSVSaisonCommand = new RelayCommand(
				this.ExecuteExtractionCSVSaisonCommand,
				this.CanExecuteExtractionCSVSaisonCommand
			);

		}

		public bool CanExecuteExtractionCSVSaisonCommand() {
			return this.Encodage != null;
		}

		public void ExecuteExtractionCSVSaisonCommand() {
			if (this.Encodage != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							this.GenererCSV(callbackmessage, ViewModelLocator.DaoInscription.ListSaisonCourante(), ViewModelLocator.DaoSaison.ReadSaisonCourante().ToShortString());
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region ExtractionCSVGroupCommand
		public ICommand ExtractionCSVGroupCommand { get; set; }

		private void CreateExtractionCSVGroupCommand() {
			this.ExtractionCSVGroupCommand = new RelayCommand(
				this.ExecuteExtractionCSVGroupCommand,
				this.CanExecuteExtractionCSVGroupCommand
			);
		}

		public bool CanExecuteExtractionCSVGroupCommand() {
			return this.Encodage != null && this.Groupe != null;
		}

		public void ExecuteExtractionCSVGroupCommand() {
			if (this.Encodage != null && this.Groupe != null) {
				NotificationMessageActionFolderDialog<string> message =
					new NotificationMessageActionFolderDialog<string>(
						callbackmessage =>
						{
							this.GenererCSV(callbackmessage, this.Groupe.Inscriptions, this.Groupe.Libelle);
						}
					);

				Messenger.Default.Send<NotificationMessageActionFolderDialog<string>>(message);
			}
		}
		#endregion

		#region private methods
		private void InitialiseEncodages() {
			var encodages = new List<EncodageIhm>();

			encodages.Add(new EncodageIhm(CodesEncodage.UTF8, ResExport.UTF8, new System.Text.UTF8Encoding(true)));
			encodages.Add(new EncodageIhm(CodesEncodage.UTF8WithoutBOM, ResExport.UTF8WithoutBOM, new System.Text.UTF8Encoding(false)));
			encodages.Add(new EncodageIhm(CodesEncodage.Unicode, ResExport.Unicode, new System.Text.UnicodeEncoding()));

			this._encodages = CollectionViewSource.GetDefaultView(encodages);
		}

		private void InitialisationGroupes() {
			var groupes = ViewModelLocator.DaoGroupe.ListSaisonCourante();
			var defaultView = CollectionViewSource.GetDefaultView(groupes);
			defaultView.SortDescriptions.Add(new SortDescription("JourSemaine.Numero", ListSortDirection.Ascending));
			defaultView.SortDescriptions.Add(new SortDescription("HeureDebut", ListSortDirection.Ascending));

			this._groupes = defaultView;
		}

		private void GenererVCard(string pSaveFolder, IEnumerable<Inscription> pInscriptions) {
			if(!string.IsNullOrWhiteSpace(pSaveFolder)) {
				foreach (Inscription inscription in pInscriptions) {
					string saveFilePath = pSaveFolder + "\\" + inscription.Adherent.ToString() + ResExport.VcardExtension;

					var generateur = new VcardGenerator21(
						inscription.Adherent.Prenom,
						inscription.Adherent.Nom
					);

					generateur.AddEmailInternet(inscription.Adherent.Mail1);
					generateur.AddTelWork(inscription.Adherent.Telephone1);
					generateur.AddOrganization(inscription.Groupe.ToString());	

					using (StreamWriter writer = new StreamWriter(saveFilePath, false, this.Encodage.Encodage)) {
						writer.Write(generateur.GetVCard());
					}
					
					this.AfficherInformationIhm(ResExport.MsgInfoGenerationVcards);
				}
			}
		}

		private void GenererVCardFichierUnique(string pSaveFolder, IEnumerable<Inscription> pInscriptions, string pFileName) {
			if (!string.IsNullOrWhiteSpace(pSaveFolder)) {
				string saveFilePath = pSaveFolder + "\\" + pFileName + ResExport.VcardExtension;

				StringBuilder sb = new StringBuilder();

				foreach (Inscription inscription in pInscriptions) {
					var generateur = new VcardGenerator21(
						inscription.Adherent.Prenom, 
						inscription.Adherent.Nom
					);

					generateur.AddEmailInternet(inscription.Adherent.Mail1);
					generateur.AddTelWork(inscription.Adherent.Telephone1);
					generateur.AddOrganization(inscription.Groupe.ToString());

					sb.Append(generateur.GetVCard());
				}

				using (StreamWriter writer = new StreamWriter(saveFilePath, false, this.Encodage.Encodage)) {
					writer.Write(sb.ToString());
				}

				this.AfficherInformationIhm(ResExport.MsgInfoGenerationVcards);
			}
		}

		private void GenererCSV(string pSaveFolder, IEnumerable<Inscription> pInscriptions, string pFileName) {
			if (!string.IsNullOrWhiteSpace(pSaveFolder)) {
				string saveFilePath = pSaveFolder + "\\" + pFileName + ResExport.CsvExtension;
				List<CSVRow> donnees = new List<CSVRow>();

				foreach (Inscription inscription in pInscriptions) {
					var row = new CSVRow();
					row.AddValue(inscription.Groupe.ToString());
					row.AddValue(inscription.Adherent.Nom);
					row.AddValue(inscription.Adherent.Prenom);
					row.AddValue(inscription.Adherent.DateNaissance.ToShortDateString());
					row.AddValue(inscription.Adherent.Adresse.ToString());

					donnees.Add(row);
				}

				var generateur = new CSVGenerator(donnees, ResExport.CsvSeparator);
				using (StreamWriter writer = new StreamWriter(saveFilePath, false, this.Encodage.Encodage)) {
					writer.Write(generateur.GetCSV());
				}

				this.AfficherInformationIhm(ResExport.MsgInfoGenerationCsv);
			}
		}
		#endregion
	}
}
