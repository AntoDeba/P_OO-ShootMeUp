# Évaluation intermédiaire (80 %) – Antoni Débaz

## Synthèse

Le jeu est riche et jouable : plusieurs types d'ennemis, des niveaux selon l'argent accumulé, une victoire et une défaite, des projectiles orientés vers la souris et des obstacles qui bloquent tout le monde. Il manque une exigence du CdC : pas de tests unitaires.  
Le code souffre de beaucoup de copier/coller et de quelques mauvaises pratiques (try/catch pour contrôler le flux, conventions de nommage).  
Les références à Drones sont toujours présentes.

## Points à améliorer d'ici au rendu final (30.10)

1. Faire disparaître les référence à Drones
2. Supprimer le bricolage try/catch ([BattleMap.cs:193-203](Base/Drones/View/BattleMap.cs#L193-L203) et [222-232](Base/Drones/View/BattleMap.cs#L222-L232)). Une exception ne doit pas servir à savoir si un élément a été retiré. Utiliser un booléen `removed` et `continue`. Attention : si `i` n'était pas le dernier indice, `allBurgers[i]` désigne maintenant **un autre burger**, qui sera testé à sa place.
4. Respecter les conventions de nommage C# (PascalCase pour les classes et les méthodes).
5. Tests unitaires
6. Rapport : sections tests, conception (diagramme de classes), IA, bilans.
