using System.Collections.Generic;
using System.Linq;
using gestadh45.model;

namespace gestadh45.dao
{
	public class JourSemaineDao : EntityDao<JourSemaine>, IJourSemaineDao
	{

		public Sexe Read(int id) {
			return (from s in Context.Sexes
					where s.ID == id
					select s).First();
		}

		public List<JourSemaine> List() {
			return (from j in Context.JourSemaines
					orderby j.Numero ascending
					select j).ToList();
		}
	}
}
