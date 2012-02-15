
using gestadh45.dal;

namespace gestadh45.manager
{
	public class ContextManager
	{
		/// <summary>
		/// Obtient le contexte de l'application
		/// </summary>
		public GestAdh45Entities Context { get; internal set; }

		/// <summary>
		/// Initialise une nouvelle instance du contexte
		/// </summary>
		public ContextManager() {
			this.Context = new GestAdh45Entities();
		}

		/// <summary>
		/// Ferme et détruit l'instance du contexte
		/// </summary>
		public void CloseContext() {
			this.Context.Dispose();
			this.Context = null;
		}
	}
}
