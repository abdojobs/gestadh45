using System.Collections.Generic;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class SexeDao : EntityDao<Sexe>, ISexeDao
	{
		public Sexe Read(int id) {
			return (from s in Context.Sexes
					where s.ID == id
					select s).First();
		}

		public List<Sexe> List() {
			return (from s in Context.Sexes
					orderby s.LibelleCourt descending
					select s).ToList();
		}
	}
}
