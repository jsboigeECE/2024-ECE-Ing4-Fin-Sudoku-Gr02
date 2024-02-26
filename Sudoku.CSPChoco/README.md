# Sudoku Solver avec la librairie PuLP

## Introduction

Ce projet contient une implémentation en Python pour résoudre des puzzles Sudoku en utilisant la programmation linéaire. La bibliothèque PuLP est utilisée pour modéliser le problème et trouver une solution satisfaisante aux contraintes du Sudoku.

## Approche

L'approche consiste à représenter le puzzle Sudoku comme un problème de programmation linéaire où chaque cellule peut prendre une valeur entre 1 et 9, en respectant les règles standard du Sudoku. Les contraintes assurent qu'aucun chiffre n'est répété dans une ligne, une colonne ou une sous-grille 3x3.

## Structure du Code

Le code est structuré autour d'une classe principale `SudokuSolver` qui contient toutes les méthodes nécessaires à la résolution d'un puzzle Sudoku.

### Méthodes

- `__init__(self, grid)`: Constructeur de la classe `SudokuSolver`. Il prend en entrée une grille de Sudoku où les cases vides sont représentées par des zéros.

- `solve(self)`: Méthode principale qui modélise le problème et fait appel au solveur. Elle crée les contraintes pour les lignes, les colonnes et les sous-grilles 3x3, ainsi que pour les valeurs déjà présentes dans la grille initiale. Retourne la grille de Sudoku résolue.

  -Contraintes par ligne: Assure que chaque nombre de 1 à 9 apparaît une fois par ligne.
  -Contraintes par colonne: Assure que chaque nombre de 1 à 9 apparaît une fois par colonne.
  -Contraintes par sous-grille: Assure que chaque nombre de 1 à 9 apparaît une fois par sous-grille 3x3.
