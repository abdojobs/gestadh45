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
    public partial class JourSemaine
    {
        public JourSemaine()
        {
            this.Groupes = new HashSet<Groupe>();
        }
    
        public System.Guid ID { get; set; }
        public int Numero { get; set; }
        public string Libelle { get; set; }
    
        public virtual ICollection<Groupe> Groupes { get; set; }
    }
    
}
