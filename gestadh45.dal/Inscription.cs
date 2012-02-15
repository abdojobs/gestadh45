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
    public partial class Inscription
    {
        public long ID { get; set; }
        public long ID_Adherent { get; set; }
        public long ID_Groupe { get; set; }
        public Nullable<decimal> Cotisation { get; set; }
        public System.DateTime DateCreation { get; set; }
        public System.DateTime DateModification { get; set; }
        public string Commentaire { get; set; }
        public long ID_StatutInscription { get; set; }
        public bool CertificatMedicalRemis { get; set; }
    
        public virtual Adherent Adherent { get; set; }
        public virtual Groupe Groupe { get; set; }
        public virtual StatutInscription StatutInscription { get; set; }
    }
    
}
