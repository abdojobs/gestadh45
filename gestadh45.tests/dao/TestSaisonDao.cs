using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestSaisonDao
	{
		public static void InfosConnection(string pFilePath) {
			SaisonDao dao = new SaisonDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(SaisonDao).ToString(), pId));

			try {
				SaisonDao dao = new SaisonDao(pFilePath);
				Saison s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", s.Id, s.EstSaisonCourante.ToString(), s.AnneeDebut.ToString(), s.AnneeFin.ToString()));
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
			Console.WriteLine(string.Format("Test de SexeDao.List : ", typeof(SaisonDao).ToString()));

			try {
				SaisonDao dao = new SaisonDao(pFilePath);
				List<Saison> list = dao.List();

				if (list.Count != 0) {
					foreach (Saison s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", s.Id, s.EstSaisonCourante.ToString(), s.AnneeDebut.ToString(), s.AnneeFin.ToString()));
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
