using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface ISexeDao : IDao<Sexe>
	{
		Sexe Read(int id);
		IEnumerable<Sexe> List();
	}
}
