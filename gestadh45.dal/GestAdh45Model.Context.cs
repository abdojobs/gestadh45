﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace gestadh45.dal
{
    public partial class gestadh45Entities : DbContext
    {
        public gestadh45Entities()
            : base("name=gestadh45Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<InfosClub> InfosClubs { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<JourSemaine> JourSemaines { get; set; }
        public DbSet<Saison> Saisons { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<StatutInscription> StatutInscriptions { get; set; }
        public DbSet<TrancheAge> TrancheAges { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<Marque> Marques { get; set; }
    }
}
