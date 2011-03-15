using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace gestadh45.Model
{
	public static class EFExtension
	{
		public static void SetAllModified<T>(this T entity, ObjectContext context) where T : IEntityWithKey {
			var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
			var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select
			  (pn => pn.FieldType.Name);
			foreach (var propName in propertyNameList)
				stateEntry.SetModifiedProperty(propName);
		}
	}
}
