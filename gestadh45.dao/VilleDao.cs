using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.model;
using System.Data.SQLite;

namespace gestadh45.dao
{
	public class VilleDao : DaoBase, IDao<Ville>
	{
		public VilleDao(string pFilePath) : base(pFilePath) { }
		
		public int Create(Ville pDonnee) {
			this.Connection.Open();

			var paramCodePostal = new SQLiteParameter("@CodePostal", System.Data.DbType.String) { Value = pDonnee.CodePostal };
			var paramLibelle = new SQLiteParameter("@Libelle", System.Data.DbType.String) { Value = pDonnee.Libelle };

			var cmdInsert = new SQLiteCommand("INSERT INTO Ville(CodePostal, Libelle) Values('@CodePostal', '@Libelle');", this.Connection);
			cmdInsert.Parameters.Add(paramCodePostal);
			cmdInsert.Parameters.Add(paramLibelle);

			try {
				cmdInsert.ExecuteNonQuery();
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
			throw new NotImplementedException();
		}

		public void Delete(Ville pDonnee) {
			throw new NotImplementedException();
		}

		public bool Exists(Ville pDonnee) {
			throw new NotImplementedException();
		}

		public bool IsUsed(Ville pDonnee) {
			throw new NotImplementedException();
		}

		public Ville Read(int pId) {
			throw new NotImplementedException();
		}

		public List<Ville> List() {
			throw new NotImplementedException();
		}
	}
}
