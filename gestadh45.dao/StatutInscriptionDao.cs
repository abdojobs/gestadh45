using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class StatutInscriptionDao : DaoBase, IStatutInscriptionDao
	{
		public StatutInscriptionDao(string pFilePath) : base(pFilePath) { }

		public StatutInscription Read(int pId) {
			this.Connection.Open();
			StatutInscription result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT Id, Libelle, CodeCouleur, Ordre FROM StatutInscription WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new StatutInscription()
				{
					Id = reader.GetInt32(0),
					Libelle = reader.GetString(1),
					CodeCouleur = reader.GetString(2),
					Ordre = reader.GetInt32(3)
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<StatutInscription> List() {
			this.Connection.Open();
			List<StatutInscription> result = new List<StatutInscription>();

			var cmd = new SQLiteCommand("SELECT Id, Libelle, CodeCouleur, Ordre FROM StatutInscription ORDER BY Ordre;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new StatutInscription()
					{
						Id = reader.GetInt32(0),
						Libelle = reader.GetString(1),
						CodeCouleur = reader.GetString(2),
						Ordre = reader.GetInt32(3)
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}


		public bool Exists(StatutInscription pDonnee) {
			this.Connection.Open();

			// La vérification s'effectuera sur l'ID
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM StatutInscription WHERE ID=@Id;", this.Connection);
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
