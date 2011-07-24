using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestGroupeDao
	{
		public static void InfosConnection(string pFilePath) {
			GroupeDao dao = new GroupeDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(GroupeDao).ToString(), pId));

			try {
				GroupeDao dao = new GroupeDao(pFilePath);
				Groupe s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", s.Id, s.Libelle, s.JourSemaine, s.NbPlaces.ToString(), s.Commentaire, s.Saison, s.HeureDebut.ToShortTimeString(), s.HeureFin.ToShortTimeString()));
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
			Console.WriteLine(string.Format("Test de {0}.List : ", typeof(GroupeDao).ToString()));

			try {
				GroupeDao dao = new GroupeDao(pFilePath);
				List<Groupe> list = dao.List();

				if (list.Count != 0) {
					foreach (Groupe s in list) {
						Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", s.Id, s.Libelle, s.JourSemaine, s.NbPlaces.ToString(), s.Commentaire, s.Saison, s.HeureDebut.ToShortTimeString(), s.HeureFin.ToShortTimeString()));
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
