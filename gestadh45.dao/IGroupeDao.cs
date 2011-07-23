using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IGroupeDao : IDao<Groupe>
	{
		Groupe Create(Groupe groupe);
		Groupe Read(int id);
		Groupe Update(Groupe groupe);

		List<Groupe> List();
		List<Groupe> ListSaisonCourante();
		bool Exists(Groupe groupe);
		bool IsUsed(Groupe groupe);
	}
}
