﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.17626
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestadh45.business.ViewModel.EquipementsVM {
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
    internal class ResEquipements {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResEquipements() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("gestadh45.business.ViewModel.EquipementsVM.ResEquipements", typeof(ResEquipements).Assembly);
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
        ///   Recherche une chaîne localisée semblable à Il existe déjà un équipement portant le numéro {0}.
        /// </summary>
        internal static string ErrEquipementExiste {
            get {
                return ResourceManager.GetString("ErrEquipementExiste", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Le modèle est obligatoire.
        /// </summary>
        internal static string ErrModeleObligatoire {
            get {
                return ResourceManager.GetString("ErrModeleObligatoire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Le numéro est obligatoire.
        /// </summary>
        internal static string ErrNumeroObligatoire {
            get {
                return ResourceManager.GetString("ErrNumeroObligatoire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Cet équipement a atteint sa durée de vie, pensez à le remplacer.
        /// </summary>
        internal static string InfoDureeDeVieAtteinte {
            get {
                return ResourceManager.GetString("InfoDureeDeVieAtteinte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Cet équipement n&apos;a pas atteint sa durée de vie.
        /// </summary>
        internal static string InfoDureeDeVieNonAtteinte {
            get {
                return ResourceManager.GetString("InfoDureeDeVieNonAtteinte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à L&apos;équipement a été dupliqué.
        /// </summary>
        internal static string InfoEquipementDuplique {
            get {
                return ResourceManager.GetString("InfoEquipementDuplique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à L&apos;équipement a été supprimé.
        /// </summary>
        internal static string InfoEquipementSupprime {
            get {
                return ResourceManager.GetString("InfoEquipementSupprime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} - Inventaire complet.
        /// </summary>
        internal static string NomFichierRapportInventaireComplet {
            get {
                return ResourceManager.GetString("NomFichierRapportInventaireComplet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} - Inventaire simple.
        /// </summary>
        internal static string NomFichierRapportInventaireSimple {
            get {
                return ResourceManager.GetString("NomFichierRapportInventaireSimple", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} équipements.
        /// </summary>
        internal static string SousTitreInventaireComplet {
            get {
                return ResourceManager.GetString("SousTitreInventaireComplet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à {0} équipements.
        /// </summary>
        internal static string SousTitreInventaireSimple {
            get {
                return ResourceManager.GetString("SousTitreInventaireSimple", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Inventaire détaillé ({0}).
        /// </summary>
        internal static string TitreRapportInventaireComplet {
            get {
                return ResourceManager.GetString("TitreRapportInventaireComplet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Inventaire ({0}).
        /// </summary>
        internal static string TitreRapportInventaireSimple {
            get {
                return ResourceManager.GetString("TitreRapportInventaireSimple", resourceCulture);
            }
        }
    }
}
