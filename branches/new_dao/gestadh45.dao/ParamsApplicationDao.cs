using System.Data;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class ParamsApplicationDao : DaoBase, IParamsApplicationDao
	{		
		public ParamsApplicationDao(string pFilePath) : base(pFilePath) { }

		#region IParamsApplicationDao Membres

		public void Update(ParamsApplication pDonnee) {
			this.Connection.Open();

			var trans = this.Connection.BeginTransaction();
			var cmd = new SQLiteCommand("UPDATE ParamsApplication SET ServeurSmtp=@ServeurSmtp", this.Connection, trans);

			var paramServeurSmtp = new SQLiteParameter("@ServeurSmtp", DbType.String) { Value = pDonnee.ServeurSmtp };
			cmd.Parameters.Add(paramServeurSmtp);

			try {
				cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SQLiteException) {
				// si un problème survient, on annule la transaction et on remonte l'exception
				trans.Rollback();
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public ParamsApplication Read() {
			this.Connection.Open();
			ParamsApplication result = null;

			var cmd = new SQLiteCommand("SELECT * FROM ParamsApplication;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new ParamsApplication()
				{
					ServeurSmtp = reader.GetString(0)
				};
			}

			this.Connection.Close();
			return result;
		}

		#endregion
	}
}
