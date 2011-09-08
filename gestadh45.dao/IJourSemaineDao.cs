using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IJourSemaineDao : IDao<JourSemaine>
	{
		Sexe Read(int id);
		IEnumerable<JourSemaine> List();
	}
}
