using gestadh45.dal;

namespace gestadh45.business.ViewModel.CategoriesVM
{
	public class ConsultationCategoriesVM : GenericConsultationVM<Categorie>
	{
		#region CreateCommand
		public override void ExecuteCreateCommand() {
			this.ShowUC(CodesUC.FormulaireCategorie);
		}
		#endregion
	}
}
