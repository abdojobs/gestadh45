using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestJourSemaineDao
	{
		public static void InfosConnection(string pFilePath) {
			JourSemaineDao dao = new JourSemaineDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(JourSemaineDao).ToString(), pId));

			try {
				JourSemaineDao dao = new JourSemaineDao(pFilePath);
				JourSemaine s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.Numero, s.Libelle));
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
			Console.WriteLine(string.Format("Test de {0}.List : ", typeof(JourSemaineDao).ToString()));

			try {
				JourSemaineDao dao = new JourSemaineDao(pFilePath);
				List<JourSemaine> list = dao.List();

				if (list.Count != 0) {
					foreach (JourSemaine s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.Numero, s.Libelle));
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
