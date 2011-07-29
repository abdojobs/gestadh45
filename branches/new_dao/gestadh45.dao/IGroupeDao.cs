using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IGroupeDao : IDao<Groupe>
	{
		List<Groupe> ListSaisonCourante();
	}
}
