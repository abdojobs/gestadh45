### Descriptif des versions ###

  * v2.1 : ??
    * Réorganisation des menus
    * Remplacement de l'écran permettant de visualiser la répartition des adhérents par un écran générique affichant le contenu des rapports Excel
    * Ajout d'un écran permettant de supprimer toutes les données Adhérent/inscription non reliées à la saison courante (pour se mettre en conformité avec la CNIL)
    * Ajout d'un système d'extraction des adresses emails (seulement la première) d'un groupe (hors inscriptions annulées) et les affiche concaténés par une virgule dans la zone de notification

  * v2.0 : 20/08/2012
    * Migration SQLite => SQL Server CE pour la base de données (Incompatibilité avec les BDD de l'ancienne version)
    * Réécriture du code pour de meilleures performances
    * Refonte de l'interface
    * Génération de rapports Excel
    * Apparition d'une section "Gestion de l'équipement"
      * Inventaire de l'équipement
      * Gestion de la durée de vie
      * Gestion des vérifications