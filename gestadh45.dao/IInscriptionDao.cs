using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IInscriptionDao : IDao<Inscription>
	{
		List<Inscription> ListSaisonCourante();
		List<Inscription> ListGroupe(Groupe pGroupe);
	}
}
