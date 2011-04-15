using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class AdherentDao
	{
		private static AdherentDao Instance;

		private AdherentDao()
		{
		}

		public void Create(Adherent pAdherent)
		{
			pAdherent.DateCreation = DateTime.Now;
			pAdherent.DateModification = DateTime.Now;
			Instance.Context.Adherents.AddObject(pAdherent);
			Instance.Context.SaveChanges();
		}

		public void Delete(Adherent pAdherent)
		{
			Instance.Context.Attach(pAdherent);
			Instance.Context.DeleteObject(pAdherent.Adresse);
			Instance.Context.DeleteObject(pAdherent.Contact);
			Instance.Context.DeleteObject(pAdherent);
			Instance.Context.SaveChanges();
		}

		public bool Exist(Adherent pAdherent)
		{
			return ((from a in Instance.Context.Adherents
				where (a.Nom.ToUpper().Equals(pAdherent.Nom.ToUpper()) && a.Prenom.ToUpper().Equals(pAdherent.Prenom.ToUpper())) && a.DateNaissance.Equals(pAdherent.DateNaissance)
				select a).Count<Adherent>() > 0);
		}

		public static AdherentDao GetInstance(Entities pContexte)
		{
			if (Instance == null)
			{
				Instance = new AdherentDao();
			}
			Instance.Context = pContexte;
			return Instance;
		}

		public bool IsUsed(Adherent pAdherent)
		{
			return ((from i in Instance.Context.Inscriptions
				where i.ID_Adherent == pAdherent.ID
				select i).Count<Inscription>() > 0);
		}

		public List<Adherent> List()
		{
			return (from a in Instance.Context.Adherents
				orderby 
					a.Nom ascending,
					a.Prenom ascending
				select a).ToList<Adherent>();
		}

		public Adherent Read(int pAdherentId)
		{
			return (from a in Instance.Context.Adherents
				where a.ID == pAdherentId
				select a).First<Adherent>();
		}

		public void Refresh(Adherent pAdherent)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent);
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent.Adresse);
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent.Contact);
		}

		public void Update(Adherent pAdherent)
		{
			pAdherent.DateModification = DateTime.Now;
			pAdherent.SetAllModified<Adherent>(Instance.Context);
			try
			{
				Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException exception)
			{
				throw exception;
			}
		}

		private Entities Context { get; set; }
	}
}
