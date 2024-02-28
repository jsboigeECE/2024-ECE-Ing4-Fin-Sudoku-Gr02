        string code = System.IO.File.ReadAllText(scriptPath);
        scope.Exec(code);

        // Appelle la fonction Python et récupère la grille résolue
        PyObject result = scope.Eval("solve_sudoku_python(sudoku_grid)");

        // Convertit le résultat Python en tableau C#
        var managedResult = result.As<int[][]>();
        return new Shared.SudokuGrid() { Cells = managedResult};
        }
    }
}


FICHIER PYTHON : 

import networkx as nx
import numpy as np

def create_sudoku_graph():
    G = nx.Graph()
    for i in range(9):
        for j in range(9):
            G.add_node((i, j))
    for i in range(9):
        for j in range(9):
            for k in range(9):
                if k != j:
                    G.add_edge((i, j), (i, k))
                if k != i:
                    G.add_edge((i, j), (k, j))
            for k in range(i//3 * 3, i//3 * 3 + 3):
                for l in range(j//3 * 3, j//3 * 3 + 3):
                    if (k, l) != (i, j):
                        G.add_edge((i, j), (k, l))
    return G

def is_valid_placement(graph, position, color):
    row, col = position
    for i in range(9):
        if 'color' in graph.nodes[(row, i)] and graph.nodes[(row, i)]['color'] == color:
            return False
        if 'color' in graph.nodes[(i, col)] and graph.nodes[(i, col)]['color'] == color:
            return False
    startRow, startCol = 3 * (row // 3), 3 * (col // 3)
    for i in range(3):
        for j in range(3):
            if 'color' in graph.nodes[(startRow + i, startCol + j)] and graph.nodes[(startRow + i, startCol + j)]['color'] == color:
                return False
    return True

def solve_sudoku_with_backtracking(graph, position=0):
    if position == 81:
        return True
    row, col = divmod(position, 9)
    if 'color' in graph.nodes[(row, col)]:
        return solve_sudoku_with_backtracking(graph, position + 1)
    for color in range(1, 10):
        if is_valid_placement(graph, (row, col), color):
            graph.nodes[(row, col)]['color'] = color
            if solve_sudoku_with_backtracking(graph, position + 1):
                return True
            del graph.nodes[(row, col)]['color']
    return False

def solve_sudoku_python(sudoku_grid):
    graph = create_sudoku_graph()
    for i in range(9):
        for j in range(9):
            if sudoku_grid[i][j] != 0:
                graph.nodes[(i, j)]['color'] = sudoku_grid[i][j]
    if solve_sudoku_with_backtracking(graph):
        return [[graph.nodes[(i, j)]['color'] for j in range(9)] for i in range(9)]
    else:
        raise ValueError("No solution found for the given Sudoku puzzle.")
