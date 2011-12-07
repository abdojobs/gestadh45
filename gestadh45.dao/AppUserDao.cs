using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.dao
{
	public class AppUserDao : EntityDao<AppUser>, IAppUserDao
	{

		#region IAppUserDao Membres

		public AppUser Read(int id) {
			return (from u in Context.AppUsers
					where u.ID == id
					select u).First();
		}

		public IList<AppUser> List() {
			return (from u in Context.AppUsers
					orderby u.Login descending
					select u).ToList();
		}

		#endregion
	}
}
