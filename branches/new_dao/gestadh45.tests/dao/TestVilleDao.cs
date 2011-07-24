using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestVilleDao
	{
		public static void InfosConnection(string pFilePath) {
			VilleDao dao = new VilleDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(VilleDao).ToString(), pId));

			try {
				VilleDao dao = new VilleDao(pFilePath);
				Ville s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.CodePostal, s.Libelle));
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
			Console.WriteLine(string.Format("Test de {0}.List : ", typeof(VilleDao).ToString()));

			try {
				VilleDao dao = new VilleDao(pFilePath);
				List<Ville> list = dao.List();

				if (list.Count != 0) {
					foreach (Ville s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.CodePostal, s.Libelle));
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
