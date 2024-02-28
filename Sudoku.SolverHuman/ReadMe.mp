# Sudoku Solver - Approche Humaine

## Introduction

Ce projet vise à créer un solveur de Sudoku qui imite l'approche humaine pour résoudre des puzzles. Le cœur de ce projet réside dans l'utilisation de techniques avancées de résolution de Sudoku, encapsulées dans une bibliothèque de résolution préexistante, et l'adaptation de ces méthodes pour être utilisées avec notre propre modèle de données `SudokuGrid`.

###1er Partie : Utilisation Méthodes huamines

## Fonctionnement

Le solveur transforme une instance de `SudokuGrid` en un `Puzzle` compatible avec la bibliothèque de résolution Sudoku. Après la résolution, le puzzle est reconverti en `SudokuGrid`. Ce processus permet d'utiliser une bibliothèque puissante tout en maintenant une interface cohérente avec notre modèle de données.

## Démarches

- **Importation de la bibliothèque**: Nous avons importé une bibliothèque existante qui implémente des techniques de résolution de Sudoku.
- **Conversion d'entrée**: Nous avons créé des fonctions pour convertir notre `SudokuGrid` en un format `Puzzle` requis par la bibliothèque.
- **Résolution**: La bibliothèque applique des techniques de résolution pour trouver la solution du puzzle.
- **Conversion de sortie**: Le `Puzzle` résolu est converti en arrière en `SudokuGrid`.

## Problèmes Rencontrés

- ** Git et VSCode**: Nous avons eu des problèmes initiaux avec la synchronisation du code et la configuration de l'environnement de développement. Après un ajustement des processus et une réinitialisation des configurations, nous avons pu les résoudre.
- ** Niveau de protection**: Nous avons rencontré un problème avec le niveau de protection de la classe `Puzzle`. La résolution a nécessité de changer la visibilité du constructeur de `Puzzle` de `internal` à `public`, afin de permettre son instanciation dans notre projet.

## Remarque

On a également amélioré le code en ajoutant une méthode qui permettant de remplir les cases triviales, ce qui augmente la vitesse de résolution du sudoku si il est facile. La vitesse reste la même pour une difficulté plus élévée

## Performance

-** Facile (tous niveaux) : 0.18 ms
-** Medium (Niveau 1) : 8.5 ms
-** Hard (Niveau): 5.5 ms

Impossible de résourdre le Sudoku à partir du niveau 2 Medium (Utilisation BackTracking)


### 2e Partie: Utilisation du BackTracking sur des sudokus plus complexes

## Démarches

- **Conversion en Tableau Bidimensionnel** : Nous convertissons le `Puzzle` en un tableau bidimensionnel pour faciliter l'implémentation du backtracking.
- **Application du Backtracking** : Un algorithme de backtracking est utilisé pour remplir les cellules vides et résoudre le puzzle.
- **Reconversion en `SudokuGrid`** : Le tableau bidimensionnel résolu est reconverti en `SudokuGrid`.

## Problèmes Rencontrés

- ** Gestion des Dimensions** : La conversion entre les différentes représentations de la grille de Sudoku a présenté des défis, notamment en ce qui concerne la gestion des indices et des dimensions du tableau. Nous avons résolu cela en créant des méthodes de conversion claires et bien définies.
- ** Probléme de type avec le board. 

## Performance

- **Difficulté Élevée : Le temps de résolution peut varier considérablement en fonction de la complexité du puzzle. Les optimisations ont permis de réduire ce temps.

## Résultat 

- **Il est maintenant possible de résourdre tous les sudokus. Grace à une condition if, le sudoku sera tout d'abord résolu par les méthodes humains si possible, sinon on utilisera le BackTracking.

## Amélioration 

On a modifié notre BackTracking en implémentant les heuristiques MRV et LCV ce qui améliorent la performance de notre code. On passe de 180 ms à environ 165 ms en Medium 2 et une petit différence d'une dizaine de milli secondes en Hard 10.