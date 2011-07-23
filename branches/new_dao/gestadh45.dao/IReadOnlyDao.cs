using System.Collections.Generic;

namespace gestadh45.dao1
{
	public interface IReadOnlyDao<T>
	{
		T Read(int pId);
		List<T> List();
	}
}
