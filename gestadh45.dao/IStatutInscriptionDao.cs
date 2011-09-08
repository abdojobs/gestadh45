using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IStatutInscriptionDao : IDao<StatutInscription>
	{
		StatutInscription Read(int id);
		List<StatutInscription> List();
	}
}
