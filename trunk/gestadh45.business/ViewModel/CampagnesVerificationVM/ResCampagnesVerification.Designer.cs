﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.17379
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestadh45.business.ViewModel.CampagnesVerificationVM {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResCampagnesVerification {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResCampagnesVerification() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("gestadh45.business.ViewModel.CampagnesVerificationVM.ResCampagnesVerification", typeof(ResCampagnesVerification).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Sélectionner au moins un équipement à vérifier.
        /// </summary>
        internal static string ErrAucunEquipement {
            get {
                return ResourceManager.GetString("ErrAucunEquipement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Ce couple libellé/date existe déjà.
        /// </summary>
        internal static string ErrCampagneExiste {
            get {
                return ResourceManager.GetString("ErrCampagneExiste", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Commentaire obligatoire pour : {0}.
        /// </summary>
        internal static string ErrCommentaireObligatoire {
            get {
                return ResourceManager.GetString("ErrCommentaireObligatoire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Tous les équipements doivent êtres vérifiés avant de clore cette campagne.
        /// </summary>
        internal static string ErrEquipementNonVerifie {
            get {
                return ResourceManager.GetString("ErrEquipementNonVerifie", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Le libellé est obligatoire.
        /// </summary>
        internal static string ErrLibelleObligatoire {
            get {
                return ResourceManager.GetString("ErrLibelleObligatoire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Le responsable est obligatoire.
        /// </summary>
        internal static string ErrResponsableObligatoire {
            get {
                return ResourceManager.GetString("ErrResponsableObligatoire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à La campagne de vérification a été supprimée.
        /// </summary>
        internal static string InfoCampagneVerificationSupprimee {
            get {
                return ResourceManager.GetString("InfoCampagneVerificationSupprimee", resourceCulture);
            }
        }
    }
}
