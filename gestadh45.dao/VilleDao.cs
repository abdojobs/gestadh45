using System.Collections.Generic;
using System.Linq;
using gestadh45.model;

namespace gestadh45.dao
{
	public class VilleDao : EntityDao<Ville>, IVilleDao
	{
		public Ville Create(Ville ville) {
			return this.Save(ville);
		}

		public Ville Read(int id) {
			return (from v in Context.Villes
				   where v.ID == id
				   select v).First();
		}

		public Ville Update(Ville ville) {
			ville.SetAllModified(Context);
			this.SaveChanges();
			return ville;
		}

		public List<Ville> List() {
			return Context.Villes.ToList();
		}

		public bool Exists(Ville ville) {
			return (from v in Context.Villes
						where v.CodePostal.Equals(ville.CodePostal)
						&& v.Libelle.Equals(ville.Libelle)
						select v).Count() > 0;
		}

		public bool IsUsed(Ville ville) {
			return ((from a in Context.Adresses
					 where a.ID_Ville == ville.ID
					 select a).Count() > 0);
		}
	}
}
