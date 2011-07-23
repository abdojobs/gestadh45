using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IJourSemaineDao : IDao<JourSemaine>
	{
		Sexe Read(int id);
		List<JourSemaine> List();
	}
}
