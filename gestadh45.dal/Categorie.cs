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
            this.Modeles = new HashSet<Modele>();
        }
    
        public System.Guid ID { get; set; }
        public string Libelle { get; set; }
        public System.Guid ID_DureeDeVie { get; set; }
    
        public virtual ICollection<Modele> Modeles { get; set; }
        public virtual DureeDeVie DureeDeVie { get; set; }
    }
    
}
