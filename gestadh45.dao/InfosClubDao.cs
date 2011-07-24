using System;
using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class InfosClubDao : DaoBase, IDao<InfosClub>
	{
		public InfosClubDao(string pFilePath) : base(pFilePath) { }

		/// <summary>
		/// Lève une exception car ne doit pas être appellée
		/// </summary>
		/// <param name="pDonnee"></param>
		/// <returns></returns>
		public int Create(InfosClub pDonnee) {
			throw new NotImplementedException("La méthode InfosClubDao.Create ne doit pas être appellée");
		}

		public void Update(InfosClub pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramNom = new SQLiteParameter("@Nom", System.Data.DbType.String) { Value = pDonnee.Nom.ToUpper() };
			var paramNumero = new SQLiteParameter("@Numero", System.Data.DbType.String) { Value = pDonnee.Numero.ToUpper() };
			var paramSiren = new SQLiteParameter("@Siren", System.Data.DbType.String) { Value = pDonnee.Siren.ToUpper() };
			var paramNIC = new SQLiteParameter("@NIC", System.Data.DbType.String) { Value = pDonnee.NIC.ToUpper() };

			var paramIdAdresse = new SQLiteParameter("@IdAdresse", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Id };
			var paramAdresseLibelle = new SQLiteParameter("@AdresseLibelle", System.Data.DbType.String) { Value = pDonnee.Adresse.Libelle };
			var paramAdresseIdVille = new SQLiteParameter("@AdresseIdVille", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Ville.Id };

			var paramIdContact = new SQLiteParameter("@IdContact", System.Data.DbType.Int32) { Value = pDonnee.Contact.Id };
			var paramContactTelephone = new SQLiteParameter("@ContactTelephone", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone1.ToUpper() };
			var paramContactMail = new SQLiteParameter("@ContactMail", System.Data.DbType.String) { Value = pDonnee.Contact.Mail1.ToLower() };	// toujours en minuscule
			var paramContactSiteWeb = new SQLiteParameter("@ContactSiteWeb", System.Data.DbType.String) { Value = pDonnee.Contact.SiteWeb.ToLower() };	// toujours en minuscule

			// on cree une transaction pour regrouper les 3 requetes de maj (adresse, contact, infosclub)
			var trans = this.Connection.BeginTransaction();

			var cmdAdresse = new SQLiteCommand("UPDATE Adresse SET Libelle=@AdresseLibelle, ID_Ville=@AdresseIdVille WHERE ID=@IdAdresse;", this.Connection, trans);
			cmdAdresse.Parameters.Add(paramIdAdresse);
			cmdAdresse.Parameters.Add(paramAdresseLibelle);
			cmdAdresse.Parameters.Add(paramAdresseIdVille);

			var cmdContact = new SQLiteCommand("UPDATE Contact SET Telephone1=@ContactTelephone, Mail1=@ContactMail, @SiteWeb=ContactSiteWeb WHERE ID=@IdContact;", this.Connection, trans);
			cmdContact.Parameters.Add(paramIdContact);
			cmdContact.Parameters.Add(paramContactTelephone);
			cmdContact.Parameters.Add(paramContactMail);
			cmdContact.Parameters.Add(paramContactSiteWeb);

			var cmdInfosClub = new SQLiteCommand("UPDATE InfosClub SET Nom=@Nom, Numero=@Numero, Siren=@Siren, NIC=@NIC WHERE ID=@Id;", this.Connection, trans);
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

		/// <summary>
		/// Lève une exception car ne doit pas être appellée
		/// </summary>
		/// <param name="pDonnee"></param>
		public void Delete(InfosClub pDonnee) {
			throw new NotImplementedException("La méthode InfosClubDao.Delete ne doit pas être appellée");
		}

		/// <summary>
		/// Lève une exception car ne doit pas être appellée
		/// </summary>
		/// <param name="pDonnee"></param>
		public bool Exists(InfosClub pDonnee) {
			throw new NotImplementedException("La méthode InfosClubDao.Exists ne doit pas être appellée");
		}

		/// <summary>
		/// Lève une exception car ne doit pas être appellée
		/// </summary>
		/// <param name="pDonnee"></param>
		public bool IsUsed(InfosClub pDonnee) {
			throw new NotImplementedException("La méthode InfosClubDao.IsUsed ne doit pas être appellée");
		}

		public InfosClub Read(int pId) {
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
						Telephone1 = reader.GetString(11),
						Mail1 = reader.GetString(12),
						SiteWeb = reader.GetString(13)
					}
				};
			}

			this.Connection.Close();
			return result;
		}

		/// <summary>
		/// Lève une exception car ne doit pas être appellée
		/// </summary>
		/// <param name="pDonnee"></param>
		public List<InfosClub> List() {
			throw new NotImplementedException("La méthode InfosClubDao.List ne doit pas être appellée");
		}
	}
}
