using gestadh45.dal;

namespace gestadh45.business.ViewModel.MarquesVM
{
	public class ConsultationMarquesVM : GenericConsultationVM<Marque>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireMarque);
		}
		#endregion
	}
}
