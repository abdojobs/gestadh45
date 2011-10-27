using System.Text;

namespace gestadh45.Ihm.ViewModel.Tools.Export
{
	public class EncodageIhm
	{
		public string Code { get; internal set; }
		public string Libelle { get; internal set; }
		public Encoding Encodage { get; internal set; }

		public EncodageIhm(string pCode, string pLibelle, Encoding pEncodage) {
			this.Code = pCode;
			this.Libelle = pLibelle;
			this.Encodage = pEncodage;
		}

		public override string ToString() {
			return this.Libelle;
		}
	}
}
