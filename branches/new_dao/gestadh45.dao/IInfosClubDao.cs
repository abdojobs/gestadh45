using gestadh45.model;

namespace gestadh45.dao
{
	public interface IInfosClubDao : IDao<InfosClub>
	{
		InfosClub Read();
		InfosClub Update(InfosClub infosClub);
	}
}
