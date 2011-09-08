using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;

namespace gestadh45.dao
{
	public class SaisonDao : EntityDao<Saison>, ISaisonDao
	{
		public Saison Create(Saison saison) {
			return this.Save(saison);
		}

		public Saison Read(int id) {
			return (from s in Context.Saisons
					where s.ID == id
					select s).First();
		}

		public Saison Update(Saison saison) {
			saison.SetAllModified(Context);
			this.SaveChanges();
			return saison;
		}

		public IList<Saison> List() {
			return (from s in Context.Saisons
					orderby s.AnneeDebut ascending
					select s).ToList();
		}

		public Saison ReadSaisonCourante() {
			return (from s in Context.Saisons
				   where s.EstSaisonCourante
				   select s).First();
		}

		public bool Exists(Saison saison) {
			return ((from s in Context.Saisons
					 where ((s.AnneeDebut <= saison.AnneeDebut) && (saison.AnneeDebut < s.AnneeFin))
					 || ((s.AnneeDebut < saison.AnneeFin) && (saison.AnneeFin <= s.AnneeFin))
					 select s).Count() > 0);
		}

		public bool IsUsed(Saison saison) {
			return ((from g in Context.Groupes
					 where g.ID_Saison == saison.ID
					 select g).Count<Groupe>() > 0);
		}
	}
}
