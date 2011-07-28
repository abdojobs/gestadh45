using System.Collections.Generic;

namespace gestadh45.dao
{
	public interface IReadOnlyDao<T>
	{
		/// <summary>
		/// Récupère une entité via son identifiant
		/// </summary>
		/// <param name="pId">Identifiant de l'entité</param>
		/// <returns>Entité (null si non trouvé)</returns>
		T Read(int pId);

		/// <summary>
		/// Récupère l'ensemble des entités
		/// </summary>
		/// <returns>Liste d'entités</returns>
		List<T> List();

		/// <summary>
		/// Vérifie l'existence d'une entité dans la base selon les règles spécifiques à l'entité (voir l'implémentation)
		/// </summary>
		/// <param name="pDonnee">Entité à vérifier</param>
		/// <returns>True si l'entité existe, False sinon</returns>
		bool Exists(T pDonnee);
	}
}
