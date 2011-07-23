using System.Collections.Generic;
using gestadh45.model;

namespace gestadh45.dao
{
	public interface IVilleDao : IDao<Ville>
	{
		Ville Create(Ville ville);
		Ville Read(int id);
		Ville Update(Ville ville);

		List<Ville> List();
		bool Exists(Ville ville);
		bool IsUsed(Ville ville);
	}
}
