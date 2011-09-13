using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;
using System;

namespace gestadh45.dao
{
	public class InscriptionDao : EntityDao<Inscription>, IInscriptionDao
	{
		public Inscription Create(Inscription inscription) {
			inscription.DateCreation = DateTime.Now;
			inscription.DateModification = DateTime.Now;

			return this.Save(inscription);
		}

		public Inscription Read(int id) {
			return (from i in Context.Inscriptions
					where i.ID == id
					select i).First();
		}

		public Inscription Update(Inscription inscription) {
			inscription.DateModification = DateTime.Now;

			inscription.SetAllModified(Context);
			this.SaveChanges();
			return inscription;
		}

		public IList<Inscription> List() {
			return (from i in Context.Inscriptions
					orderby
						i.Adherent.Nom,
						i.Adherent.Prenom
					select i).ToList<Inscription>();
		}

		public IList<Inscription> ListSaisonCourante() {
			return (from i in Context.Inscriptions
					where i.Groupe.Saison.EstSaisonCourante
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
