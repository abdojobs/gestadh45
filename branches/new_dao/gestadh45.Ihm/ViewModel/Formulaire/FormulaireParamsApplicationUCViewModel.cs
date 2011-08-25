using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Formulaire
{
	public class FormulaireParamsApplicationUCViewModel : ViewModelBaseFormulaire
	{
		private ParamsApplication _paramsApplication;
		private IParamsApplicationDao _daoParamsApplication;

		/// <summary>
		/// Gets or sets the params application.
		/// </summary>
		/// <value>
		/// The params application.
		/// </value>
		public ParamsApplication ParamsApplication {
			get {
				return this._paramsApplication;
			}
			set {
				if (this._paramsApplication != value) {
					this._paramsApplication = value;
					this.RaisePropertyChanged(() => this.ParamsApplication);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormulaireParamsApplicationUCViewModel"/> class.
		/// </summary>
		public FormulaireParamsApplicationUCViewModel() {
			this._daoParamsApplication = DaoFactory.GetParamsApplicationDao(ViewModelLocator.DataSource);
			this.CodeUCOrigine = CodesUC.ConsultationParamsApplication;

			this.ParamsApplication = this._daoParamsApplication.Read();
		}

		/// <summary>
		/// Executes the EnregistrerCommand
		/// </summary>
		public override void ExecuteEnregistrerCommand() {
			if (this.VerifierSaisie()) {
				this._daoParamsApplication.Update(this.ParamsApplication);
				base.ExecuteEnregistrerCommand();
			}
			else {
				this.AfficherErreursIhm(this.Erreurs);
			}
		}
	}
}
