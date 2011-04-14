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
					using (SQLiteCommand lCommand = new SQLiteCommand(ResDatabase.SQL_Initialisation, lConnection)) {
						lCommand.ExecuteNonQuery();
						lConnection.Close();
					}
				}
			}
		}
	}
}
