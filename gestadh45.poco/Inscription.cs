//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace gestadh45.poco
{
    public partial class Inscription
    {
        #region Primitive Properties
    
        public virtual long ID
        {
            get;
            set;
        }
    
        public virtual long ID_Adherent
        {
            get { return _iD_Adherent; }
            set
            {
                if (_iD_Adherent != value)
                {
                    if (Adherent != null && Adherent.ID != value)
                    {
                        Adherent = null;
                    }
                    _iD_Adherent = value;
                }
            }
        }
        private long _iD_Adherent;
    
        public virtual long ID_Groupe
        {
            get { return _iD_Groupe; }
            set
            {
                if (_iD_Groupe != value)
                {
                    if (Groupe != null && Groupe.ID != value)
                    {
                        Groupe = null;
                    }
                    _iD_Groupe = value;
                }
            }
        }
        private long _iD_Groupe;
    
        public virtual bool CertificatMedicalRemis
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> Cotisation
        {
            get;
            set;
        }
    
        public virtual System.DateTime DateCreation
        {
            get;
            set;
        }
    
        public virtual System.DateTime DateModification
        {
            get;
            set;
        }
    
        public virtual string Commentaire
        {
            get;
            set;
        }
    
        public virtual long ID_StatutInscription
        {
            get { return _iD_StatutInscription; }
            set
            {
                if (_iD_StatutInscription != value)
                {
                    if (StatutInscription != null && StatutInscription.ID != value)
                    {
                        StatutInscription = null;
                    }
                    _iD_StatutInscription = value;
                }
            }
        }
        private long _iD_StatutInscription;

        #endregion
        #region Navigation Properties
    
        public virtual Adherent Adherent
        {
            get { return _adherent; }
            set
            {
                if (!ReferenceEquals(_adherent, value))
                {
                    var previousValue = _adherent;
                    _adherent = value;
                    FixupAdherent(previousValue);
                }
            }
        }
        private Adherent _adherent;
    
        public virtual Groupe Groupe
        {
            get { return _groupe; }
            set
            {
                if (!ReferenceEquals(_groupe, value))
                {
                    var previousValue = _groupe;
                    _groupe = value;
                    FixupGroupe(previousValue);
                }
            }
        }
        private Groupe _groupe;
    
        public virtual StatutInscription StatutInscription
        {
            get { return _statutInscription; }
            set
            {
                if (!ReferenceEquals(_statutInscription, value))
                {
                    var previousValue = _statutInscription;
                    _statutInscription = value;
                    FixupStatutInscription(previousValue);
                }
            }
        }
        private StatutInscription _statutInscription;

        #endregion
        #region Association Fixup
    
        private void FixupAdherent(Adherent previousValue)
        {
            if (previousValue != null && previousValue.Inscription.Contains(this))
            {
                previousValue.Inscription.Remove(this);
            }
    
            if (Adherent != null)
            {
                if (!Adherent.Inscription.Contains(this))
                {
                    Adherent.Inscription.Add(this);
                }
                if (ID_Adherent != Adherent.ID)
                {
                    ID_Adherent = Adherent.ID;
                }
            }
        }
    
        private void FixupGroupe(Groupe previousValue)
        {
            if (previousValue != null && previousValue.Inscription.Contains(this))
            {
                previousValue.Inscription.Remove(this);
            }
    
            if (Groupe != null)
            {
                if (!Groupe.Inscription.Contains(this))
                {
                    Groupe.Inscription.Add(this);
                }
                if (ID_Groupe != Groupe.ID)
                {
                    ID_Groupe = Groupe.ID;
                }
            }
        }
    
        private void FixupStatutInscription(StatutInscription previousValue)
        {
            if (previousValue != null && previousValue.Inscription.Contains(this))
            {
                previousValue.Inscription.Remove(this);
            }
    
            if (StatutInscription != null)
            {
                if (!StatutInscription.Inscription.Contains(this))
                {
                    StatutInscription.Inscription.Add(this);
                }
                if (ID_StatutInscription != StatutInscription.ID)
                {
                    ID_StatutInscription = StatutInscription.ID;
                }
            }
        }

        #endregion
    }
}
