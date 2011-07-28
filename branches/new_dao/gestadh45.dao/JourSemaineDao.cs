using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class JourSemaineDao : DaoBase, IReadOnlyDao<JourSemaine>
	{
		public JourSemaineDao(string pFilePath) : base(pFilePath) { }
		
		public JourSemaine Read(int pId) {
			this.Connection.Open();
			JourSemaine result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT Id, UPPER(Libelle), Numero FROM JourSemaine WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new JourSemaine()
				{
					Id = reader.GetInt32(0),
					Libelle = reader.GetString(1),
					Numero = reader.GetInt32(2)
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<JourSemaine> List() {
			this.Connection.Open();
			List<JourSemaine> result = new List<JourSemaine>();

			var cmd = new SQLiteCommand("SELECT Id, UPPER(Libelle), Numero FROM JourSemaine ORDER BY Numero;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new JourSemaine()
					{
						Id = reader.GetInt32(0),
						Libelle = reader.GetString(1),
						Numero = reader.GetInt32(2)
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}

		public bool Exists(JourSemaine pDonnee) {
			this.Connection.Open();

			// La vérification s'effectuera sur l'ID
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM JourSemaine WHERE ID=@Id;", this.Connection);
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
