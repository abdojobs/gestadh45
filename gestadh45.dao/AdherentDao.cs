using System.Collections.Generic;
using System.Data.SQLite;
using gestadh45.model;

namespace gestadh45.dao
{
	public class AdherentDao : DaoBase, IDao<Adherent>
	{
		public AdherentDao(string pFilePath) : base(pFilePath) { }

		public int Create(Adherent pDonnee) {
			this.Connection.Open();

			// Contact
			var cmdContact = new SQLiteCommand("INSERT INTO Contact(Telephone1, Telephone2, Telephone3, Mail1, Mail2, Mail3) VALUES @ContactTelephone1, @ContactTelephone2, @ContactTelephone3, @ContactMail1, @ContactMail2, @ContactMail3;", this.Connection);
			var paramContactTelephone1 = new SQLiteParameter("@ContactTelephone1", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone1.ToUpper() };
			var paramContactTelephone2 = new SQLiteParameter("@ContactTelephone2", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone2.ToUpper() };
			var paramContactTelephone3 = new SQLiteParameter("@ContactTelephone3", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone3.ToUpper() };
			var paramContactMail1 = new SQLiteParameter("@ContactMail1", System.Data.DbType.String) { Value = pDonnee.Contact.Mail1.ToLower() };
			var paramContactMail2 = new SQLiteParameter("@ContactMail2", System.Data.DbType.String) { Value = pDonnee.Contact.Mail2.ToLower() };
			var paramContactMail3 = new SQLiteParameter("@ContactMail3", System.Data.DbType.String) { Value = pDonnee.Contact.Mail3.ToLower() };
			cmdContact.Parameters.Add(paramContactTelephone1);
			cmdContact.Parameters.Add(paramContactTelephone2);
			cmdContact.Parameters.Add(paramContactTelephone3);
			cmdContact.Parameters.Add(paramContactMail1);
			cmdContact.Parameters.Add(paramContactMail2);
			cmdContact.Parameters.Add(paramContactMail3);
			
			// Adresse
			var cmdAdresse = new SQLiteCommand("INSERT INTO Adresse(Libelle, ID_Ville) VALUES @AdresseLibelle, @AdresseIdVille;", this.Connection);
			var paramAdresseLibelle = new SQLiteParameter("@AdresseLibelle", System.Data.DbType.String) { Value = pDonnee.Adresse.Libelle };
			var paramAdresseIdVille = new SQLiteParameter("@AdresseIdVille", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Ville.Id };
			cmdAdresse.Parameters.Add(paramAdresseLibelle);
			cmdAdresse.Parameters.Add(paramAdresseIdVille);
			
			// Adhérent
			var cmdAdherent = new SQLiteCommand("INSERT INTO Adherent(Nom, Prenom, DateNaissance, DateCreation, DateModification, Commentaire, ID_Sexe, ID_Contact, ID_Adresse) Values(@Nom, @Prenom, @DateNaissance, @DateCreation, @DateModification, @Commentaire, @IdSexe, @IdContact, @IdAdresse);", this.Connection);
			var paramNom = new SQLiteParameter("@Nom", System.Data.DbType.String) { Value = pDonnee.Nom.ToUpper() };
			var paramPrenom = new SQLiteParameter("@Prenom", System.Data.DbType.String) { Value = pDonnee.Prenom };
			var paramDateNaissance = new SQLiteParameter("@DateNaissance", System.Data.DbType.DateTime) { Value = pDonnee.DateNaissance };
			var paramDateCreation = new SQLiteParameter("@DateCreation", System.Data.DbType.DateTime) { Value = pDonnee.DateCreation };
			var paramDateModification = new SQLiteParameter("@DateModification", System.Data.DbType.DateTime) { Value = pDonnee.DateModification };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdSexe = new SQLiteParameter("@IdSexe", System.Data.DbType.Int32) { Value = pDonnee.Sexe.Id };
			cmdAdherent.Parameters.Add(paramNom);
			cmdAdherent.Parameters.Add(paramPrenom);
			cmdAdherent.Parameters.Add(paramDateNaissance);
			cmdAdherent.Parameters.Add(paramDateCreation);
			cmdAdherent.Parameters.Add(paramDateModification);
			cmdAdherent.Parameters.Add(paramCommentaire);
			cmdAdherent.Parameters.Add(paramIdSexe);

			try {
				cmdContact.ExecuteNonQuery();
				var paramIdContact = new SQLiteParameter("@IdContact", System.Data.DbType.Int32) { Value = this.GetLastInsertId() };
				cmdAdherent.Parameters.Add(paramIdContact);

				cmdAdresse.ExecuteNonQuery();
				var paramIdAdresse = new SQLiteParameter("@IdAdresse", System.Data.DbType.Int32) { Value = this.GetLastInsertId() };
				cmdAdherent.Parameters.Add(paramIdAdresse);

				cmdAdherent.ExecuteNonQuery();

				return this.GetLastInsertId();
			}
			catch (SQLiteException) {
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public void Update(Adherent pDonnee) {
			this.Connection.Open();
			var trans = this.Connection.BeginTransaction();

			// Contact
			var cmdContact = new SQLiteCommand("UPDATE Contact SET Telephone1=@ContactTelephone1, Telephone2=@ContactTelephone2, Telephone3=@ContactTelephone3, Mail1=@ContactMail1, Mail2=@ContactMail2, Mail3=@ContactMail3 WHERE ID=@IdContact;", this.Connection, trans);
			var paramIdContact = new SQLiteParameter("@IdContact", System.Data.DbType.Int32) { Value = pDonnee.Contact.Id };
			var paramContactTelephone1 = new SQLiteParameter("@ContactTelephone1", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone1.ToUpper() };
			var paramContactTelephone2 = new SQLiteParameter("@ContactTelephone2", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone2.ToUpper() };
			var paramContactTelephone3 = new SQLiteParameter("@ContactTelephone3", System.Data.DbType.String) { Value = pDonnee.Contact.Telephone3.ToUpper() };
			var paramContactMail1 = new SQLiteParameter("@ContactMail1", System.Data.DbType.String) { Value = pDonnee.Contact.Mail1.ToLower() };
			var paramContactMail2 = new SQLiteParameter("@ContactMail2", System.Data.DbType.String) { Value = pDonnee.Contact.Mail2.ToLower() };
			var paramContactMail3 = new SQLiteParameter("@ContactMail3", System.Data.DbType.String) { Value = pDonnee.Contact.Mail3.ToLower() };
			cmdContact.Parameters.Add(paramIdContact);
			cmdContact.Parameters.Add(paramContactTelephone1);
			cmdContact.Parameters.Add(paramContactTelephone2);
			cmdContact.Parameters.Add(paramContactTelephone3);
			cmdContact.Parameters.Add(paramContactMail1);
			cmdContact.Parameters.Add(paramContactMail2);
			cmdContact.Parameters.Add(paramContactMail3);

			// Adresse
			var cmdAdresse = new SQLiteCommand("UPDATE Adresse SET Libelle=@AdresseLibelle, ID_Ville=@IdVille WHERE ID=@IdAdresse;", this.Connection,trans);
			var paramIdAdresse = new SQLiteParameter("@IdAdresse", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Id };
			var paramAdresseLibelle = new SQLiteParameter("@AdresseLibelle", System.Data.DbType.String) { Value = pDonnee.Adresse.Libelle };
			var paramAdresseIdVille = new SQLiteParameter("@AdresseIdVille", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Ville.Id };
			cmdAdresse.Parameters.Add(paramIdAdresse);
			cmdAdresse.Parameters.Add(paramAdresseLibelle);
			cmdAdresse.Parameters.Add(paramAdresseIdVille);

			// Adhérent (la date de création ne peut être modifiée)
			var cmdAdherent = new SQLiteCommand("UPDATE Adherent SET Nom=@Nom, Prenom=@Prenom, DateNaissance=@DateNaissance, DateModification=@DateModification, Commentaire=@Commentaire, ID_Sexe=@IdSexe WHERE ID = @Id);", this.Connection, trans);
			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramNom = new SQLiteParameter("@Nom", System.Data.DbType.String) { Value = pDonnee.Nom.ToUpper() };
			var paramPrenom = new SQLiteParameter("@Prenom", System.Data.DbType.String) { Value = pDonnee.Prenom };
			var paramDateNaissance = new SQLiteParameter("@DateNaissance", System.Data.DbType.DateTime) { Value = pDonnee.DateNaissance };
			var paramDateModification = new SQLiteParameter("@DateModification", System.Data.DbType.DateTime) { Value = pDonnee.DateModification };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdSexe = new SQLiteParameter("@IdSexe", System.Data.DbType.Int32) { Value = pDonnee.Sexe.Id };
			cmdAdherent.Parameters.Add(paramId);
			cmdAdherent.Parameters.Add(paramNom);
			cmdAdherent.Parameters.Add(paramPrenom);
			cmdAdherent.Parameters.Add(paramDateNaissance);
			cmdAdherent.Parameters.Add(paramDateModification);
			cmdAdherent.Parameters.Add(paramCommentaire);
			cmdAdherent.Parameters.Add(paramIdSexe);

			try {
				cmdContact.ExecuteNonQuery();
				cmdAdresse.ExecuteNonQuery();
				cmdAdherent.ExecuteNonQuery();

				trans.Commit();
			}
			catch (SQLiteException) {
				trans.Rollback();
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public void Delete(Adherent pDonnee) {
			this.Connection.Open();
			var trans = this.Connection.BeginTransaction();

			var cmdContact = new SQLiteCommand("DELETE FROM Contact WHERE ID=@IdCOntact;", this.Connection, trans);
			var paramIdContact = new SQLiteParameter("@IdContact", System.Data.DbType.Int32) { Value = pDonnee.Contact.Id };
			cmdContact.Parameters.Add(paramIdContact);

			var cmdAdresse = new SQLiteCommand("DELETE FROM Adresse WHERE ID=@IdAdresse;", this.Connection, trans);
			var paramIdAdresse = new SQLiteParameter("@IdAdresse", System.Data.DbType.Int32) { Value = pDonnee.Adresse.Id };
			cmdAdresse.Parameters.Add(paramIdAdresse);

			var cmdAdherent = new SQLiteCommand("DELETE FROM Adherent WHERE ID=@Id;", this.Connection, trans);
			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			cmdAdherent.Parameters.Add(paramId);

			try {
				cmdContact.ExecuteNonQuery();
				cmdAdresse.ExecuteNonQuery();
				cmdAdherent.ExecuteNonQuery();

				trans.Commit();
			}
			catch (SQLiteException) {
				trans.Rollback();
				throw;
			}
			finally {
				this.Connection.Close();
			}
		}

		public bool Exists(Adherent pDonnee) {
			this.Connection.Open();

			// la vérification s'effectuera sur le nom et le prénom
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Adherent WHERE UPPER(Nom)=@Nom AND UPPER(Prenom)=@Prenom;", this.Connection);
			var paramNom = new SQLiteParameter("@Nom", System.Data.DbType.String) { Value = pDonnee.Nom.ToUpper() };
			var paramPrenom = new SQLiteParameter("@Prenom", System.Data.DbType.String) { Value = pDonnee.Prenom.ToUpper() };

			cmd.Parameters.Add(paramNom);
			cmd.Parameters.Add(paramPrenom);

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

		public bool IsUsed(Adherent pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Inscription WHERE ID_Adherent=@Id;", this.Connection);
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

		public Adherent Read(int pId) {
			this.Connection.Open();
			Adherent result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT * FROM V_Adherent WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Adherent()
				{
					Id = reader.GetInt32(0),
					Nom = reader.GetString(1),
					Prenom = reader.GetString(2),
					DateNaissance = reader.GetDateTime(3),
					DateCreation = reader.GetDateTime(4),
					DateModification = reader.GetDateTime(5),
					Commentaire = reader.IsDBNull(6) ? string.Empty : reader.GetString(6), // gestion du NULL

					Sexe = new Sexe()
					{
						Id = reader.GetInt32(7),
						LibelleCourt = reader.GetString(8),
						LibelleLong = reader.GetString(9)
					},

					Contact = new Contact()
					{
						Id = reader.GetInt32(10),
						Telephone1 = reader.IsDBNull(11) ? string.Empty : reader.GetString(11), // gestion du NULL
						Telephone2 = reader.IsDBNull(12) ? string.Empty : reader.GetString(12), // gestion du NULL
						Telephone3 = reader.IsDBNull(13) ? string.Empty : reader.GetString(13), // gestion du NULL
						Mail1 = reader.IsDBNull(14) ? string.Empty : reader.GetString(14), // gestion du NULL
						Mail2 = reader.IsDBNull(15) ? string.Empty : reader.GetString(15), // gestion du NULL
						Mail3 = reader.IsDBNull(16) ? string.Empty : reader.GetString(16), // gestion du NULL
						SiteWeb = reader.IsDBNull(17) ? string.Empty : reader.GetString(17) // gestion du NULL
					},

					Adresse = new Adresse()
					{
						Id = reader.GetInt32(18),
						Libelle = reader.GetString(19),

						Ville = new Ville()
						{
							Id = reader.GetInt32(20),
							CodePostal = reader.GetString(21),
							Libelle = reader.GetString(22)
						}
					}
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Adherent> List() {
			this.Connection.Open();

			List<Adherent> result = new List<Adherent>();

			var cmd = new SQLiteCommand("SELECT * FROM V_Adherent;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Adherent()
					{
						Id = reader.GetInt32(0),
						Nom = reader.GetString(1),
						Prenom = reader.GetString(2),
						DateNaissance = reader.GetDateTime(3),
						DateCreation = reader.GetDateTime(4),
						DateModification = reader.GetDateTime(5),
						Commentaire = reader.IsDBNull(6) ? string.Empty : reader.GetString(6), // gestion du NULL

						Sexe = new Sexe()
						{
							Id = reader.GetInt32(7),
							LibelleCourt = reader.GetString(8),
							LibelleLong = reader.GetString(9)
						},

						Contact = new Contact()
						{
							Id = reader.GetInt32(10),
							Telephone1 = reader.IsDBNull(11) ? string.Empty : reader.GetString(11), // gestion du NULL
							Telephone2 = reader.IsDBNull(12) ? string.Empty : reader.GetString(12), // gestion du NULL
							Telephone3 = reader.IsDBNull(13) ? string.Empty : reader.GetString(13), // gestion du NULL
							Mail1 = reader.IsDBNull(14) ? string.Empty : reader.GetString(14), // gestion du NULL
							Mail2 = reader.IsDBNull(15) ? string.Empty : reader.GetString(15), // gestion du NULL
							Mail3 = reader.IsDBNull(16) ? string.Empty : reader.GetString(16), // gestion du NULL
							SiteWeb = reader.IsDBNull(17) ? string.Empty : reader.GetString(17) // gestion du NULL
						},

						Adresse = new Adresse()
						{
							Id = reader.GetInt32(18),
							Libelle = reader.GetString(19),

							Ville = new Ville()
							{
								Id = reader.GetInt32(20),
								CodePostal = reader.GetString(21),
								Libelle = reader.GetString(22)
							}
						}
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}
	}
}
