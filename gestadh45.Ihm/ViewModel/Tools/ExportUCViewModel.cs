using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;
using gestadh45.Ihm.ServiceAdaptateurs;
using gestadh45.Ihm.SpecialMessages;
using gestadh45.Ihm.ViewModel.Tools.Export;
using gestadh45.service.VCards;

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

		#region private methods
		private void InitialiseEncodages() {
			var encodages = new List<EncodageIhm>();

			encodages.Add(new EncodageIhm(CodesEncodage.UTF8, ResEncodages.UTF8, new System.Text.UTF8Encoding(true)));
			encodages.Add(new EncodageIhm(CodesEncodage.UTF8WithoutBOM, ResEncodages.UTF8WithoutBOM, new System.Text.UTF8Encoding(false)));
			encodages.Add(new EncodageIhm(CodesEncodage.Unicode, ResEncodages.Unicode, new System.Text.UnicodeEncoding()));

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
					DonneesVCard donnees = ServiceVCardAdaptateur.InscriptionToDonneesVCard(inscription);
					string saveFilePath = pSaveFolder + "\\" + inscription.Adherent.ToString() + ResVCards.Extension;

					var generateur = new VCardGenerateur(donnees, saveFilePath);
					generateur.CreerVCard();

					this.AfficherInformationIhm(ResMessages.MessageInfoGenerationVCardsGroupe);
				}
			}
		}

		private void GenererVCardFichierUnique(string pSaveFolder, IEnumerable<Inscription> pInscriptions, string pFileName) {
			if (!string.IsNullOrWhiteSpace(pSaveFolder)) {
				string saveFilePath = pSaveFolder + "\\" + pFileName + ResVCards.Extension;
				List<DonneesVCard> donnees = new List<DonneesVCard>();

				foreach (Inscription inscription in pInscriptions) {
					donnees.Add(ServiceVCardAdaptateur.InscriptionToDonneesVCard(inscription));
				}

				var generateur = new VCardGenerateur(donnees, saveFilePath);
				generateur.CreerVCard();

				this.AfficherInformationIhm(ResMessages.MessageInfoGenerationVCardsGroupe);
			}
		}
		#endregion
	}
}
