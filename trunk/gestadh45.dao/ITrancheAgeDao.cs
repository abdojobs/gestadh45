using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface ITrancheAgeDao : IDao<TrancheAge>
	{
		TrancheAge Create(TrancheAge trancheAge);
		TrancheAge Read(int id);
		TrancheAge Update(TrancheAge trancheAge);

		IList<TrancheAge> List();

		/// <summary>
		/// Vérifie si la tranche d'âge existe déjà (il suffit que l'AgeInf ou l'AgeSup soit déjà compris dans une tranche)
		/// </summary>
		/// <param name="trancheAge"></param>
		/// <returns></returns>
		bool Exists(TrancheAge trancheAge);

		/// <summary>
		/// Ne devrait jamais être appellé
		/// </summary>
		/// <param name="trancheAge"></param>
		/// <returns>Toujorus True</returns>
		bool IsUsed(TrancheAge trancheAge);
	}
}
