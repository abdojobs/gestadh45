using System.Data.SQLite;
using gestadh45.model;
using gestadh45.model.bo;

namespace gestadh45.dao
{
	public class InfosClubDao : DaoBase, IInfosClubDao
	{
		public InfosClubDao(string pFilePath) : base(pFilePath) { }

		public void Update(InfosClub pDonnee) {
			this.Connection.Open();

			// on cree une transaction pour regrouper les 3 requetes de maj (adresse, contact, infosclub)
			var trans = this.Connection.BeginTransaction();

			var cmdAdresse = new SQLiteCommand("UPDATE Adresse SET Libelle=@AdresseLibelle, ID_Ville=@AdresseIdVille WHERE ID=@IdAdresse;", this.Connection, trans);
			var paramIdAdresse = new SQLiteParameter("@IdAdresse", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Id };
			var paramAdresseLibelle = new SQLiteParameter("@AdresseLibelle", System.Data.DbType.String) { Value = pDonnee.Adresse.Libelle };
			var paramAdresseIdVille = new SQLiteParameter("@AdresseIdVille", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Ville.Id };
			cmdAdresse.Parameters.Add(paramIdAdresse);
			cmdAdresse.Parameters.Add(paramAdresseLibelle);
			cmdAdresse.Parameters.Add(paramAdresseIdVille);

			var cmdContact = new SQLiteCommand("UPDATE Contact SET Telephone1=@ContactTelephone, Mail1=@ContactMail, SiteWeb=@ContactSiteWeb WHERE ID=@IdContact;", this.Connection, trans);
			var paramIdContact = new SQLiteParameter("@IdContact", System.Data.DbType.Int32) { Value = pDonnee.Contact.Id };
			var paramContactTelephone = new SQLiteParameter("@ContactTelephone", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone1.Numero };
			var paramContactMail = new SQLiteParameter("@ContactMail", System.Data.DbType.String) { Value = pDonnee.Contact.Mail1.Adresse };	// toujours en minuscule
			var paramContactSiteWeb = new SQLiteParameter("@ContactSiteWeb", System.Data.DbType.String) { Value = pDonnee.Contact.SiteWeb.ToLower() };	// toujours en minuscule
			cmdContact.Parameters.Add(paramIdContact);
			cmdContact.Parameters.Add(paramContactTelephone);
			cmdContact.Parameters.Add(paramContactMail);
			cmdContact.Parameters.Add(paramContactSiteWeb);

			var cmdInfosClub = new SQLiteCommand("UPDATE InfosClub SET Nom=@Nom, Numero=@Numero, Siren=@Siren, NIC=@NIC WHERE ID=@Id;", this.Connection, trans);
			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramNom = new SQLiteParameter("@Nom", System.Data.DbType.String) { Value = pDonnee.Nom.ToUpper() };
			var paramNumero = new SQLiteParameter("@Numero", System.Data.DbType.String) { Value = pDonnee.Numero.ToUpper() };
			var paramSiren = new SQLiteParameter("@Siren", System.Data.DbType.String) { Value = pDonnee.Siren.ToUpper() };
			var paramNIC = new SQLiteParameter("@NIC", System.Data.DbType.String) { Value = pDonnee.NIC.ToUpper() };
			cmdInfosClub.Parameters.Add(paramId);
			cmdInfosClub.Parameters.Add(paramNom);
			cmdInfosClub.Parameters.Add(paramNumero);
			cmdInfosClub.Parameters.Add(paramSiren);
			cmdInfosClub.Parameters.Add(paramNIC);

			try {
				cmdAdresse.ExecuteNonQuery();
				cmdContact.ExecuteNonQuery();
				cmdInfosClub.ExecuteNonQuery();

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

		public InfosClub Read() {
			this.Connection.Open();
			InfosClub result = null;

			var cmd = new SQLiteCommand("SELECT * FROM V_InfosClub;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new InfosClub()
				{
					Id = reader.GetInt32(0),
					Nom = reader.GetString(1),
					Numero = reader.IsDBNull(2) ? string.Empty : reader.GetString(2), // gestion du NULL
					Siren = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // gestion du NULL
					NIC = reader.IsDBNull(4) ? string.Empty : reader.GetString(4), // gestion du NULL

					Adresse = new Adresse()
					{
						Id = reader.GetInt32(5),
						Libelle = reader.GetString(6),
						Ville = new Ville()
						{
							Id = reader.GetInt32(7),
							CodePostal = reader.GetString(8),
							Libelle = reader.GetString(9)
						}
					},

					Contact = new Contact()
					{
						Id = reader.GetInt32(10),
						Telephone1 = new NumeroTelephone() { Numero = reader.GetString(11) },
						Mail1 = new AdresseEmail() { Adresse = reader.GetString(12) },
						SiteWeb = reader.GetString(13)
					}
				};
			}

			this.Connection.Close();
			return result;
		}
	}
}
