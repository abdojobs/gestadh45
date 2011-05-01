using gestadh45.Model;

namespace gestadh45.dao
{
	public static class ContextManager
	{
		private static Entities context;

		/// <summary>
		/// Créé le contexte de l'application
		/// </summary>
		/// <param name="connectionString">Chaîne de connexion à Entity</param>
		public static void CreateContext(string connectionString) {
			context = new Entities(connectionString);
		}

		/// <summary>
		/// Obtient le contexte de l'application
		/// </summary>
		public static Entities Context { get { return context; } }
	}
}
