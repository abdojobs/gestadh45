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
    public partial class CampagneVerification
    {
        public CampagneVerification()
        {
            this.Verifications = new HashSet<Verification>();
        }
    
        public System.Guid ID { get; set; }
        public System.DateTime Date { get; set; }
        public string Responsable { get; set; }
        public bool EstValidee { get; set; }
    
        public virtual ICollection<Verification> Verifications { get; set; }
    }
    
}