using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.InfosClubVM
{
	public class FormulaireInfosClubVM : VMFormulaireBase
	{
		#region InfosClub
		private InfosClub _infosClub;

		/// <summary>
		/// Obtient/Définit l'objet contenant les informations du club
		/// </summary>
		public InfosClub InfosClub {
			get { return this._infosClub; }
			set {
				if (this._infosClub != value) {
					this._infosClub = value;
					this.RaisePropertyChanged(() => this.InfosClub);
				}
			}
		}
		#endregion

		#region Villes
		private ICollectionView _villes;

		/// <summary>
		/// Obitent/Définit la liste des villes
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
		private Repository<InfosClub> repoMain;
		private Repository<Ville> repoVille;
		#endregion

		public FormulaireInfosClubVM() {
			this.UCParentCode = CodesUC.ConsultationInfosClub;

			this.repoMain = new Repository<InfosClub>(this._context);
			this.repoVille = new Repository<Ville>(this._context);

			this.InfosClub = repoMain.GetFirst();
			this.PopulatesVilles();

			Messenger.Default.Register<NMSelectionElement<Ville>>(
				this, 
				(msg) => this.SelectVille(msg.Content)
			);
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie, il faut s'assurer de rafraîchir l'objet InfosClub avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			this.repoMain.Reload(this.InfosClub);

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			List<string> errors = new List<string>();
			
			if (this.CheckFormValidity(errors)) {
				this.repoMain.Edit(this.InfosClub);
				this.repoMain.Save();
				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion

		#region PopupVilleCommand
		public override void ExecutePopupVilleCommand() {
			base.ExecutePopupVilleCommand();

			// rafraichissement de la liste des villes
			this.PopulatesVilles();
		}
		#endregion

		private void PopulatesVilles() {
			ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.repoVille.GetAll());
			defaultView.SortDescriptions.Add(new SortDescription("Libelle", ListSortDirection.Ascending));
			this.Villes = defaultView;
		}

		private void SelectVille(Ville ville) {
			this.InfosClub.Ville = ville;
			this.RaisePropertyChanged(() => this.InfosClub);
		}

		protected override bool CheckFormValidity(List<string> errors) {
			// TODO sortir les chaines
			if (string.IsNullOrWhiteSpace(this.InfosClub.Nom)) {
				errors.Add("Le nom est obligatoire");
			}

			if (this.InfosClub.Adresse == null || string.IsNullOrWhiteSpace(this.InfosClub.Adresse)) {
				errors.Add("L'adresse est obligatoire");
			}

			if (this.InfosClub.Adresse != null && this.InfosClub.Ville == null) {
				errors.Add("La ville est obligatoire");
			}

			return errors.Count == 0;
		}
	}
}
