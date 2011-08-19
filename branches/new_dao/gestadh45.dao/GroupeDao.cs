using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using gestadh45.model;

namespace gestadh45.dao
{
	public class GroupeDao : DaoBase, IGroupeDao
	{
		public GroupeDao(string pFilePath) : base(pFilePath) { }

		public int Create(Groupe pDonnee) {
			this.Connection.Open();

			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };
			var paramIdJourSemaine = new SQLiteParameter("@IdJourSemaine", System.Data.DbType.Int32) { Value = pDonnee.JourSemaine.Id };
			var paramNbPlaces = new SQLiteParameter("@NbPlaces", System.Data.DbType.Int32) { Value = pDonnee.NbPlaces };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdSaison = new SQLiteParameter("@IdSaison", System.Data.DbType.Int32) { Value = pDonnee.Saison.Id };
			var paramHeureDebut = new SQLiteParameter("@HeureDebut", System.Data.DbType.DateTime) { Value = pDonnee.HeureDebut };
			var paramHeureFin = new SQLiteParameter("@HeureFin", System.Data.DbType.DateTime) { Value = pDonnee.HeureFin };

			var cmd = new SQLiteCommand("INSERT INTO Groupe(Libelle, ID_JourSemaine, NbPlaces, Commentaire, ID_Saison, HeureDebutDT, HeureFinDT) Values(@Libelle, @IdJourSemaine, @NbPlaces, @Commentaire, @IdSaison, @HeureDebut, @HeureFin);", this.Connection);
			cmd.Parameters.Add(paramLibelle);
			cmd.Parameters.Add(paramIdJourSemaine);
			cmd.Parameters.Add(paramNbPlaces);
			cmd.Parameters.Add(paramCommentaire);
			cmd.Parameters.Add(paramIdSaison);
			cmd.Parameters.Add(paramHeureDebut);
			cmd.Parameters.Add(paramHeureFin);

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

		public void Update(Groupe pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };
			var paramIdJourSemaine = new SQLiteParameter("@IdJourSemaine", System.Data.DbType.Int32) { Value = pDonnee.JourSemaine.Id };
			var paramNbPlaces = new SQLiteParameter("@NbPlaces", System.Data.DbType.Int32) { Value = pDonnee.NbPlaces };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdSaison = new SQLiteParameter("@IdSaison", System.Data.DbType.Int32) { Value = pDonnee.Saison.Id };
			var paramHeureDebut = new SQLiteParameter("@HeureDebut", System.Data.DbType.DateTime) { Value = pDonnee.HeureDebut };
			var paramHeureFin = new SQLiteParameter("@HeureFin", System.Data.DbType.DateTime) { Value = pDonnee.HeureFin };

			var cmd = new SQLiteCommand("UPDATE Groupe SET Libelle=@Libelle, ID_JourSemaine=@IdJourSemaine, NbPlaces=@NbPlaces, Commentaire=@Commentaire, ID_Saison=@IdSaison, HeureDebutDT=@HeureDebut, HeureFinDT=@HeureFin WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(paramId);
			cmd.Parameters.Add(paramLibelle);
			cmd.Parameters.Add(paramIdJourSemaine);
			cmd.Parameters.Add(paramNbPlaces);
			cmd.Parameters.Add(paramCommentaire);
			cmd.Parameters.Add(paramIdSaison);
			cmd.Parameters.Add(paramHeureDebut);
			cmd.Parameters.Add(paramHeureFin);

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

		public void Delete(Groupe pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };

			var cmd = new SQLiteCommand("DELETE FROM Groupe WHERE ID=@Id;", this.Connection);
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

		public bool Exists(Groupe pDonnee) {
			this.Connection.Open();

			// La vérification s'effectuera sur la saison, le jour et le libellé
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Groupe WHERE ID_Saison=@IdSaison AND ID_JourSemaine=@IdJourSemaine AND UPPER(Libelle)=@Libelle;", this.Connection);
			var paramIdSaison = new SQLiteParameter("@IdSaison", System.Data.DbType.Int32) { Value = pDonnee.Saison.Id };
			var paramIdJourSemaine = new SQLiteParameter("@IdJourSemaine", System.Data.DbType.Int32) { Value = pDonnee.JourSemaine.Id };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle.ToUpper() };
			cmd.Parameters.Add(paramIdSaison);
			cmd.Parameters.Add(paramIdJourSemaine);
			cmd.Parameters.Add(paramLibelle);

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

		public bool IsUsed(Groupe pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Inscription WHERE ID_Groupe=@Id;", this.Connection);
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

		public Groupe Read(int pId) {
			this.Connection.Open();
			Groupe result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT * FROM V_Groupe WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Groupe()
				{
					Id = reader.GetInt32(0),
					Libelle = reader.GetString(1),
					NbPlaces = reader.GetInt32(2),
					Commentaire = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // gestion du NULL
					HeureDebut = reader.GetDateTime(4),
					HeureFin = reader.GetDateTime(5),

					Saison = new Saison()
					{
						Id = reader.GetInt32(6),
						EstSaisonCourante = reader.GetBoolean(7),
						AnneeDebut = reader.GetInt32(8),
						AnneeFin = reader.GetInt32(9)
					},

					JourSemaine = new JourSemaine()
					{
						Id = reader.GetInt32(10),
						Libelle = reader.GetString(11),
						Numero = reader.GetInt32(12)
					}
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Groupe> List() {
			this.Connection.Open();

			List<Groupe> result = new List<Groupe>();

			var cmd = new SQLiteCommand("SELECT * FROM V_Groupe;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Groupe()
					{
						Id = reader.GetInt32(0),
						Libelle = reader.GetString(1),
						NbPlaces = reader.GetInt32(2),
						Commentaire = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // gestion du NULL
						HeureDebut = reader.GetDateTime(4),
						HeureFin = reader.GetDateTime(5),

						Saison = new Saison()
						{
							Id = reader.GetInt32(6),
							EstSaisonCourante = reader.GetBoolean(7),
							AnneeDebut = reader.GetInt32(8),
							AnneeFin = reader.GetInt32(9)
						},

						JourSemaine = new JourSemaine()
						{
							Id = reader.GetInt32(10),
							Libelle = reader.GetString(11),
							Numero = reader.GetInt32(12)
						}
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}

		/// <summary>
		/// Récupère la liste des groupes pour la saison courante
		/// </summary>
		/// <returns>Groupe de la saison courante</returns>
		public List<Groupe> ListSaisonCourante() {
			var rq = from g in this.List()
					 where g.Saison.EstSaisonCourante
					 select g;

			return rq.ToList();
		}
	}
}
