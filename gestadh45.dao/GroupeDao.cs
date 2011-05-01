using System.Collections.Generic;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class GroupeDao : EntityDao<Groupe>, IGroupeDao
	{
		public Groupe Create(Groupe groupe) {
			return this.Save(groupe);
		}

		public Groupe Read(int id) {
			return (from g in Context.Groupes
					where g.ID == id
					select g).First();
		}

		public Groupe Update(Groupe groupe) {
			groupe.SetAllModified(Context);
			return this.Save(groupe);
		}

		public List<Groupe> List() {
			return (from g in Context.Groupes
					orderby
						g.JourSemaine.Numero ascending,
						g.HeureDebutDT ascending
					select g).ToList();
		}

		public List<Groupe> ListSaisonCourante() {
			return (from g in Context.Groupes
					where g.Saison.EstSaisonCourante == 1
					orderby
						g.JourSemaine.Numero ascending,
						g.HeureDebutDT ascending
					select g).ToList();
		}

		public bool Exists(Groupe groupe) {
			return (from g in Context.Groupes
					where g.Saison.ID == groupe.Saison.ID
					   && g.JourSemaine.ID == groupe.JourSemaine.ID
					   && g.Libelle.Equals(groupe.Libelle)
					select g).Count<Groupe>() > 0;
		}

		public bool IsUsed(Groupe groupe) {
			return ((from i in Context.Inscriptions
					 where i.ID_Groupe == groupe.ID
					 select i).Count<Inscription>() > 0);
		}
	}
}
