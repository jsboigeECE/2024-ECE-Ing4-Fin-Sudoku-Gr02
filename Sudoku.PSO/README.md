22/02/2024                                   
PSO Premi�re Tentative :


Liste des classes :

1- PSOSolver 
2- Particle (avec ses constructeurs)

Liste des m�thodes :

- UpdatePosition, qui nous permet de mettre � jour les positions des particules
- SudokuGrid Solve  

En derni�re partie du code de la classe Particle en commentaire, on a cherch� � impl�menter une m�thode UpdateVelocity pour calculer la mise � jour des vitesses des particules

Ce qu'on souhaite faire :

M�thode InitializeParticles : Initialise une liste de particules (solutions candidates) avec des valeurs al�atoires.
M�thode GenerateRandomSolution : G�n�re une solution al�atoire pour le puzzle Sudoku.
M�thode EvaluateFitness : �value la qualit� d'une solution en comptant les conflits (nombres dupliqu�s) dans les lignes, les colonnes et les sous-grilles.
M�thodes CountConflictsInRow, CountConflictsInColumn, CountConflictsInSubgrid : M�thodes auxiliaires pour compter les conflits dans les lignes, les colonnes et les sous-grilles, respectivement.
M�thode UpdateParticles : Met � jour les positions et les vitesses des particules selon l'algorithme PSO.
M�thode ApplySolution : Applique la meilleure solution trouv�e � la grille Sudoku.
Classe SudokuGrid : Repr�sente la grille Sudoku et fournit des m�thodes pour d�finir et obtenir les valeurs des cellules.

Questions :

- Devons-nous utiliser un certain type de topologie (en �toile, en anneau, en rayon) ?
- Quand nous lan�ons le code, il y a des erreurs sur Sudoku.Benchmark 
- Est-ce que nos recherches sur la m�thode de r�solution par essaims optimis�s nous m�nent � avoir un bon d�but de structure de code pour r�soudre le sudoku ?



