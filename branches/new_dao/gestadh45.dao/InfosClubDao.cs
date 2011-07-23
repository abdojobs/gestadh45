using System.Linq;
using gestadh45.model;

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
			this.SaveChanges();
			return infosClub;
		}
	}
}
