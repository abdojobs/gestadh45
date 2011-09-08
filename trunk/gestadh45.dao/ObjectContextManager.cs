using gestadh45.dal;

namespace gestadh45.dao
{
	public static class ObjectContextManager
	{
		private static Entities context;

		/// <summary>
		/// Créé la connexion au contexte
		/// </summary>
		/// <param name="connectionString">Chaîne de connexion à Entity</param>
		public static void CreateContext(string connectionString) {
			context = new Entities(connectionString);
		}

		/// <summary>
		/// Détruit la connexion au contexte
		/// </summary>
		public static void DestroyContext() {
			context = null;
		}

		/// <summary>
		/// Obtient le contexte de l'application
		/// </summary>
		public static Entities Context { get { return context; } }
	}
}
