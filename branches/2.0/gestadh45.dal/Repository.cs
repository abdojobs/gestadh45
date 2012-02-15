using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace gestadh45.dal
{
	public class Repository<C, T> where T : class where C : DbContext, new()
	{
		private C _entities = new C();
		protected C Context {
			get { return this._entities; }
			set { this._entities = value; }
		}

		#region IRepository<T> Membres

		public IEnumerable<T> GetAll() {
			return this._entities.Set<T>().ToList();
		}

		public T GetById(int id) {
			return this._entities.Set<T>().Find(id);
		}

		public void Add(T entity) {
			this._entities.Set<T>().Add(entity);
		}

		public void Delete(T entity) {
			this._entities.Set<T>().Remove(entity);
		}

		public void Edit(T entity) {
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
