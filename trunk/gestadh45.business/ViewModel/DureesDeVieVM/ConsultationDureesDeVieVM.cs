using gestadh45.dal;

namespace gestadh45.business.ViewModel.DureesDeVieVM
{
	public class ConsultationDureesDeVieVM : GenericConsultationVM<DureeDeVie>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireDureeDeVie);
		}
		#endregion
	}
}
