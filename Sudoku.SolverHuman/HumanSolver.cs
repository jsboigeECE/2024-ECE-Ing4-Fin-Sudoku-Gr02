using Sudoku.Shared;
using Kermalis.SudokuSolver;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.HumanSolver;

public class HumanSolver : ISudokuSolver
{
    // Méthode principale pour résoudre un Sudoku.
    public SudokuGrid Solve(SudokuGrid s)
    {
        Preprocess(s);

        Puzzle puzzle = ConvertToPuzzle(s);

        Solver solver = new Solver(puzzle);
        bool solvedByHumanMethods = solver.TrySolve();

        if (!solvedByHumanMethods)
        {
            int[][] board = ConvertPuzzleToBoard(puzzle);
            if (SolveWithBacktracking(board))
            {
                ConvertBoardToSudokuGrid(board, s);
            }
        }

        return s;
    }

    // Conversion d'un SudokuGrid en Puzzle
    private static Puzzle ConvertToPuzzle(SudokuGrid s)
    {
        int[][] board = s.Cells.Select(row => row.ToArray()).ToArray();
        return new Puzzle(board, false);
    }

    // Conversion d'un Puzzle en SudokuGrid
    private static void ConvertBoardToSudokuGrid(int[][] board, SudokuGrid s)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s.Cells[i][j] = board[i][j];
            }
        }
    }

    // Conversion d'un Puzzle en tableau pour le backtracking
    private static int[][] ConvertPuzzleToBoard(Puzzle puzzle)
    {
        int[][] board = new int[9][];
        for (int i = 0; i < 9; i++)
        {
            board[i] = new int[9];
            for (int j = 0; j < 9; j++)
            {
                board[i][j] = puzzle[i, j].Value;
            }
        }
        return board;
    }

    // Prétraitement: remplissage des cellules simples
     private static void Preprocess(SudokuGrid s)
        {
            // Remplissage des cellules simples.
            bool progress;
            do
            {
                progress = false;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (s.Cells[i][j] == 0)
                        {
                            var candidates = GetCandidates(s.Cells, i, j);

                            if (candidates.Count == 1)
                            {
                                s.Cells[i][j] = candidates.First();
                                progress = true;
                            }
                        }
                    }
                }
            } while (progress);
        }
   

    private static List<int> GetCandidates(int[][] board, int row, int col)
{
    // Obtient les candidats possibles pour une cellule donnée.
    var candidates = Enumerable.Range(1, 9).ToList();
    for (int i = 0; i < 9; i++)
    {
        candidates.Remove(board[row][i]);
        candidates.Remove(board[i][col]);

        int startRow = row / 3 * 3;
        int startCol = col / 3 * 3;
        for (int r = startRow; r < startRow + 3; r++)
        {
            for (int c = startCol; c < startCol + 3; c++)
            {
                candidates.Remove(board[r][c]);
            }
        }
    }
    return candidates;
}


   private static bool SolveWithBacktracking(int[][] board)
{
    var emptyCell = FindEmptyCellWithLeastCandidates(board);
    if (!emptyCell.HasValue)
    {
        return true; // Solution trouvée
    }

    (int row, int col) = emptyCell.Value;
    var candidates = GetCandidates(board, row, col);

    // Intégration de LCV: Trier les candidats basés sur le moins de contraintes
    var sortedCandidates = SortCandidatesByLCV(board, candidates, row, col);

    foreach (var num in sortedCandidates)
    {
        if (IsValid(board, row, col, num))
        {
            board[row][col] = num;
            if (SolveWithBacktracking(board))
            {
                return true;
            }
            board[row][col] = 0; // Effacer et essayer de nouveau
        }
    }

    return false; // Échec, déclenche le backtracking
}

// Trier les candidats basés sur LCV
private static List<int> SortCandidatesByLCV(int[][] board, List<int> candidates, int row, int col)
{
    return candidates.OrderBy(candidate =>
    {
        int constraintCount = 0;

        // Parcourir toutes les cellules dans la même rangée
        for (int c = 0; c < 9; c++)
        {
            // Si la cellule est vide et n'est pas la cellule actuelle
            if (board[row][c] == 0 && c != col)
            {
                // Vérifier si le candidat est une option pour cette cellule
                if (IsCandidateForCell(board, candidate, row, c))
                {
                    constraintCount++;
                }
            }
        }

        // Parcourir toutes les cellules dans la même colonne
        for (int r = 0; r < 9; r++)
        {
            // Si la cellule est vide et n'est pas la cellule actuelle
            if (board[r][col] == 0 && r != row)
            {
                // Vérifier si le candidat est une option pour cette cellule
                if (IsCandidateForCell(board, candidate, r, col))
                {
                    constraintCount++;
                }
            }
        }

        // Parcourir toutes les cellules dans le même bloc 3x3
        int startRow = row - row % 3;
        int startCol = col - col % 3;
        for (int r = startRow; r < startRow + 3; r++)
        {
            for (int c = startCol; c < startCol + 3; c++)
            {
                // Si la cellule est vide et n'est pas la cellule actuelle
                if (board[r][c] == 0 && (r != row || c != col))
                {
                    // Vérifier si le candidat est une option pour cette cellule
                    if (IsCandidateForCell(board, candidate, r, c))
                    {
                        constraintCount++;
                    }
                }
            }
        }

        return constraintCount;
    }).ToList();
}

// Méthode pour vérifier si un candidat est possible pour une cellule donnée
private static bool IsCandidateForCell(int[][] board, int candidate, int row, int col)
{
    // Vérifier la rangée
    for (int c = 0; c < 9; c++)
    {
        if (board[row][c] == candidate)
        {
            return false;
        }
    }

    // Vérifier la colonne
    for (int r = 0; r < 9; r++)
    {
        if (board[r][col] == candidate)
        {
            return false;
        }
    }

    // Vérifier le bloc 3x3
    int startRow = row - row % 3;
    int startCol = col - col % 3;
    for (int r = startRow; r < startRow + 3; r++)
    {
        for (int c = startCol; c < startCol + 3; c++)
        {
            if (board[r][c] == candidate)
            {
                return false;
            }
        }
    }

    return true; // Le candidat est possible pour cette cellule
}



    // Trouver une cellule vide avec le moins de candidats (MRV)
    private static (int, int)? FindEmptyCellWithLeastCandidates(int[][] board)
{
    int minCandidates = 10; // Plus que le maximum possible de candidats (1-9)
    (int, int)? cellWithLeastCandidates = null;

    for (int row = 0; row < board.Length; row++)
    {
        for (int col = 0; col < board[row].Length; col++)
        {
            if (board[row][col] == 0) // Cellule vide
            {
                var candidates = GetCandidates(board, row, col);
                if (candidates.Count < minCandidates)
                {
                    minCandidates = candidates.Count;
                    cellWithLeastCandidates = (row, col);
                }
            }
        }
    }

    return cellWithLeastCandidates;
}

    // Vérifier si une valeur est valide pour une cellule donnée en prenant en compte les contraintes du Sudoku
    // Vérifier si une valeur est valide pour une cellule donnée (row, col)
private static bool IsValid(int[][] board, int row, int col, int num)
{
    // Vérifier la ligne
    for (int i = 0; i < board.Length; i++)
    {
        if (board[row][i] == num) return false;
    }

    // Vérifier la colonne
    for (int j = 0; j < board[0].Length; j++)
    {
        if (board[j][col] == num) return false;
    }

    // Vérifier le bloc 3x3
    int startRow = row - row % 3, startCol = col - col % 3;
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (board[i + startRow][j + startCol] == num) return false;
        }
    }

    return true; // La valeur est valide
}

}
