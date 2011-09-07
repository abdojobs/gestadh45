using System.Collections.Generic;
using gestadh45.Model;

namespace gestadh45.dao
{
	public interface IAdherentDao : IDao<Adherent>
	{
		Adherent Create(Adherent adherent);
		Adherent Read(int id);
		Adherent Update(Adherent adherent);

		List<Adherent> List();
		bool Exists(Adherent adherent);
		bool IsUsed(Adherent adherent);
	}
}
