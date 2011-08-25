using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.Ihm.ViewModel.Consultation
{
	public class ConsultationParamsApplicationViewModel : ViewModelBaseConsultation
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
			get { return this._paramsApplication; }
			set {
				if (this._paramsApplication != value) {
					this._paramsApplication = value;
					this.RaisePropertyChanged(()=>this.ParamsApplication);
				}
			}
		}

		public ConsultationParamsApplicationViewModel() {
			this._daoParamsApplication = DaoFactory.GetParamsApplicationDao(ViewModelLocator.DataSource);
			this.ParamsApplication = this._daoParamsApplication.Read();
			this.CreateEditerCommand();
		}

		public override bool CanExecuteEditerCommand() {
			return true;
		}

		public override void ExecuteEditerCommand() {
			base.ExecuteEditerCommand();

			this.AfficherEcran(CodesUC.FormulaireParamsApplication);
		}
	}
}
