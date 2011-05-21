
namespace gestadh45.dao
{
	public interface IDaoFactory
	{
		IVilleDao GetVilleDao();
		ISexeDao GetSexeDao();
		IJourSemaineDao GetJourSemaineDao();
		ISaisonDao GetSaisonDao();
		IInscriptionDao GetInscriptionDao();
		IInfosClubDao GetInfosClubDao();
		IGroupeDao GetGroupeDao();
		IAdherentDao GetAdherentDao();
		IContactDao GetContactDao();
		IAdresseDao GetAdresseDao();
	}
}
