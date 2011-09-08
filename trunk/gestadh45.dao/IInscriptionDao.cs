using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IInscriptionDao : IDao<Inscription>
	{
		Inscription Create(Inscription inscription);
		Inscription Read(int id);
		Inscription Update(Inscription inscription);

		IList<Inscription> List();
		IList<Inscription> ListSaisonCourante();
		bool Exists(Inscription inscription);
	}
}
