using System;
using System.Collections.Generic;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestStatutInscriptionDao
	{
		public static void InfosConnection(string pFilePath) {
			StatutInscriptionDao dao = new StatutInscriptionDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(StatutInscriptionDao).ToString(), pId));

			try {
				StatutInscriptionDao dao = new StatutInscriptionDao(pFilePath);
				StatutInscription s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", s.Id, s.CodeCouleur, s.Ordre, s.Libelle));
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
			Console.WriteLine(string.Format("Test de StatutInscriptionDao.List : ", typeof(StatutInscriptionDao).ToString()));

			try {
				StatutInscriptionDao dao = new StatutInscriptionDao(pFilePath);
				List<StatutInscription> list = dao.List();

				if (list.Count != 0) {
					foreach (StatutInscription s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", s.Id, s.CodeCouleur, s.Ordre, s.Libelle));
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
