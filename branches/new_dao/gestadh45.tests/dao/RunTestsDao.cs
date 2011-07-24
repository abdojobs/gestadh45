using System;
using gestadh45.dao;

namespace gestadh45.tests.dao
{
	public class RunTestsDao
	{
		private string _filePath;
		
		public RunTestsDao(string pFilePath) {
			this._filePath = pFilePath;
		}

		public void Launch() {
			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(SexeDao).ToString()));
			Console.WriteLine();
			TestSexeDao.InfosConnection(this._filePath);
			TestSexeDao.TestRead(this._filePath, 0);
			TestSexeDao.TestRead(this._filePath, 1);
			TestSexeDao.TestList(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(SexeDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();

			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(JourSemaineDao).ToString()));
			Console.WriteLine();
			TestJourSemaineDao.InfosConnection(this._filePath);
			TestJourSemaineDao.TestRead(this._filePath, 0);
			TestJourSemaineDao.TestRead(this._filePath, 1);
			TestJourSemaineDao.TestList(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(JourSemaineDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();

			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(StatutInscriptionDao).ToString()));
			Console.WriteLine();
			TestStatutInscriptionDao.InfosConnection(this._filePath);
			TestStatutInscriptionDao.TestRead(this._filePath, 0);
			TestStatutInscriptionDao.TestRead(this._filePath, 1);
			TestStatutInscriptionDao.TestList(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(StatutInscriptionDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();

			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(InfosClubDao).ToString()));
			Console.WriteLine();
			TestInfosClubDao.InfosConnection(this._filePath);
			TestInfosClubDao.TestRead(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(InfosClubDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();

			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(SaisonDao).ToString()));
			Console.WriteLine();
			TestSaisonDao.InfosConnection(this._filePath);
			TestSaisonDao.TestRead(this._filePath, 0);
			TestSaisonDao.TestRead(this._filePath, 4);
			TestSaisonDao.TestList(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(SaisonDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();

			Console.WriteLine("####################################################################################################");
			Console.WriteLine(string.Format("Test de {0}...", typeof(VilleDao).ToString()));
			Console.WriteLine();
			TestVilleDao.InfosConnection(this._filePath);
			TestVilleDao.TestRead(this._filePath, 0);
			TestVilleDao.TestRead(this._filePath, 28);
			TestVilleDao.TestList(this._filePath);
			Console.WriteLine(string.Format("Fin du test de {0}...", typeof(VilleDao).ToString()));
			Console.WriteLine("####################################################################################################");

			Console.WriteLine();
		}
	}
}
