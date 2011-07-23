using System.Data.SQLite;

namespace gestadh45.dao
{
	public abstract class DaoBase
	{
		/// <summary>
		/// Obtient/Définit la connexion à la base
		/// </summary>
		public SQLiteConnection Connection { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pFilePath">Chemin du fichier base</param>
		public DaoBase(string pFilePath) {
			var scb = new SQLiteConnectionStringBuilder();
			scb.DataSource = pFilePath;

			this.Connection = new SQLiteConnection(scb.ToString());
		}
	}
}
