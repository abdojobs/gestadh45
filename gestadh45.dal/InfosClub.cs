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
    public partial class InfosClub
    {
        public System.Guid ID { get; set; }
        public string Nom { get; set; }
        public string Numero { get; set; }
        public string Siren { get; set; }
        public string NIC { get; set; }
        public string Adresse { get; set; }
        public Nullable<System.Guid> ID_Ville { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string SiteWeb { get; set; }
        public string NumAPS { get; set; }
    
        public virtual Ville Ville { get; set; }
    }
    
}
