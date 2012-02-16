using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace gestadh45.dal
{
	public class Repository<TEntity, TContext> where TEntity : class where TContext : DbContext, new()
	{
		private TContext _entities = new TContext();

		#region IRepository<T> Membres

		public IEnumerable<TEntity> GetAll() {
			return this._entities.Set<TEntity>().ToList();
		}

		public TEntity GetById(int id) {
			return this._entities.Set<TEntity>().Find(id);
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

		private bool disposed = false;

		protected virtual void Dispose(bool disposing) {
			if (!this.disposed) {
				if (disposing) {
					this._entities.Dispose();
				}
			}

			this.disposed = true;
		}

		#region IDisposable Membres

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
