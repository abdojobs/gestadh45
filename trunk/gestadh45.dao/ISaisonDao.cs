using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface ISaisonDao : IDao<Saison>
	{
		Saison Create(Saison saison);
		Saison Read(int id);
		Saison Update(Saison saison);

		List<Saison> List();
		Saison ReadSaisonCourante();
		bool Exists(Saison saison);
		bool IsUsed(Saison saison);
	}
}
