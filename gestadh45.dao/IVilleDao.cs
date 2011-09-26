﻿using System.Collections.Generic;
using gestadh45.dal;

namespace gestadh45.dao
{
	public interface IVilleDao : IDao<Ville>
	{
		Ville Create(Ville ville);
		Ville Read(int id);
		Ville Update(Ville ville);

		IList<Ville> List();
		bool Exists(Ville ville);
		bool IsUsed(Ville ville);
	}
}