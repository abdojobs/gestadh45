using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class InfosClubDao : EntityDao<InfosClub>, IInfosClubDao
	{
		public InfosClub Read() {
			return (from i in Context.InfosClubs
					select i).First();
		}

		public InfosClub Update(InfosClub infosClub) {
			infosClub.SetAllModified(Context);
			return this.Save(infosClub);
		}
	}
}
