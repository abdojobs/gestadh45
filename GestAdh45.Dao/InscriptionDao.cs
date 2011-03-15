namespace GestAdh45.Dao
{
    using GestAdh45.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class InscriptionDao
    {
        private static InscriptionDao Instance;

        private InscriptionDao()
        {
        }

        public void Create(Inscription pInscription)
        {
            pInscription.DateCreation = DateTime.Now;
            pInscription.DateModification = DateTime.Now;
            Instance.Context.Inscriptions.AddObject(pInscription);
            Instance.Context.SaveChanges();
        }

        public void Delete(Inscription pInscription)
        {
            Instance.Context.Attach(pInscription);
            Instance.Context.DeleteObject(pInscription);
            Instance.Context.SaveChanges();
        }

        public bool Exist(Inscription pInscription)
        {
            return ((from i in Instance.Context.Inscriptions
                where (i.ID_Adherent == pInscription.ID_Adherent) && (i.ID_Groupe == pInscription.ID_Groupe)
                select i).Count<Inscription>() > 0);
        }

        public static InscriptionDao GetInstance(Entities pContexte)
        {
            if (Instance == null)
            {
                Instance = new InscriptionDao();
            }
            Instance.Context = pContexte;
            return Instance;
        }

        public List<Inscription> List()
        {
            return (from i in Instance.Context.Inscriptions
                orderby i.Groupe.Saison.ID
                orderby i.Adherent.Nom
                orderby i.Adherent.Prenom
                select i).ToList<Inscription>();
        }

        public List<Inscription> ListSaisonCourante()
        {
            return (from i in Instance.Context.Inscriptions
                where i.Groupe.Saison.EstSaisonCourante == 1L
                orderby i.Adherent.Nom
                orderby i.Adherent.Prenom
                select i).ToList<Inscription>();
        }

        public Inscription Read(int pInscriptionId)
        {
            return (from i in Instance.Context.Inscriptions
                where i.ID == pInscriptionId
                select i).First<Inscription>();
        }

        public void Refresh(Inscription pInscription)
        {
            this.Context.Refresh(RefreshMode.StoreWins, pInscription);
            this.Context.Refresh(RefreshMode.StoreWins, pInscription.Adherent);
            this.Context.Refresh(RefreshMode.StoreWins, pInscription.Groupe);
        }

        public void Update(Inscription pInscription)
        {
            pInscription.DateModification = DateTime.Now;
            pInscription.SetAllModified<Inscription>(Instance.Context);
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

