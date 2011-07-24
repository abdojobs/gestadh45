
namespace gestadh45.dao
{
	public interface IDao<T> : IReadOnlyDao<T>
	{
		/// <summary>
		/// Insère une nouvelle entité dans la base
		/// </summary>
		/// <param name="pDonnee">Entité à insérer</param>
		/// <returns>ID de l'entité insérée</returns>
		int Create(T pDonnee);

		/// <summary>
		/// Met à jour une entité dans la base
		/// </summary>
		/// <param name="pDonnee">Entité à mettre à jour</param>
		void Update(T pDonnee);

		/// <summary>
		/// Supprime une entité de la base
		/// </summary>
		/// <param name="pDonnee">Entité à supprimer</param>
		void Delete(T pDonnee);
		
		/// <summary>
		/// Vérifie l'existence d'une entité dans la base
		/// </summary>
		/// <param name="pDonnee">Entité à vérifier</param>
		/// <returns>True si l'entité existe, False sinon</returns>
		bool Exists(T pDonnee);

		/// <summary>
		/// Vérifie si l'entité est utilisée par une autre
		/// </summary>
		/// <param name="pDonnee">True si l'entité est utilisée, False sinon</param>
		/// <returns></returns>
		bool IsUsed(T pDonnee);
	}
}
