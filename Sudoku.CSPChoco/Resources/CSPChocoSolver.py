from timeit import default_timer
from pulp import *

class SudokuSolver:
        
    #Initialisation de la classe
    def __init__(self, grid):
        self.SIZE = 9           #Variable représentant la taille d'un sudoku
        self.grid = grid
        self.model = LpProblem("SudokuSolver", LpMinimize)          #Création du modèle de programmation linéaire
        self.vars = LpVariable.dicts("Vars", (range(self.SIZE), range(self.SIZE), range(1, self.SIZE+1)), cat='Binary')         #Création des variables de décision, une pour chaque possible chiffre dans chaque cellule

    #Méthode pour la résolution du sudoku
    def solve(self):

        #Etablissement des contraintes

        #Contrainte 1: chaque cellule doit contenir exactement un chiffre 
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for v in range(1, self.SIZE+1)]) == 1

        #Contraintes pour les lignes, colonnes, et sous-grilles
        for v in range(1, self.SIZE+1):
            
            #Contrainte 2: les valeurs des cases de chaque ligne doivent être différentes
            for r in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for c in range(self.SIZE)]) == 1

            #Contrainte 3: les valeurs des cases de chaque colonne doivent être différentes
            for c in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for r in range(self.SIZE)]) == 1

            #Contrainte 4: les valeurs des cases de chaque sous-grille de sudoku doivent être différentes
            for boxRow in range(3):
                for boxCol in range(3):
                    self.model += lpSum([self.vars[3*boxRow+i][3*boxCol+j][v] for i in range(3) for j in range(3)]) == 1
        
        #Contrainte pour les valeurs pré-assignées
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                if self.grid[r][c] != 0:            #Les entrées non nulles sont pré-attribuées
                    self.model += self.vars[r][c][self.grid[r][c]] == 1

        #Résolution du sudoku avec le solveur
        self.model.solve()
        
        #Récupération du sudoku résolu
        grille = [[None for _ in range(9)] for _ in range(9)]   # Création de la grille de sudoku résolue qui sera renvoyée au code c#
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                for v in range(1, self.SIZE+1):
                    if value(self.vars[r][c][v]) == 1:
                        grille[r][c] = v
                        break
        return grille
        
#Programme principal
    
'''
sudoku = [
    [5, 3, 0, 0, 7, 0, 0, 0, 0],
    [6, 0, 0, 1, 9, 5, 0, 0, 0],
    [0, 9, 8, 0, 0, 0, 0, 6, 0],
    [8, 0, 0, 0, 6, 0, 0, 0, 3],
    [4, 0, 0, 8, 0, 3, 0, 0, 1],
    [7, 0, 0, 0, 2, 0, 0, 0, 6],
    [0, 6, 0, 0, 0, 0, 2, 8, 0],
    [0, 0, 0, 4, 1, 9, 0, 0, 5],
    [0, 0, 0, 0, 8, 0, 0, 7, 9]
]
'''

#Création d'une instance du solveur avec la grille de sudoku récupérée
solver = SudokuSolver(instance)

#Appel de la méthode de résolution du sudoku et récupération de la grille résolue
r = solver.solve()
