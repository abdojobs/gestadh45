using System.Reflection;
using System.Resources;

namespace gestadh45.business.IhmObjects
{
	/// <summary>
	/// Classe représentant un couple code/libellé pour le chois des graphs
	/// </summary>
	public class ChoixGraphIhm
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

		/// <summary>
		/// Initializes a new instance of the <see cref="ChoixGraphIhm"/> class.
		/// </summary>
		/// <param name="code">The code.</param>
		public ChoixGraphIhm(string code) {
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
			ResourceManager resMan = new ResourceManager("gestadh45.business.ViewModel.Statistiques.RessourcesStats", assembly);
			return resMan.GetString(resourceCode);
		}
	}
}
