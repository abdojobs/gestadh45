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
    public partial class Equipement
    {
        public System.Guid ID { get; set; }
        public string Numero { get; set; }
        public Nullable<System.DateTime> DateAchat { get; set; }
        public Nullable<System.DateTime> DateMiseEnService { get; set; }
        public System.DateTime DateCreation { get; set; }
        public System.DateTime DateModification { get; set; }
        public string Commentaire { get; set; }
        public System.Guid ID_Marque { get; set; }
    
        public virtual Marque Marque { get; set; }
    }
    
}
