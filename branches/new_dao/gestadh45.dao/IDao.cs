
namespace gestadh45.dao
{
	public interface IDao<T> : IReadOnlyDao<T>
	{
		T Create(T pDonnee);		
		T Update(T pDonnee);
		void Delete(T pDonnee);
				
		bool Exists(T pDonnee);
		bool IsUsed(T pDonnee);

		void Refresh(T pDonnee);
	}
}
