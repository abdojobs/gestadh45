using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class GroupeDao
	{
		private static GroupeDao Instance;

		private GroupeDao() {
		}

		public void Create(Groupe pGroupe) {
			Instance.Context.Groupes.AddObject(pGroupe);
			Instance.Context.SaveChanges();
		}

		public void Delete(Groupe pGroupe) {
			Instance.Context.Attach(pGroupe);
			Instance.Context.DeleteObject(pGroupe);
			Instance.Context.SaveChanges();
		}

		public bool Exist(Groupe pGroupe) {
			return (from g in Instance.Context.Groupes
					 where g.Saison.ID == pGroupe.Saison.ID
						&& g.JourSemaine.ID == pGroupe.JourSemaine.ID 
						&& g.Libelle.Equals(pGroupe.Libelle)
					 select g).Count<Groupe>() > 0;
		}

		public static GroupeDao GetInstance(Entities pContexte) {
			if (Instance == null) {
				Instance = new GroupeDao();
			}
			Instance.Context = pContexte;
			return Instance;
		}

		public bool IsUsed(Groupe pGroupe) {
			return ((from i in Instance.Context.Inscriptions
					 where i.ID_Groupe == pGroupe.ID
					 select i).Count<Inscription>() > 0);
		}

		public List<Groupe> List() {
			return (from g in Instance.Context.Groupes
					orderby
						g.JourSemaine.Numero ascending,
						g.HeureDebutDT ascending
					select g).ToList<Groupe>();
		}

		public List<Groupe> ListSaisonCourante() {
			return (from g in Instance.Context.Groupes
					where g.Saison.EstSaisonCourante == 1L
					orderby
						g.JourSemaine.Numero ascending,
						g.HeureDebutDT ascending
					select g).ToList<Groupe>();
		}

		public Groupe Read(int pGroupeId) {
			return (from g in Instance.Context.Groupes
					where g.ID == pGroupeId
					select g).First<Groupe>();
		}

		public void Refresh(Groupe pGroupe) {
			this.Context.Refresh(RefreshMode.StoreWins, pGroupe);
			this.Context.Refresh(RefreshMode.StoreWins, pGroupe.Saison);
		}

		public void Update(Groupe pGroupe) {
			pGroupe.SetAllModified<Groupe>(Instance.Context);
			try {
				Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException exception) {
				throw exception;
			}
		}

		private Entities Context { get; set; }
	}
}
