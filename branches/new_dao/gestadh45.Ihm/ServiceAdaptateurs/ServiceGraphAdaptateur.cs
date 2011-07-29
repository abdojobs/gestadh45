using gestadh45.dao;
using gestadh45.service.Graphs;

namespace gestadh45.Ihm.ServiceAdaptateurs
{
	public static class ServiceGraphAdaptateur
	{
		public static DonneesGraph GetDonnees(string pFilePath) {
			DonneesGraph result = new DonneesGraph();

			IInfosClubDao daoInfosClub = DaoFactory.GetInfosClubDao(pFilePath);
			IInscriptionDao daoInscription = DaoFactory.GetInscriptionDao(pFilePath);
			IGroupeDao daoGroupe = DaoFactory.GetGroupeDao(pFilePath);
			ISexeDao daoSexe = DaoFactory.GetSexeDao(pFilePath);

			result.InfosClub = daoInfosClub.Read();
			result.GroupesSaisonCourante = daoGroupe.ListSaisonCourante();
			result.Sexes = daoSexe.List();
			result.InscriptionsSaisonCourante = daoInscription.ListSaisonCourante();

			return result;
		}
	}
}
