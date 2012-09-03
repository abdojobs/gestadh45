using gestadh45.dal;

namespace gestadh45.business.ViewModel.LocalisationVM
{
	public class ConsultationLocalisationsVM : GenericConsultationVM<Localisation>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireLocalisation);
		}
		#endregion
	}
}
