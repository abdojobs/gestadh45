using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.dao
{
	public class StatutInscriptionDao : EntityDao<StatutInscription>, IStatutInscriptionDao
	{
		public StatutInscription Read(int id) {
			return (from s in Context.StatutInscriptions
					where s.ID == id
					select s).First();
		}

		public IEnumerable<StatutInscription> List() {
			return (from s in Context.StatutInscriptions
					orderby s.Libelle ascending
					select s).ToList();
		}
	}
}
