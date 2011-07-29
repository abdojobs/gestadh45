using gestadh45.model;

namespace gestadh45.dao
{
	public interface IInfosClubDao
	{
		void Update(InfosClub pDonnee);
		InfosClub Read(int pId);
	}
}
