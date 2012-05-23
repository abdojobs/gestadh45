
using System.Collections.Generic;
using System.Text;
namespace gestadh45.services.VCards
{
	public abstract class VcardGeneratorBase
	{
		#region generic fields templates
		private string Header {
			get { return "BEGIN:VCARD"; }
		}

		private string Footer {
			get { return "END:VCARD"; }
		}


		/// <summary>
		/// Gets the first name template. (eg. : FN:[FirstName] [LastName])
		/// </summary>
		private string FirstNameTemplate {
			get { return "FN:{0} {1}"; }
		}

		/// <summary>
		/// Gets the name template. (eg. : N:[LastName];[FirstName])
		/// </summary>
		private string NameTemplate {
			get { return "N:{0};{1}"; }
		}
		#endregion

		#region abstract fields templates
		protected abstract string Version { get; }
		protected abstract string TelWorkTemplate { get; }
		protected abstract string EmailInternetTemplate { get; }
		protected abstract string OrganizationTemplate { get; }
		#endregion

		#region methods
		public void AddTelWork(string telWork) {
			this.vcardFields.Add(string.Format(this.TelWorkTemplate, telWork));
		}

		public void AddEmailInternet(string emailInternet) {
			this.vcardFields.Add(string.Format(this.EmailInternetTemplate, emailInternet));
		}

		public void AddOrganization(string organization) {
			this.vcardFields.Add(string.Format(this.OrganizationTemplate, organization));
		}
		#endregion

		#region mandatory fields
		private string _firstName;
		private string _lastName;
		#endregion

		#region fields
		protected List<string> vcardFields;
		#endregion

		/// <summary>
		/// Initialize a new instance of VcardGeneratorBase
		/// </summary>
		/// <param name="firstName">The First Name</param>
		/// <param name="lastName">The Last Name</param>
		protected VcardGeneratorBase(string firstName, string lastName) {
			this._firstName = firstName;
			this._lastName = lastName;

			this.vcardFields = new List<string>();
		}

		public string GetVCard() {
			var sb = new StringBuilder();

			sb.AppendLine(this.Header);
			sb.AppendLine(this.Version);

			sb.AppendLine(string.Format(this.FirstNameTemplate, this._firstName, this._lastName));
			sb.AppendLine(string.Format(this.NameTemplate, this._lastName, this._firstName));

			foreach (string field in this.vcardFields) {
				sb.AppendLine(field);
			}

			sb.AppendLine(this.Footer);

			return sb.ToString();
		}
	}
}
