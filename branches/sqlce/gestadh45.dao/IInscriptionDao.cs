using System.Collections.Generic;
using gestadh45.Model;

namespace gestadh45.dao
{
	public interface IInscriptionDao : IDao<Inscription>
	{
		Inscription Create(Inscription inscription);
		Inscription Read(int id);
		Inscription Update(Inscription inscription);

		List<Inscription> List();
		List<Inscription> ListSaisonCourante();
		bool Exists(Inscription inscription);
	}
}
