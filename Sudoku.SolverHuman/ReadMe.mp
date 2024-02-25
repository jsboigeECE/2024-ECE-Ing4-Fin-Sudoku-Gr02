# Sudoku Solver - Approche Humaine

## Introduction

Ce projet vise à créer un solveur de Sudoku qui imite l'approche humaine pour résoudre des puzzles. Le cœur de ce projet réside dans l'utilisation de techniques avancées de résolution de Sudoku, encapsulées dans une bibliothèque de résolution préexistante, et l'adaptation de ces méthodes pour être utilisées avec notre propre modèle de données `SudokuGrid`.

## Fonctionnement

Le solveur transforme une instance de `SudokuGrid` en un `Puzzle` compatible avec la bibliothèque de résolution Sudoku. Après la résolution, le puzzle est reconverti en `SudokuGrid`. Ce processus permet d'utiliser une bibliothèque puissante tout en maintenant une interface cohérente avec notre modèle de données.

## Démarches

- **Importation de la bibliothèque**: Nous avons importé une bibliothèque existante qui implémente des techniques de résolution de Sudoku.
- **Conversion d'entrée**: Nous avons créé des fonctions pour convertir notre `SudokuGrid` en un format `Puzzle` requis par la bibliothèque.
- **Résolution**: La bibliothèque applique des techniques de résolution pour trouver la solution du puzzle.
- **Conversion de sortie**: Le `Puzzle` résolu est converti en arrière en `SudokuGrid`.

## Problèmes Rencontrés

- **Git et VSCode**: Nous avons eu des problèmes initiaux avec la synchronisation du code et la configuration de l'environnement de développement. Après un ajustement des processus et une réinitialisation des configurations, nous avons pu les résoudre.
- **Niveau de protection**: Nous avons rencontré un problème avec le niveau de protection de la classe `Puzzle`. La résolution a nécessité de changer la visibilité du constructeur de `Puzzle` de `internal` à `public`, afin de permettre son instanciation dans notre projet.
- ** Le code compile mais ne résourd pas le sudoku

## Remarque

On a également amélioré le code en ajoutant une méthode qui permettant de remplir les cases triviales, ce qui augmente la vitesse de résolution du sudoku si il est facile. La vitesse reste la même pour une difficulté plus élévée

## Performance

-** Facile : 0.18 ms
-** Medium : 8.5 ms
-** Hard : 5.5 ms