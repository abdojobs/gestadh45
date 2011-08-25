
namespace gestadh45.dao
{
	public static class DaoFactory
	{
		public static IAdherentDao GetAdherentDao(string pFilePath) {
			return new AdherentDao(pFilePath);
		}

		public static IGroupeDao GetGroupeDao(string pFilePath) {
			return new GroupeDao(pFilePath);
		}

		public static IInfosClubDao GetInfosClubDao(string pFilePath) {
			return new InfosClubDao(pFilePath);
		}

		public static IInscriptionDao GetInscriptionDao(string pFilePath) {
			return new InscriptionDao(pFilePath);
		}

		public static IJourSemaineDao GetJourSemaineDao(string pFilePath) {
			return new JourSemaineDao(pFilePath);
		}

		public static ISaisonDao GetSaisonDao(string pFilePath) {
			return new SaisonDao(pFilePath);
		}

		public static ISexeDao GetSexeDao(string pFilePath) {
			return new SexeDao(pFilePath);
		}

		public static IStatutInscriptionDao GetStatutInscriptionDao(string pFilePath) {
			return new StatutInscriptionDao(pFilePath);
		}

		public static IVilleDao GetVilleDao(string pFilePath) {
			return new VilleDao(pFilePath);
		}

		public static IParamsApplicationDao GetParamsApplicationDao(string pFilePath) {
			return new ParamsApplicationDao(pFilePath);
		}
	}
}
