using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IStatutInscriptionDao : IDao<StatutInscription>
	{
		StatutInscription Read(int id);
		List<StatutInscription> List();
	}
}
