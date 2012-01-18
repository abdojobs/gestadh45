using System.Data.SQLite;
using System;

namespace gestadh45.service.Database
{
	public class GenerateurDatabase
	{
		public string ConnectionString { get; set; }

		public GenerateurDatabase(string pFilePath) {
			if (!string.IsNullOrWhiteSpace(pFilePath)) {
				SQLiteConnectionStringBuilder lBuilder = new SQLiteConnectionStringBuilder();
				lBuilder.DataSource = pFilePath;
				this.ConnectionString = lBuilder.ToString();
			}
		}

		public void CreateDatabase() {
			if (!string.IsNullOrWhiteSpace(this.ConnectionString)) {
				using (SQLiteConnection lConnection = new SQLiteConnection(this.ConnectionString)) {
					lConnection.Open();

					using (SQLiteTransaction lTransaction = lConnection.BeginTransaction()) {
						// Suppression des tables pour le cas où la base existe déjà
						using (SQLiteCommand lCommand = new SQLiteCommand(ResDatabase.SQL_DropTables, lConnection, lTransaction)) {
							lCommand.ExecuteNonQuery();
						}

						// Création des tables
						using (SQLiteCommand lCommand = new SQLiteCommand(ResDatabase.SQL_CreateTables, lConnection, lTransaction)) {
							lCommand.ExecuteNonQuery();
						}

						// Insertion des données indispensables
						DonneesBase lDonnees = new DonneesBase();
						string lSql = string.Format(
							ResDatabase.SQL_InsertData,
							lDonnees.LibelleVilleClub,
							lDonnees.CodePostalVilleClub,
							lDonnees.CodePostalVilleClub,
							lDonnees.NomClub,
							lDonnees.AnneeDebutSaison,
							lDonnees.AnneeDebutSaison + 1
						);

						using (SQLiteCommand lCommand = new SQLiteCommand(lSql, lConnection, lTransaction)) {
							lCommand.ExecuteNonQuery();
						}

						lTransaction.Commit();
					}
				}
			}
		}
	}
}
