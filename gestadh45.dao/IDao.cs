
using System.Collections.Generic;
namespace gestadh45.dao
{
	public interface IDao<TEntity>
	{
		TEntity Save(TEntity entity);
		void Delete(TEntity entity);
		void Refresh(TEntity entity);
		void SaveChanges();
	}
}
