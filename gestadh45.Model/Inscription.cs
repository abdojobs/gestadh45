
namespace gestadh45.Model
{
	public partial class Inscription
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant si le certificat medical a été remis
		/// </summary>
		public bool CertificatMedicalRemisBool {
			get { return this.CertificatMedicalRemis == 1; }
			set { this.CertificatMedicalRemis = (value) ? 1 : 0; }
		}
		
		public override string ToString() {
			return string.Format("{0} - {1}", this.Adherent, this.Groupe.Libelle);
		}
	}
}
