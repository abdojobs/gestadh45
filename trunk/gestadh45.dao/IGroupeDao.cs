using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IGroupeDao : IDao<Groupe>
	{
		Groupe Create(Groupe groupe);
		Groupe Read(int id);
		Groupe Update(Groupe groupe);

		IList<Groupe> List();
		IList<Groupe> ListSaisonCourante();
		bool Exists(Groupe groupe);
		bool IsUsed(Groupe groupe);
	}
}
