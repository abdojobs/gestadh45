using gestadh45.dal;

namespace gestadh45.business.ViewModel.VillesVM
{
	public class ConsultationVillesVM : GenericConsultationVM<Ville>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireVille);
		}
		#endregion
	}
}
