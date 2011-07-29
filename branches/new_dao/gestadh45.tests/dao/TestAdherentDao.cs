using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestAdherentDao
	{
		public static void InfosConnection(string pFilePath) {
			AdherentDao dao = new AdherentDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(AdherentDao).ToString(), pId));

			try {
				IAdherentDao dao = DaoFactory.GetAdherentDao(pFilePath);
				Adherent s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(
						string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}", 
							s.Id, 
							s.Nom, 
							s.Prenom, 
							s.DateNaissance.ToShortDateString(),
							s.DateCreation.ToShortDateString(),
							s.DateModification.ToShortDateString(),
							s.Commentaire, 
							s.Sexe.ToString(),
							s.Contact.Id,
							s.Contact.Telephone1,
							s.Contact.Telephone2,
							s.Contact.Telephone3,
							s.Contact.Mail1,
							s.Contact.Mail2,
							s.Contact.Mail3,
							s.Contact.SiteWeb,
							s.Adresse.Id,
							s.Adresse.Libelle,
							s.Adresse.Ville.Id,
							s.Adresse.Ville.CodePostal,
							s.Adresse.Ville.Libelle
						)
					);
				}
				else {
					Console.WriteLine(string.Format("Aucun résultat pour ID = {0}", pId));
				}
			}
			catch (Exception ex) {
				Console.WriteLine("Exception : " + ex.ToString());
				Console.WriteLine(ex.Message);
				Console.WriteLine();
			}

			Console.WriteLine("Fin du test.");
			Console.WriteLine();
		}

		public static void TestList(string pFilePath) {
			Console.WriteLine(string.Format("Test de {0}.List : ", typeof(AdherentDao).ToString()));

			try {
				IAdherentDao dao = DaoFactory.GetAdherentDao(pFilePath);
				List<Adherent> list = dao.List();

				if (list.Count != 0) {
					foreach (Adherent s in list) {
						Console.WriteLine(
						string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}",
							s.Id,
							s.Nom,
							s.Prenom,
							s.DateNaissance.ToShortDateString(),
							s.DateCreation.ToShortDateString(),
							s.DateModification.ToShortDateString(),
							s.Commentaire,
							s.Sexe.ToString(),
							s.Contact.Id,
							s.Contact.Telephone1,
							s.Contact.Telephone2,
							s.Contact.Telephone3,
							s.Contact.Mail1,
							s.Contact.Mail2,
							s.Contact.Mail3,
							s.Contact.SiteWeb,
							s.Adresse.Id,
							s.Adresse.Libelle,
							s.Adresse.Ville.Id,
							s.Adresse.Ville.CodePostal,
							s.Adresse.Ville.Libelle
						)
					);
					}
				}
				else {
					Console.WriteLine("Aucun résultat.");
				}

			}
			catch (Exception ex) {
				Console.WriteLine("Exception : " + ex.ToString());
				Console.WriteLine(ex.Message);
				Console.WriteLine();
			}

			Console.WriteLine("Fin du test.");
			Console.WriteLine();
		}
	}
}
