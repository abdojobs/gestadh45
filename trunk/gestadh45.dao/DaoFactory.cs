
namespace gestadh45.dao
{
	public class DaoFactory : IDaoFactory
	{
		public IVilleDao GetVilleDao() {
			return new VilleDao();
		}

		public ISexeDao GetSexeDao() {
			return new SexeDao();
		}
		
		public IJourSemaineDao GetJourSemaineDao() {
			return new JourSemaineDao();
		}

		public ISaisonDao GetSaisonDao() {
			return new SaisonDao();
		}

		public IInscriptionDao GetInscriptionDao() {
			return new InscriptionDao();
		}

		public IInfosClubDao GetInfosClubDao() {
			return new InfosClubDao();
		}

		public IGroupeDao GetGroupeDao() {
			return new GroupeDao();
		}

		public IAdherentDao GetAdherentDao() {
			return new AdherentDao();
		}

		public IStatutInscriptionDao GetStatutInscriptionDao() {
			return new StatutInscriptionDao();
		}

		public ITrancheAgeDao GetTrancheAgeDao() {
			return new TrancheAgeDao();
		}

		public IAppUserDao GetAppUserDao() {
			return new AppUserDao();
		}

	}
}
