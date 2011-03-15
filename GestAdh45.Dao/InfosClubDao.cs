namespace GestAdh45.Dao
{
    using GestAdh45.Model;
    using System;
    using System.Data;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class InfosClubDao
    {
        private static InfosClubDao Instance;

        private InfosClubDao()
        {
        }

        public static InfosClubDao GetInstance(Entities pContexte)
        {
            if (Instance == null)
            {
                Instance = new InfosClubDao();
            }
            Instance.Context = pContexte;
            return Instance;
        }

        public InfosClub Read()
        {
            return (from i in Instance.Context.InfosClubs select i).First<InfosClub>();
        }

        public void Refresh(InfosClub pInfosClub)
        {
            this.Context.Refresh(RefreshMode.StoreWins, pInfosClub);
        }

        public void Update(InfosClub pInfosClub)
        {
            pInfosClub.SetAllModified<InfosClub>(Instance.Context);
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

