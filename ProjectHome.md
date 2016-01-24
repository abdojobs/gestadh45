**14/09/2012** : Le projet a migré sur [Codeplex](http://gestadh45.codeplex.com/).
Cette migration s'accompagne de montée de version pour tous les modules utilisés (Entity Framework, MVVM Light, SQL Server CE...)

##################################################################################
Logiciel de gestion des adhérents pour une association sportive.

Technologies utilisées : .NET 4.0, C#, Entity Framework, SQL Server CE 3.5, WPF, MVVM Light Toolkit

**La v2.0 est désormais disponible avec au programme :**

- réécriture complète de l'application pour de meilleur performances

- abandon de SQLite pour SQL Server CE (/!\ les anciennes BDD ne sont plus compatibles, une migration de données et nécessaire)

- Refonte de l'interface

- Ajout de la gestion des EPI (Equipement de Protection Individuels) => nouvel axe de développement principal
  * Inventaire
  * Gestion de la durée de vie
  * Gestion des vérifications
  * Génération de rapports