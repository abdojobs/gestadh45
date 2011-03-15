namespace GestAdh45.Dao
{
    using GestAdh45.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class JourSemaineDao
    {
        private static JourSemaineDao Instance;

        private JourSemaineDao()
        {
        }

        public static JourSemaineDao GetInstance(Entities pContexte)
        {
            if (Instance == null)
            {
                Instance = new JourSemaineDao();
            }
            Instance.Context = pContexte;
            return Instance;
        }

        public List<JourSemaine> List()
        {
            return (from j in Instance.Context.JourSemaines
                orderby j.Numero
                select j).ToList<JourSemaine>();
        }

        public JourSemaine Read(int pJourId)
        {
            return (from j in Instance.Context.JourSemaines
                where j.ID == pJourId
                select j).First<JourSemaine>();
        }

        public void Refresh(JourSemaine pJourSemaine)
        {
            this.Context.Refresh(RefreshMode.StoreWins, pJourSemaine);
        }

        private Entities Context { get; set; }
    }
}

