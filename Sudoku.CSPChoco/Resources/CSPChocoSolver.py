from timeit import default_timer
from pulp import *

class SudokuSolver:
    
    def __init__(self, grid):
        self.SIZE = 9
        self.grid = grid
        self.model = LpProblem("SudokuSolver", LpMinimize)
        self.vars = LpVariable.dicts("Vars", (range(self.SIZE), range(self.SIZE), range(1, self.SIZE+1)), cat='Binary')

    def solve(self):
        # Objective function (dummy in this case, as we don't need to optimize any particular variable)
        self.model += 0, "Arbitrary Objective Function"

        # Constraints
        # Each cell must only contain one value
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for v in range(1, self.SIZE+1)]) == 1

        # Each value appears once per row, column, and box
        for v in range(1, self.SIZE+1):
            for r in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for c in range(self.SIZE)]) == 1
            for c in range(self.SIZE):
                self.model += lpSum([self.vars[r][c][v] for r in range(self.SIZE)]) == 1
            for boxRow in range(3):
                for boxCol in range(3):
                    self.model += lpSum([self.vars[3*boxRow+i][3*boxCol+j][v] for i in range(3) for j in range(3)]) == 1
        
        # Pre-assigned cells
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                if self.grid[r][c] != 0: # Non-zero entries are pre-assigned
                    self.model += self.vars[r][c][self.grid[r][c]] == 1

        # Solve the model
        self.model.solve()
        
        # Solution extraction
        grille = [[None for _ in range(9)] for _ in range(9)]   # Création de la grille de sudoku résolue qui sera renvoyée au code c#
        for r in range(self.SIZE):
            for c in range(self.SIZE):
                for v in range(1, self.SIZE+1):
                    if value(self.vars[r][c][v]) == 1:
                        grille[r][c] = v
                        break
        return grille
        
# Programme principal
    
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

solver = SudokuSolver(instance)
r = solver.solve()
