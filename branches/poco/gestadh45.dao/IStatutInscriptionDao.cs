using System.Collections.Generic;
using gestadh45.Model;

namespace gestadh45.dao
{
	public interface IStatutInscriptionDao : IDao<StatutInscription>
	{
		StatutInscription Read(int id);
		List<StatutInscription> List();
	}
}
