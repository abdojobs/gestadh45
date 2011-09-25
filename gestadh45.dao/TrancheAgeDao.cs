using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.dao
{
	public class TrancheAgeDao : EntityDao<TrancheAge>, ITrancheAgeDao
	{
		#region ITrancheAgeDao Membres

		public TrancheAge Create(TrancheAge trancheAge) {
			return this.Save(trancheAge);
		}

		public TrancheAge Read(int id) {
			return (from t in Context.TrancheAges
					where t.ID == id
					select t).First();
		}

		public TrancheAge Update(TrancheAge trancheAge) {
			trancheAge.SetAllModified(Context);
			this.SaveChanges();
			return trancheAge;
		}

		public IList<TrancheAge> List() {
			return (from t in Context.TrancheAges
					orderby t.AgeInf ascending
					select t).ToList();
		}

		public bool Exists(TrancheAge trancheAge) {
			return ((from t in Context.TrancheAges
					 where 
						(trancheAge.AgeInf >= t.AgeInf && trancheAge.AgeInf <= t.AgeSup)
						||
						(trancheAge.AgeSup >= t.AgeInf && trancheAge.AgeSup <= t.AgeSup)
					 select t).Count() > 0);
		}

		public bool IsUsed(TrancheAge trancheAge) {
			return false;
		}

		#endregion
	}
}
