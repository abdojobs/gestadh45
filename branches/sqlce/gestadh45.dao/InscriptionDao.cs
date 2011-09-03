using System.Collections.Generic;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class InscriptionDao : EntityDao<Inscription>, IInscriptionDao
	{
		public Inscription Create(Inscription inscription) {
			return this.Save(inscription);
		}

		public Inscription Read(int id) {
			return (from i in Context.Inscriptions
					where i.ID == id
					select i).First();
		}

		public Inscription Update(Inscription inscription) {
			inscription.SetAllModified(Context);
			this.SaveChanges();
			return inscription;
		}

		public List<Inscription> List() {
			return (from i in Context.Inscriptions
					orderby
						i.Adherent.Nom,
						i.Adherent.Prenom
					select i).ToList<Inscription>();
		}

		public List<Inscription> ListSaisonCourante() {
			return (from i in Context.Inscriptions
					where i.Groupe.Saison.EstSaisonCourante == 1
					orderby
						i.Adherent.Nom,
						i.Adherent.Prenom
					select i).ToList<Inscription>();
		}

		public bool Exists(Inscription inscription) {
			return ((from i in Context.Inscriptions
					 where (i.ID_Adherent == inscription.ID_Adherent) 
					 && (i.Groupe.ID_Saison == inscription.Groupe.ID_Saison)
					 select i).Count<Inscription>() > 0);
		}
	}
}
