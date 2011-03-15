namespace GestAdh45.Dao
{
    using GestAdh45.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SexeDao
    {
        private static SexeDao Instance;

        private SexeDao()
        {
        }

        public static SexeDao GetInstance(Entities pContexte)
        {
            if (Instance == null)
            {
                Instance = new SexeDao();
            }
            Instance.Context = pContexte;
            return Instance;
        }

        public List<Sexe> List()
        {
            return (from s in Instance.Context.Sexes
                orderby s.LibelleCourt
                select s).ToList<Sexe>();
        }

        public Sexe Read(int pSexeId)
        {
            return (from s in Instance.Context.Sexes
                where s.ID == pSexeId
                select s).First<Sexe>();
        }

        public void Refresh(Sexe pSexe)
        {
            this.Context.Refresh(RefreshMode.StoreWins, pSexe);
        }

        private Entities Context { get; set; }
    }
}

