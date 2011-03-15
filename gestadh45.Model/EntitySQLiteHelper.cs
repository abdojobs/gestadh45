using System.Data.EntityClient;
using System.Data.SQLite;

namespace gestadh45.Model
{
	public static class EntitySQLiteHelper
	{
		public const string Metadata = "res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl";
		public const string Provider = "System.Data.SQLite";

		public static string GetConnectionString(string pFilePath) {
			SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder
			{
				DataSource = pFilePath
			};

			EntityConnectionStringBuilder builder2 = new EntityConnectionStringBuilder
			{
				Metadata = "res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl",
				Provider = "System.Data.SQLite",
				ProviderConnectionString = builder.ConnectionString
			};

			return builder2.ConnectionString;
		}

		public static string GetFilePathFromContext(Entities pContexte) {
			EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder(pContexte.Connection.ConnectionString);
			return builder.ProviderConnectionString;
		}
	}
}
