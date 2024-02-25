22/02/2024                                   
PSO Première Tentative :


Liste des classes :

1- PSOSolver 
2- Particle (avec ses constructeurs)

Liste des méthodes :

- UpdatePosition, qui nous permet de mettre à jour les positions des particules
- SudokuGrid Solve  

En dernière partie du code de la classe Particle en commentaire, on a cherché à implémenter une méthode UpdateVelocity pour calculer la mise à jour des vitesses des particules

Ce qu'on souhaite faire :

Méthode InitializeParticles : Initialise une liste de particules (solutions candidates) avec des valeurs aléatoires.
Méthode GenerateRandomSolution : Génère une solution aléatoire pour le puzzle Sudoku.
Méthode EvaluateFitness : Évalue la qualité d'une solution en comptant les conflits (nombres dupliqués) dans les lignes, les colonnes et les sous-grilles.
Méthodes CountConflictsInRow, CountConflictsInColumn, CountConflictsInSubgrid : Méthodes auxiliaires pour compter les conflits dans les lignes, les colonnes et les sous-grilles, respectivement.
Méthode UpdateParticles : Met à jour les positions et les vitesses des particules selon l'algorithme PSO.
Méthode ApplySolution : Applique la meilleure solution trouvée à la grille Sudoku.
Classe SudokuGrid : Représente la grille Sudoku et fournit des méthodes pour définir et obtenir les valeurs des cellules.

Questions :

- Devons-nous utiliser un certain type de topologie (en étoile, en anneau, en rayon) ?
- Quand nous lançons le code, il y a des erreurs sur Sudoku.Benchmark 
- Est-ce que nos recherches sur la méthode de résolution par essaims optimisés nous mènent à avoir un bon début de structure de code pour résoudre le sudoku ?



