
namespace gestadh45.dao
{
	public interface IDao<T> : IReadOnlyDao<T>
	{
		int Create(T pDonnee);		
		void Update(T pDonnee);
		void Delete(T pDonnee);
				
		bool Exists(T pDonnee);
		bool IsUsed(T pDonnee);
	}
}
