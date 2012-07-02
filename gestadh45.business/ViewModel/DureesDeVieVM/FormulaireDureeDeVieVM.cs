using System;
using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.business.ViewModel.DureesDeVieVM
{
	public class FormulaireDureeDeVieVM : VMFormulaireBase
	{
		#region CurrentDureeDeVie
		private DureeDeVie _currentDureeDeVie;

		/// <summary>
		/// Obtient/Définit la duree de vie courante
		/// </summary>
		public DureeDeVie CurrentDureeDeVie {
			get { return this._currentDureeDeVie; }
			set {
				if (this._currentDureeDeVie != value) {
					this._currentDureeDeVie = value;
					this.RaisePropertyChanged(() => this.CurrentDureeDeVie);
				}
			}
		}
		#endregion

		#region Repository
		private Repository<DureeDeVie> _repoDureeDeVie;
		#endregion

		#region Constructeurs
		public FormulaireDureeDeVieVM() {
			this._repoDureeDeVie = new Repository<DureeDeVie>(this._context);
			this.UCParentCode = CodesUC.ConsultationDureesDeVie;
			this.IsEditMode = false;
			this.CurrentDureeDeVie = new DureeDeVie();
		}

		/// <summary>
		/// Constructeur pour le mode édition
		/// </summary>
		/// <param name="idDureeDeVie">ID de l'objet à éditer</param>
		public FormulaireDureeDeVieVM(Guid idDureeDeVie) {
			this._repoDureeDeVie = new Repository<DureeDeVie>(this._context);
			this.UCParentCode = CodesUC.ConsultationDureesDeVie;
			this.IsEditMode = true;
			this.CurrentDureeDeVie = this._repoDureeDeVie.GetByKey(idDureeDeVie);
		}
		#endregion

		protected override bool CheckFormValidity(List<string> errors) {
			if (string.IsNullOrWhiteSpace(this.CurrentDureeDeVie.Libelle)) {
				errors.Add(ResDureesDeVie.ErreurLibelleObligatoire);
			}

			return errors.Count == 0;
		}

		#region CancelCommand
		/// <summary>
		/// Si on annule la saisie en mode édition, il faut s'assurer de rafraîchir l'objet courant avec ses valeurs d'origine (Reload)
		/// </summary>
		public override void ExecuteCancelCommand() {
			if (this.IsEditMode) {
				this._repoDureeDeVie.Reload(this.CurrentDureeDeVie);
			}

			base.ExecuteCancelCommand();
		}
		#endregion

		#region SaveCommand
		public override void ExecuteSaveCommand() {
			var errors = new List<string>();

			if (this.CheckFormValidity(errors)) {

				if (this.IsEditMode) {
					this._repoDureeDeVie.Edit(this.CurrentDureeDeVie);
				}
				else {
					this.CurrentDureeDeVie.ID = Guid.NewGuid();
					this._repoDureeDeVie.Add(this.CurrentDureeDeVie);
				}

				this._repoDureeDeVie.Save();

				base.ExecuteSaveCommand();
			}
			else {
				this.ShowUserNotifications(errors);
			}
		}
		#endregion
	}
}
