using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestSexeDao
	{
		public static void InfosConnection(string pFilePath) {
			SexeDao dao = new SexeDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(SexeDao).ToString(), pId));

			try {
				SexeDao dao = new SexeDao(pFilePath);
				Sexe s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.LibelleCourt, s.LibelleLong));
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
			Console.WriteLine(string.Format("Test de SexeDao.List : ", typeof(SexeDao).ToString()));

			try {
				SexeDao dao = new SexeDao(pFilePath);
				List<Sexe> list = dao.List();

				if (list.Count != 0) {
					foreach (Sexe s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}", s.Id, s.LibelleCourt, s.LibelleLong));
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
