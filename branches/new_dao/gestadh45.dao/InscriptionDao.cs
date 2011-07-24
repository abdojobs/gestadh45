using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using gestadh45.model;

namespace gestadh45.dao
{
	public class InscriptionDao : DaoBase, IDao<Inscription>
	{
		public InscriptionDao(string pFilePath) : base(pFilePath) { }

		public int Create(Inscription pDonnee) {
			this.Connection.Open();

			var cmd = new SQLiteCommand("INSERT INTO Inscription(ID_Adherent, ID_Groupe, CertificatMedicalRemis, Cotisation, DateCreation, DateModification, Commentaire, ID_StatutInscription) VALUES(@IdAdherent, @IdGroupe, @CertificatMedicalRemis, @Cotisation, @DateCreation, @DateModification, @Commentaire, @IdStatutInscription);", this.Connection);
			var paramIdAdherent = new SQLiteParameter("@IdAdherent", System.Data.DbType.Int32) { Value = pDonnee.Adherent.Id };
			var paramIdGroupe = new SQLiteParameter("@IdGroupe", System.Data.DbType.Int32) { Value = pDonnee.Groupe.Id };
			var paramCertificatMedicalRemis = new SQLiteParameter("@CertificatMedicalRemis", System.Data.DbType.Boolean) { Value = pDonnee.CertificatMedicalRemis };
			var paramCotisation = new SQLiteParameter("@Cotisation", System.Data.DbType.Decimal) { Value = pDonnee.Cotisation };
			var paramDateCreation = new SQLiteParameter("@DateCreation", System.Data.DbType.DateTime) { Value = pDonnee.DateCreation };
			var paramDateModification = new SQLiteParameter("@DateModification", System.Data.DbType.DateTime) { Value = pDonnee.DateModification };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdStatutInscription = new SQLiteParameter("@IdStatutInscription", System.Data.DbType.Int32) { Value = pDonnee.StatutInscription.Id };
			cmd.Parameters.Add(paramIdAdherent);
			cmd.Parameters.Add(paramIdGroupe);
			cmd.Parameters.Add(paramCertificatMedicalRemis);
			cmd.Parameters.Add(paramCotisation);
			cmd.Parameters.Add(paramDateCreation);
			cmd.Parameters.Add(paramDateModification);
			cmd.Parameters.Add(paramCommentaire);
			cmd.Parameters.Add(paramIdStatutInscription);

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

		public void Update(Inscription pDonnee) {
			this.Connection.Open();

			// la date de création ne peut être modifiée
			var cmd = new SQLiteCommand("UPDATE Inscription SET ID_Adherent=@IdAdherent, ID_Groupe=@IdGroupe, CertificatMedicalRemis=@CertificatMedicalRemis, Cotisation=@Cotisation, DateModification=@DateModification, Commentaire=@Commentaire, ID_StatutInscription=@IdStatutInscription WHERE ID=@Id;", this.Connection);
			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };
			var paramIdAdherent = new SQLiteParameter("@IdAdherent", System.Data.DbType.Int32) { Value = pDonnee.Adherent.Id };
			var paramIdGroupe = new SQLiteParameter("@IdGroupe", System.Data.DbType.Int32) { Value = pDonnee.Groupe.Id };
			var paramCertificatMedicalRemis = new SQLiteParameter("@CertificatMedicalRemis", System.Data.DbType.Boolean) { Value = pDonnee.CertificatMedicalRemis };
			var paramCotisation = new SQLiteParameter("@Cotisation", System.Data.DbType.Decimal) { Value = pDonnee.Cotisation };
			var paramDateModification = new SQLiteParameter("@DateModification", System.Data.DbType.DateTime) { Value = pDonnee.DateModification };
			var paramCommentaire = new SQLiteParameter("@Commentaire", System.Data.DbType.String) { Value = pDonnee.Commentaire };
			var paramIdStatutInscription = new SQLiteParameter("@IdStatutInscription", System.Data.DbType.Int32) { Value = pDonnee.StatutInscription.Id };
			cmd.Parameters.Add(paramId);
			cmd.Parameters.Add(paramIdAdherent);
			cmd.Parameters.Add(paramIdGroupe);
			cmd.Parameters.Add(paramCertificatMedicalRemis);
			cmd.Parameters.Add(paramCotisation);
			cmd.Parameters.Add(paramDateModification);
			cmd.Parameters.Add(paramCommentaire);
			cmd.Parameters.Add(paramIdStatutInscription);

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

		public void Delete(Inscription pDonnee) {
			this.Connection.Open();

			var paramId = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pDonnee.Id };

			var cmd = new SQLiteCommand("DELETE FROM Inscription WHERE ID=@Id;", this.Connection);
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

		public bool Exists(Inscription pDonnee) {
			this.Connection.Open();

			// La vérification s'effectuera sur le couple adhérent/groupe (le groupe inclut la saison)
			var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Inscription WHERE ID_Adherent=@IdAdherent AND ID_Groupe=@IdGroupe;", this.Connection);
			var paramIdAdherent = new SQLiteParameter("@IdAdherent", System.Data.DbType.Int32) { Value = pDonnee.Adherent.Id };
			var paramIdGroupe = new SQLiteParameter("@IdGroupe", System.Data.DbType.Int32) { Value = pDonnee.Groupe.Id };
			cmd.Parameters.Add(paramIdAdherent);
			cmd.Parameters.Add(paramIdGroupe);

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

		public bool IsUsed(Inscription pDonnee) {
			// une inscription est un objet du plus haut niveau, il n'est utilisé par aucun autre
			return false;
		}

		public Inscription Read(int pId) {
			this.Connection.Open();
			Inscription result = null;

			var param = new SQLiteParameter("@Id", System.Data.DbType.Int32) { Value = pId };
			var cmd = new SQLiteCommand("SELECT * FROM V_Inscription WHERE ID=@Id;", this.Connection);
			cmd.Parameters.Add(param);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				reader.Read();

				result = new Inscription()
				{
					Id = reader.GetInt32(0),
					CertificatMedicalRemis = reader.GetBoolean(1),
					Cotisation = reader.GetDecimal(2),
					DateCreation = reader.GetDateTime(3),
					DateModification = reader.GetDateTime(4),
					Commentaire = reader.IsDBNull(5) ? string.Empty : reader.GetString(5), // gestion du NULL

					Adherent = new Adherent()
					{
						Id = reader.GetInt32(6),
						Nom = reader.GetString(7),
						Prenom = reader.GetString(8),
						DateNaissance = reader.GetDateTime(9),
						DateCreation = reader.GetDateTime(10),
						DateModification = reader.GetDateTime(11),
						Commentaire = reader.IsDBNull(12) ? string.Empty : reader.GetString(12), // gestion du NULL

						Sexe = new Sexe()
						{
							Id = reader.GetInt32(13),
							LibelleCourt = reader.GetString(14),
							LibelleLong = reader.GetString(15)
						},

						Contact = new Contact()
						{
							Id = reader.GetInt32(16),
							Telephone1 = reader.IsDBNull(17) ? string.Empty : reader.GetString(17), // gestion du NULL
							Telephone2 = reader.IsDBNull(18) ? string.Empty : reader.GetString(18), // gestion du NULL
							Telephone3 = reader.IsDBNull(19) ? string.Empty : reader.GetString(19), // gestion du NULL
							Mail1 = reader.IsDBNull(20) ? string.Empty : reader.GetString(20), // gestion du NULL
							Mail2 = reader.IsDBNull(21) ? string.Empty : reader.GetString(21), // gestion du NULL
							Mail3 = reader.IsDBNull(22) ? string.Empty : reader.GetString(22), // gestion du NULL
							SiteWeb = reader.IsDBNull(23) ? string.Empty : reader.GetString(23) // gestion du NULL
						},

						Adresse = new Adresse()
						{
							Id = reader.GetInt32(24),
							Libelle = reader.GetString(25),

							Ville = new Ville()
							{
								Id = reader.GetInt32(26),
								CodePostal = reader.GetString(27),
								Libelle = reader.GetString(28)
							}
						}
					},

					Groupe = new Groupe()
					{
						Id = reader.GetInt32(29),
						Libelle = reader.GetString(30),
						NbPlaces = reader.GetInt32(31),
						Commentaire = reader.IsDBNull(32) ? string.Empty : reader.GetString(32), // gestion du NULL
						HeureDebut = reader.GetDateTime(33),
						HeureFin = reader.GetDateTime(34),

						Saison = new Saison()
						{
							Id = reader.GetInt32(35),
							EstSaisonCourante = reader.GetBoolean(36),
							AnneeDebut = reader.GetInt32(37),
							AnneeFin = reader.GetInt32(38)
						},

						JourSemaine = new JourSemaine()
						{
							Id = reader.GetInt32(39),
							Libelle = reader.GetString(40),
							Numero = reader.GetInt32(41)
						}
					},

					StatutInscription = new StatutInscription()
					{
						Id = reader.GetInt32(42),
						Libelle = reader.GetString(43),
						CodeCouleur = reader.GetString(44),
						Ordre = reader.GetInt32(45)
					}
				};
			}

			this.Connection.Close();
			return result;
		}

		public List<Inscription> List() {
			this.Connection.Open();
			List<Inscription> result = new List<Inscription>();

			var cmd = new SQLiteCommand("SELECT * FROM V_Inscription;", this.Connection);

			var reader = cmd.ExecuteReader();

			if (reader.HasRows) {
				while (reader.Read()) {
					var donnee = new Inscription()
					{
						Id = reader.GetInt32(0),
						CertificatMedicalRemis = reader.GetBoolean(1),
						Cotisation = reader.GetDecimal(2),
						DateCreation = reader.GetDateTime(3),
						DateModification = reader.GetDateTime(4),
						Commentaire = reader.IsDBNull(5) ? string.Empty : reader.GetString(5), // gestion du NULL

						Adherent = new Adherent()
						{
							Id = reader.GetInt32(6),
							Nom = reader.GetString(7),
							Prenom = reader.GetString(8),
							DateNaissance = reader.GetDateTime(9),
							DateCreation = reader.GetDateTime(10),
							DateModification = reader.GetDateTime(11),
							Commentaire = reader.IsDBNull(12) ? string.Empty : reader.GetString(12), // gestion du NULL

							Sexe = new Sexe()
							{
								Id = reader.GetInt32(13),
								LibelleCourt = reader.GetString(14),
								LibelleLong = reader.GetString(15)
							},

							Contact = new Contact()
							{
								Id = reader.GetInt32(16),
								Telephone1 = reader.IsDBNull(17) ? string.Empty : reader.GetString(17), // gestion du NULL
								Telephone2 = reader.IsDBNull(18) ? string.Empty : reader.GetString(18), // gestion du NULL
								Telephone3 = reader.IsDBNull(19) ? string.Empty : reader.GetString(19), // gestion du NULL
								Mail1 = reader.IsDBNull(20) ? string.Empty : reader.GetString(20), // gestion du NULL
								Mail2 = reader.IsDBNull(21) ? string.Empty : reader.GetString(21), // gestion du NULL
								Mail3 = reader.IsDBNull(22) ? string.Empty : reader.GetString(22), // gestion du NULL
								SiteWeb = reader.IsDBNull(23) ? string.Empty : reader.GetString(23) // gestion du NULL
							},

							Adresse = new Adresse()
							{
								Id = reader.GetInt32(24),
								Libelle = reader.GetString(25),

								Ville = new Ville()
								{
									Id = reader.GetInt32(26),
									CodePostal = reader.GetString(27),
									Libelle = reader.GetString(28)
								}
							}
						},

						Groupe = new Groupe()
						{
							Id = reader.GetInt32(29),
							Libelle = reader.GetString(30),
							NbPlaces = reader.GetInt32(31),
							Commentaire = reader.IsDBNull(32) ? string.Empty : reader.GetString(32), // gestion du NULL
							HeureDebut = reader.GetDateTime(33),
							HeureFin = reader.GetDateTime(34),

							Saison = new Saison()
							{
								Id = reader.GetInt32(35),
								EstSaisonCourante = reader.GetBoolean(36),
								AnneeDebut = reader.GetInt32(37),
								AnneeFin = reader.GetInt32(38)
							},

							JourSemaine = new JourSemaine()
							{
								Id = reader.GetInt32(39),
								Libelle = reader.GetString(40),
								Numero = reader.GetInt32(41)
							}
						},

						StatutInscription = new StatutInscription()
						{
							Id = reader.GetInt32(42),
							Libelle = reader.GetString(43),
							CodeCouleur = reader.GetString(44),
							Ordre = reader.GetInt32(45)
						}
					};

					result.Add(donnee);
				}
			}

			this.Connection.Close();
			return result;
		}

		/// <summary>
		/// Retourne la liste des inscriptions pour la saison courante
		/// </summary>
		/// <returns>Liste des inscriptions pour la saison courante</returns>
		public List<Inscription> ListSaisonCourante() {
			// TODO faire des tests de perf pour voir si linq est assez performant pour le filtre ou si il faut passer par une nouvelle vue
			var rq = from Inscription i in this.List()
					 where i.Groupe.Saison.EstSaisonCourante
					 select i;

			return rq.ToList();
		}

		/// <summary>
		/// Récupère la liste des inscriptions pour un groupe donné
		/// </summary>
		/// <param name="pGroupe">Groupe</param>
		/// <returns>Liste des inscriptions du groupe</returns>
		public List<Inscription> ListGroupe(Groupe pGroupe) {
			// Faire des tests de perf
			var rq = from Inscription i in this.List()
					 where i.Groupe.Id == pGroupe.Id
					 select i;

			return rq.ToList();
		}
	}
}
