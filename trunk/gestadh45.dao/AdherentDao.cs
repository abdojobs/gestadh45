using System.Collections.Generic;
using System.Linq;
using gestadh45.dal;
using System;

namespace gestadh45.dao
{
	public class AdherentDao : EntityDao<Adherent>, IAdherentDao
	{
		public Adherent Create(Adherent adherent) {
			adherent.DateCreation = DateTime.Now;
			adherent.DateModification = DateTime.Now;

			return this.Save(adherent);
		}

		public Adherent Read(int id) {
			return (from a in Context.Adherents
					where a.ID == id
					select a).First();
		}

		public Adherent Update(Adherent adherent) {
			adherent.DateModification = DateTime.Now;

			adherent.SetAllModified(Context);
			this.SaveChanges();
			return adherent;
		}

		public IList<Adherent> List() {
			return (from a in Context.Adherents
					orderby
						a.Nom ascending,
						a.Prenom ascending
					select a).ToList();
		}

		public bool Exists(Adherent adherent) {
			return ((from a in Context.Adherents
					 where (a.Nom.ToUpper().Equals(adherent.Nom.ToUpper()) 
					 && a.Prenom.ToUpper().Equals(adherent.Prenom.ToUpper()))
					 && a.DateNaissance.Equals(adherent.DateNaissance)
					 select a).Count<Adherent>() > 0);
		}

		public bool IsUsed(Adherent adherent) {
			return ((from i in Context.Inscriptions
					 where i.ID_Adherent == adherent.ID
					 select i).Count<Inscription>() > 0);
		}
	}
}
