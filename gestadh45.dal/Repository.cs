using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace gestadh45.dal
{
	public class Repository<TEntity> where TEntity : class, new()
	{
		private DbContext _entities;

		public Repository(DbContext context) {
			this._entities = context;
		}

		#region IRepository<T> Membres

		public IEnumerable<TEntity> GetAll() {
			return this._entities.Set<TEntity>().ToList();
		}

		public TEntity GetById(int id) {
			return this._entities.Set<TEntity>().Find(id);
		}

		public TEntity GetByKey(object[] keyValues) {
			return this._entities.Set<TEntity>().Find(keyValues);
		}

		public void Add(TEntity entity) {
			this._entities.Set<TEntity>().Add(entity);
		}

		public void Delete(TEntity entity) {
			this._entities.Set<TEntity>().Remove(entity);
		}

		public void Edit(TEntity entity) {
			this._entities.Entry(entity).State = System.Data.EntityState.Modified;
		}

		public void Save() {
			this._entities.SaveChanges();
		}

		#endregion
	}
}
