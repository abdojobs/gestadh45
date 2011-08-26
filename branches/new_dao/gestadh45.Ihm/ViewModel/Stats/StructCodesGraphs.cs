
namespace gestadh45.Ihm.ViewModel.Consultation.Stats 
{
	public struct StructCodesGraphs
	{
		public string Code { get; set; }
		public string Libelle { get; set; }

		public override string ToString() {
			return this.Libelle;
		}
	}
}
