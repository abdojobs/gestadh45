using gestadh45.model;

namespace gestadh45.dao
{
	public interface ISaisonDao : IDao<Saison>
	{
		Saison ReadSaisonCourante();
	}
}
