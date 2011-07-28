using System.Data.SQLite;
using System.Collections.Generic;

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

			this.Connection = new SQLiteConnection(scb.ConnectionString);
		}

		/// <summary>
		/// Retourne le dernier ID inséré dans la base
		/// </summary>
		/// <returns>Dernier ID inséré dans la base</returns>
		protected int GetLastInsertId() {
			bool connectionFlag = false;

			if (this.Connection.State != System.Data.ConnectionState.Open) {
				this.Connection.Open();
				connectionFlag = true;
			}

			var cmd = new SQLiteCommand("SELECT last_insert_rowid();", this.Connection);
			long result = (long)cmd.ExecuteScalar();

			if (connectionFlag) {
				this.Connection.Close();
			}

			return (int)result;
		}

		public override string ToString() {
			return string.Format("{0} - {1}", this.Connection.ConnectionString, this.Connection.Database);
		}

		/// <summary>
		/// Convertit une valeur booléenne en entier
		/// </summary>
		/// <param name="pVal">Booléen</param>
		/// <returns>1 si True, 0 si False</returns>
		protected int BoolToInt(bool pVal) {
			return pVal ? 1 : 0;
		}

		/// <summary>
		/// Convertit un int en bool
		/// </summary>
		/// <param name="pVal">Entier</param>
		/// <returns>True si 1, False sinon</returns>
		protected bool IntToBool(int pVal) {
			return pVal == 1;
		}
	}
}
