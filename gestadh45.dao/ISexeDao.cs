using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface ISexeDao : IDao<Sexe>
	{
		Sexe Read(int id);
		List<Sexe> List();
	}
}
