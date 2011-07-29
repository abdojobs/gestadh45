using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class SexeDao : DaoBase, ISexeDao 
	{
		public SexeDao(string pFilePath) : base(pFilePath) { }

		public Sexe Read(int pId) {
			this.Connection.Open();
			Sexe result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value=pId };
			var cmd = new SQLiteCommand("SELECT Id, UPPER(LibelleCourt), LibelleLong FROM Sexe WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Sexe()
				{
					Id = reader.GetInt32(0),
					LibelleCourt = reader.GetString(1),
					LibelleLong = reader.GetString(2)
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Sexe> List() {
			this.Connection.Open();

			List<Sexe> result = new List<Sexe>();

			var cmd = new SQLiteCommand("SELECT Id, UPPER(LibelleCourt), LibelleLong FROM Sexe ORDER BY LibelleCourt DESC;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Sexe()
					{
						Id = reader.GetInt32(0),
						LibelleCourt = reader.GetString(1),
						LibelleLong = reader.GetString(2)
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}

		public bool Exists(Sexe pDonnee) {
			this.Connection.Open();

			// La vérification s'effectuera sur l'ID
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Sexe WHERE ID=@Id;", this.Connection);
			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			cmd.Parameters.Add(paramId);

			try {
				var result = (long)cmd.ExecuteScalar();
				return result > 0;
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}
	}
}
