using System;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestInfosClubDao
	{
		public static void InfosConnection(string pFilePath) {
			InfosClubDao dao = new InfosClubDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath) {
			Console.WriteLine(string.Format("Test de {0}.Read: ", typeof(InfosClubDao).ToString()));

			try {
				InfosClubDao dao = new InfosClubDao(pFilePath);
				InfosClub s = dao.Read(0);
				if (s != null) {
					Console.WriteLine(s.Id);
					Console.WriteLine(s.Nom);
					Console.WriteLine(s.Numero);
					Console.WriteLine(s.Siren);
					Console.WriteLine(s.NIC);
					Console.WriteLine(s.Adresse);
					Console.WriteLine(s.Contact.Telephone1);
					Console.WriteLine(s.Contact.Mail1);
					Console.WriteLine(s.Contact.SiteWeb);
				}
				else {
					Console.WriteLine(string.Format("Aucun résultat"));
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
