using gestadh45.dal;

namespace gestadh45.business.ViewModel.TranchesAgeVM
{
	public class ConsultationTranchesAgeVM : GenericConsultationVM<TrancheAge>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireTrancheAge);
		}
		#endregion
	}
}
