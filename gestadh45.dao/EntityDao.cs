using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using gestadh45.dal;

namespace gestadh45.dao
{
	public abstract class EntityDao<TEntity> : IDao<TEntity> where TEntity : EntityObject, new()
	{
		#region Helper methods
		protected Entities Context {
			get {
				return ObjectContextManager.Context;
			}
		}

		private string entitySetName;
		protected string EntitySetName {
			get {
				if (String.IsNullOrEmpty(entitySetName)) {
					entitySetName = Context.GetEntitySetName(typeof(TEntity).Name);
				}
				return entitySetName;
			}
		}
		#endregion

		#region IDao<TEntity> Members
		public TEntity Save(TEntity entity) {
			Context.AddObject(EntitySetName, entity);
			SaveChanges();
			return entity;
		}

		public void Delete(TEntity entity) {
			Context.DeleteObject(entity);
			SaveChanges();
		}

		public void Refresh(TEntity entity) {
			Context.Refresh(RefreshMode.StoreWins, entity);
		}

		public void SaveChanges() {
			Context.SaveChanges();
		}
		#endregion
	}
}
