using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using NLog;

namespace gestadh45.dal
{
	public class Repository<TEntity> where TEntity : class, new()
	{
		private DbContext _entities;

		private static Logger logger = LogManager.GetCurrentClassLogger();

		public Repository(DbContext context) {
			logger.Debug(string.Format("New Repository<{0}>", typeof(TEntity).ToString()));
			this._entities = context;
		}

		public ICollection<TEntity> GetAll() {
			logger.Debug(string.Format("Repository<{0}>.GetAll()", typeof(TEntity).ToString()));
			return this._entities.Set<TEntity>().ToList();
		}

		public TEntity GetByKey(params object[] keyValues) {
			logger.Debug(string.Format("Repository<{0}>.GetByKey()", typeof(TEntity).ToString()));
			return this._entities.Set<TEntity>().Find(keyValues);
		}

		public TEntity GetFirst() {
			logger.Debug(string.Format("Repository<{0}>.GetFirst()", typeof(TEntity).ToString()));
			return this._entities.Set<TEntity>().FirstOrDefault();
		}

		public void Add(TEntity entity) {
			logger.Debug(string.Format("Repository<{0}>.Add({1})", typeof(TEntity).ToString(), entity.ToString()));
			this._entities.Set<TEntity>().Add(entity);
		}

		public void Delete(TEntity entity) {
			logger.Debug(string.Format("Repository<{0}>.Delete({1})", typeof(TEntity).ToString(), entity.ToString()));
			this._entities.Set<TEntity>().Remove(entity);
		}

		public void Edit(TEntity entity) {
			logger.Debug(string.Format("Repository<{0}>.Edit({1})", typeof(TEntity).ToString(), entity.ToString()));
			this._entities.Entry(entity).State = System.Data.EntityState.Modified;
		}

		public void Reload(TEntity entity) {
			logger.Debug(string.Format("Repository<{0}>.Reload({1})", typeof(TEntity).ToString(), entity.ToString()));
			this._entities.Entry<TEntity>(entity).Reload();
		}

		public void Save() {
			logger.Debug(string.Format("Repository<{0}>.Save()", typeof(TEntity).ToString()));
			try {
				this._entities.SaveChanges();
			}
			catch (DbEntityValidationException dbEx) {
				#if DEBUG
				foreach (var validationErrors in dbEx.EntityValidationErrors) {
					foreach (var validationError in validationErrors.ValidationErrors) {
						logger.Error(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
					}
				}
				#else
				throw;
				#endif
			}
		}
	}
}
