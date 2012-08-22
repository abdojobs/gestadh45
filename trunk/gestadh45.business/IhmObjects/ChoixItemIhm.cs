using System.Reflection;
using System.Resources;

namespace gestadh45.business.IhmObjects
{
	/// <summary>
	/// Classe représentant un couple code/libellé pour un choix
	/// </summary>
	public class ChoixItemIhm
	{
		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		public string Code {
			get;
			internal set;
		}

		private string _resourceBaseName;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChoixItemIhm"/> class.
		/// </summary>
		/// <param name="resourceBaseName">ResourceBaseName</param>
		/// <param name="code">The code.</param>
		public ChoixItemIhm(string resourceBaseName, string code) {
			this._resourceBaseName = resourceBaseName;
			this.Code = code;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return this.ReadResource(this.Code);
		}

		/// <summary>
		/// Reads the resource.
		/// </summary>
		/// <param name="resourceCode">The resource code.</param>
		/// <returns>The resource</returns>
		private string ReadResource(string resourceCode) {
			Assembly assembly = this.GetType().Assembly;
			ResourceManager resMan = new ResourceManager(this._resourceBaseName, assembly);
			return resMan.GetString(resourceCode);
		}
	}
}
