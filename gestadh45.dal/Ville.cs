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
    public partial class Ville
    {
        public Ville()
        {
            this.Adherents = new HashSet<Adherent>();
            this.InfosClubs = new HashSet<InfosClub>();
        }
    
        public System.Guid ID { get; set; }
        public string Libelle { get; set; }
        public string CodePostal { get; set; }
    
        public virtual ICollection<Adherent> Adherents { get; set; }
        public virtual ICollection<InfosClub> InfosClubs { get; set; }
    }
    
}
