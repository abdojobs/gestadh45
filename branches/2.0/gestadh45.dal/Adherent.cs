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
    public partial class Adherent
    {
        public Adherent()
        {
            this.Inscriptions = new HashSet<Inscription>();
        }
    
        public long ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public System.DateTime DateNaissance { get; set; }
        public System.DateTime DateCreation { get; set; }
        public System.DateTime DateModification { get; set; }
        public long ID_Sexe { get; set; }
        public string Commentaire { get; set; }
        public string Adresse { get; set; }
        public long ID_Ville { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Mail1 { get; set; }
        public string Mail2 { get; set; }
        public string Mail3 { get; set; }
    
        public virtual ICollection<Inscription> Inscriptions { get; set; }
        public virtual Sexe Sexe { get; set; }
        public virtual Ville Ville { get; set; }
    }
    
}
