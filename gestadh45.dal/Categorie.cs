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
    public partial class Categorie
    {
        public Categorie()
        {
            this.Equipements = new HashSet<Equipement>();
        }
    
        public System.Guid ID { get; set; }
        public string Libelle { get; set; }
    
        public virtual ICollection<Equipement> Equipements { get; set; }
    }
    
}
