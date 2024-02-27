# Sudoku Solver - Backtracking Algorithm

Ce projet est une implémentation d'un solveur de Sudoku en C# utilisant l'algorithme de backtracking.

## Fonctionnalités

- Résolution de grilles de Sudoku à l'aide de l'algorithme de backtracking.
- Intégration facile dans d'autres applications ou projets utilisant le langage C#.

## Structure du Projet

- `Sudoku.Shared` : Contient les interfaces et les types partagés pour le projet.
- `Sudoku.Backtracking2` : Implémentation du solveur de Sudoku utilisant l'algorithme de backtracking.
- `README.md` : Ce fichier. Fournit des informations sur le projet, son utilisation et sa structure.

## Méthodes

### `SudokuGrid Solve(SudokuGrid s)`

Cette méthode est utilisée pour résoudre une grille de Sudoku en utilisant l'algorithme de backtracking.

Paramètres :
- `s` : La grille de Sudoku à résoudre.

Retour :
- La grille de Sudoku résolue.

### `int[,] Conversion(SudokuGrid s)`

Cette méthode convertit une grille de Sudoku au format utilisé par l'algorithme de backtracking.

Paramètres :
- `s` : La grille de Sudoku à convertir.

Retour :
- La grille de Sudoku convertie au format `int[,]`.

### `bool IsSafe(int[,] grid, int row, int col, int num)`

Cette méthode vérifie si la valeur donnée peut être placée dans la case spécifiée sans violer les règles du Sudoku.
Elle vérifie si la valeur est déjà présente dans la même ligne, la même colonne ou le même bloc.

Paramètres :
- `grid` : La grille de Sudoku.
- `row` : L'indice de ligne de la case.
- `col` : L'indice de colonne de la case.
- `num` : La valeur à vérifier.

Retour :
- True si la valeur peut être placée en toute sécurité dans la case spécifiée, False sinon.

### `bool SolverBacktracking(int[,] grid, int row, int col)`

Cette méthode récursive est utilisée pour résoudre une grille de Sudoku en utilisant l'algorithme de backtracking.
Elle parcourt la grille case par case en essayant différentes valeurs et vérifie si chaque valeur est valide.

Paramètres :
- `grid` : La grille de Sudoku à résoudre.
- `row` : L'indice de ligne actuel.
- `col` : L'indice de colonne actuel.

Retour :
- True si la grille a été résolue avec succès, False sinon.

## Difficultés Rencontrées

    -** Git et VSCode :  La configuration du projet, la synchronisation et l'utilisation de GitHub ont présenté des défis initiaux.
    -** Une des autres difficultés a été de trouver une solution pour représenter la grille de Sudoku de manière efficace pour l'algorithme de backtracking.   
     Nous avons dû mettre en place une conversion entre le format utilisé par le solveur et celui utilisé par l'interface utilisateur.
    -** De plus la logique impliquée dans le processus de backtracking est complexe donc nous avons pris du temps
     pour le comprendre parfaitement pour povoir l'implémenter de manière efficace dans le code.

## Performance 

    -** Facile : 0.0204 ms 
    -** Medium : 117,766 ms    
    -** Hard : 3804.4044 ms      

puzzle index 1

## Exemple d'utilisation

```C#
// Exemple d'utilisation dans votre propre application
SudokuGrid grid = new SudokuGrid(/* Insérez votre grille de Sudoku ici */);
Backtracking2Solver solver = new Backtracking2Solver();
SudokuGrid solution = solver.Solve(grid);
// Utilisez la grille solution pour afficher ou traiter le résultat.


