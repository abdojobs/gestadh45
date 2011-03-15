namespace GestAdh45.Dao
{
    using GestAdh45.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class VilleDao
    {
        private static VilleDao Instance;

        private VilleDao()
        {
        }

        public void Create(Ville pVille)
        {
            Instance.Context.Villes.AddObject(pVille);
            Instance.Context.SaveChanges();
        }

        public void Delete(Ville pVille)
        {
            Instance.Context.Attach(pVille);
            Instance.Context.DeleteObject(pVille);
            Instance.Context.SaveChanges();
        }

        public bool Exist(Ville pVille)
        {
            return ((from v in Instance.Context.Villes
                where v.CodePostal.Equals(pVille.CodePostal) && v.Libelle.Equals(pVille.Libelle)
                select v).Count<Ville>() > 0);
        }

        public static VilleDao GetInstance(Entities pContexte)
        {
            if (Instance == null)
            {
                Instance = new VilleDao();
            }
            Instance.Context = pContexte;
            return Instance;
        }

        public bool IsUsed(Ville pVille)
        {
            return ((from a in Instance.Context.Adresses
                where a.ID_Ville == pVille.ID
                select a).Count<Adresse>() > 0);
        }

        public List<Ville> List()
        {
            return (from v in Instance.Context.Villes
                orderby v.Libelle
                select v).ToList<Ville>();
        }

        public Ville Read(int pVilleId)
        {
            return (from v in Instance.Context.Villes
                where v.ID == pVilleId
                select v).First<Ville>();
        }

        public void Refresh(Ville pVille)
        {
            this.Context.Refresh(RefreshMode.StoreWins, pVille);
        }

        public void Update(Ville pVille)
        {
            pVille.SetAllModified<Ville>(Instance.Context);
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

