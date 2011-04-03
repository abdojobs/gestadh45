
namespace gestadh45.service.Graphs
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
