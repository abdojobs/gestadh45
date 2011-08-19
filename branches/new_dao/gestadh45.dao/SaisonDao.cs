using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using gestadh45.model;

namespace gestadh45.dao
{
	public class SaisonDao : DaoBase, ISaisonDao
	{
		public SaisonDao(string pFilePath) : base(pFilePath) { }

		public int Create(Saison pDonnee) {
			this.Connection.Open();

			var paramEstSaisonCourante = new SQLiteParameter("@EstSaisonCourante", System.Data.DbType.Boolean) { Value = pDonnee.EstSaisonCourante };
			var paramAnneeDebut = new SQLiteParameter("@AnneeDebut", System.Data.DbType.Int32) { Value = pDonnee.AnneeDebut };
			var paramAnneeFin = new SQLiteParameter("@AnneeFin", System.Data.DbType.Int32) { Value = pDonnee.AnneeFin };

			var cmd = new SQLiteCommand("INSERT INTO Saison(EstSaisonCourante, AnneeDebut, AnneeFin) Values(@EstSaisonCourante, @AnneeDebut, @AnneeFin);", this.Connection);
			cmd.Parameters.Add(paramEstSaisonCourante);
			cmd.Parameters.Add(paramAnneeDebut);
			cmd.Parameters.Add(paramAnneeFin);

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

		public void Update(Saison pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramEstSaisonCourante = new SQLiteParameter("@EstSaisonCourante", System.Data.DbType.Boolean) { Value = pDonnee.EstSaisonCourante };
			var paramAnneeDebut = new SQLiteParameter("@AnneeDebut", System.Data.DbType.Int32) { Value = pDonnee.AnneeDebut };
			var paramAnneeFin = new SQLiteParameter("@AnneeFin", System.Data.DbType.Int32) { Value = pDonnee.AnneeFin };

			var cmd = new SQLiteCommand("UPDATE Saison SET EstSaisonCourante=@EstSaisonCourante, AnneeDebut=@AnneeDebut, AnneeFin=@AnneeFin WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(paramId);
			cmd.Parameters.Add(paramEstSaisonCourante);
			cmd.Parameters.Add(paramAnneeDebut);
			cmd.Parameters.Add(paramAnneeFin);

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

		public void Delete(Saison pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };

			var cmd = new SQLiteCommand("DELETE FROM Saison WHERE ID=@Id;", this.Connection);
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

		public bool Exists(Saison pDonnee) {
			this.Connection.Open();

			// une saison existe si son année de début est comprise entre l'année de début et l'année de fin d'une saison existante
			// ou si son année de fin est comprise entre l'année de début et l'année de fin d'une saison existante
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Saison WHERE (AnneeDebut<=@AnneeDebut AND @AnneeDebut<AnneeFin) OR (AnneeDebut<@AnneeFin AND @AnneeFin<=AnneeFin);", this.Connection);
			var paramAnneeDebut = new SQLiteParameter("@AnneeDebut", System.Data.DbType.Int32) { Value = pDonnee.AnneeDebut };
			var paramAnneeFin = new SQLiteParameter("@AnneeFin", System.Data.DbType.Int32) { Value = pDonnee.AnneeFin };
			cmd.Parameters.Add(paramAnneeDebut);
			cmd.Parameters.Add(paramAnneeFin);

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

		public bool IsUsed(Saison pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Groupe WHERE ID_Saison=@Id;", this.Connection);
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

		public Saison Read(int pId) {
			this.Connection.Open();
			Saison result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT Id, EstSaisonCourante, AnneeDebut, AnneeFin FROM Saison WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Saison()
				{
					Id = reader.GetInt32(0),
					EstSaisonCourante = reader.GetBoolean(1),
					AnneeDebut = reader.GetInt32(2),
					AnneeFin = reader.GetInt32(3)
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Saison> List() {
			this.Connection.Open();

			List<Saison> result = new List<Saison>();

			var cmd = new SQLiteCommand("SELECT Id, EstSaisonCourante, AnneeDebut, AnneeFin FROM Saison ORDER BY AnneeDebut;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Saison()
					{
						Id = reader.GetInt32(0),
						EstSaisonCourante = reader.GetBoolean(1),
						AnneeDebut = reader.GetInt32(2),
						AnneeFin = reader.GetInt32(3)
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}

		/// <summary>
		/// Récupère la saison courante
		/// </summary>
		/// <returns>Saison courante</returns>
		public Saison ReadSaisonCourante() {
			var rq = from Saison s in this.List()
					 where s.EstSaisonCourante
					 select s;

			return rq.First();
		}
	}
}
