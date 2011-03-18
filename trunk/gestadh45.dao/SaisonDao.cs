using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class SaisonDao
	{
		private static SaisonDao Instance;

		private SaisonDao() {
		}

		public void Create(Saison pSaison) {
			Instance.Context.Saisons.AddObject(pSaison);
			Instance.Context.SaveChanges();
		}

		public void Delete(Saison pSaison) {
			Instance.Context.Attach(pSaison);
			Instance.Context.DeleteObject(pSaison);
			Instance.Context.SaveChanges();
		}

		public bool Exist(Saison pSaison) {
			return ((from s in Instance.Context.Saisons
					 where ((s.AnneeDebut <= pSaison.AnneeDebut) && (pSaison.AnneeDebut < s.AnneeFin)) || ((s.AnneeDebut < pSaison.AnneeFin) && (pSaison.AnneeFin <= s.AnneeFin))
					 select s).Count<Saison>() > 0);
		}

		public static SaisonDao GetInstance(Entities pContexte) {
			if (Instance == null) {
				Instance = new SaisonDao();
			}
			Instance.Context = pContexte;
			return Instance;
		}

		public bool IsUsed(Saison pSaison) {
			return ((from g in Instance.Context.Groupes
					 where g.ID_Saison == pSaison.ID
					 select g).Count<Groupe>() > 0);
		}

		public List<Saison> List() {
			return (from s in Instance.Context.Saisons
					orderby s.AnneeDebut
					select s).ToList<Saison>();
		}

		public Saison Read(int pSaisonId) {
			return (from s in Instance.Context.Saisons
					where s.ID == pSaisonId
					select s).First<Saison>();
		}

		public Saison ReadSaisonCourante() {
			return (from s in Instance.Context.Saisons
					where s.EstSaisonCourante == 1L
					select s).First<Saison>();
		}

		public void Refresh(Saison pSaison) {
			this.Context.Refresh(RefreshMode.StoreWins, pSaison);
		}

		public void Update(Saison pSaison) {
			pSaison.SetAllModified<Saison>(Instance.Context);
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
