using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gestadh45.dao;
using gestadh45.model;

namespace gestadh45.tests.dao
{
	public static class TestInscription
	{
		public static void InfosConnection(string pFilePath) {
			InscriptionDao dao = new InscriptionDao(pFilePath);
			Console.WriteLine("Dao : " + dao.ToString());
			Console.WriteLine();
		}

		public static void TestRead(string pFilePath, int pId) {
			Console.WriteLine(string.Format("Test de {0}.Read (ID = {1}) : ", typeof(InscriptionDao).ToString(), pId));

			try {
				IDao<Inscription> dao = new InscriptionDao(pFilePath);
				Inscription s = dao.Read(pId);
				if (s != null) {
					Console.WriteLine(
						string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
							s.Id,
							s.Adherent.ToString(),
							s.Groupe.ToString(),
							s.CertificatMedicalRemis.ToString(),
							s.Cotisation.ToString(),
							s.Commentaire,
							s.DateCreation,
							s.DateModification,
							s.StatutInscription.ToString()
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
			Console.WriteLine(string.Format("Test de {0}.List : ", typeof(InscriptionDao).ToString()));

			try {
				InscriptionDao dao = new InscriptionDao(pFilePath);
				List<Inscription> list = dao.List();

				if (list.Count != 0) {
					foreach (Inscription s in list) {
						Console.WriteLine(
							string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
								s.Id,
								s.Adherent.ToString(),
								s.Groupe.ToString(),
								s.CertificatMedicalRemis.ToString(),
								s.Cotisation.ToString(),
								s.Commentaire,
								s.DateCreation,
								s.DateModification,
								s.StatutInscription.ToString()
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
