//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace gestadh45.dal
{
    public partial class Groupe
    {
        public Groupe()
        {
            this.Inscriptions = new HashSet<Inscription>();
        }
    
        public System.Guid ID { get; set; }
        public string Libelle { get; set; }
        public System.Guid ID_JourSemaine { get; set; }
        public int NbPlaces { get; set; }
        public string Commentaire { get; set; }
        public System.Guid ID_Saison { get; set; }
        public System.DateTime HeureDebut { get; set; }
        public System.DateTime HeureFin { get; set; }
    
        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual JourSemaine JourSemaine { get; set; }
        public virtual Saison Saison { get; set; }
    }
    
}
