using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IAppUserDao : IDao<AppUser>
	{
		AppUser Read(int id);
		IList<AppUser> List();
	}
}
