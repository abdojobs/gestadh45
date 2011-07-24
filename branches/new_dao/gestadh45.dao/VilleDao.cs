using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class VilleDao : DaoBase, IDao<Ville>
	{
		public VilleDao(string pFilePath) : base(pFilePath) { }
		
		public int Create(Ville pDonnee) {
			this.Connection.Open();

			var paramCodePostal = new SQLiteParameter("@CodePostal", System.Data.DbType.String) { Value = pDonnee.CodePostal.ToUpper() };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };

			var cmd = new SQLiteCommand("INSERT INTO Ville(CodePostal, Libelle) Values(@CodePostal, @Libelle);", this.Connection);
			cmd.Parameters.Add(paramCodePostal);
			cmd.Parameters.Add(paramLibelle);

			try {
				cmd.ExecuteNonQuery();
				return this.GetLastInsertId();
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public void Update(Ville pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramCodePostal = new SQLiteParameter("@CodePostal", System.Data.DbType.String) { Value = pDonnee.CodePostal.ToUpper() };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };

			var cmd = new SQLiteCommand("UPDATE Ville SET CodePostal=@CodePostal, Libelle=@Libelle WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(paramId);
			cmd.Parameters.Add(paramCodePostal);
			cmd.Parameters.Add(paramLibelle);

			try {
				cmd.ExecuteNonQuery();
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public void Delete(Ville pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };

			var cmd = new SQLiteCommand("DELETE FROM Ville WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(paramId);

			try {
				cmd.ExecuteNonQuery();
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public bool Exists(Ville pDonnee) {
			this.Connection.Open();

			// la vérification s'effectuera sur le couple code postal/libellé
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Ville WHERE UPPER(CodePostal)=@CodePostal AND UPPER(Libelle)=@Libelle;", this.Connection);
			var paramCodePostal = new SQLiteParameter("@CodePostal", System.Data.DbType.String) { Value = pDonnee.CodePostal.ToUpper() };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };
			cmd.Parameters.Add(paramCodePostal);
			cmd.Parameters.Add(paramLibelle);

			try {
				var result = (int)cmd.ExecuteScalar();
				return result > 0;
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public bool IsUsed(Ville pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Adresse WHERE ID_Ville=@Id;", this.Connection);
			cmd.Parameters.Add(paramId);

			try {
				var result = (int)cmd.ExecuteScalar();
				return result > 0;
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public Ville Read(int pId) {
			this.Connection.Open();
			Ville result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT Id, UPPER(CodePostal), UPPER(Libelle) FROM Ville WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Ville()
				{
					Id = reader.GetInt32(0),
					CodePostal = reader.GetString(1),
					Libelle = reader.GetString(2)
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Ville> List() {
			this.Connection.Open();

			List<Ville> result = new List<Ville>();

			var cmd = new SQLiteCommand("SELECT Id, UPPER(CodePostal), UPPER(Libelle) FROM Ville ORDER BY Libelle;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Ville()
					{
						Id = reader.GetInt32(0),
						CodePostal = reader.GetString(1),
						Libelle = reader.GetString(2)
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}
	}
}
