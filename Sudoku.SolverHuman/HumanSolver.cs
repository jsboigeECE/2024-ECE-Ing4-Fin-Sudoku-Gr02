using Sudoku.Shared;
using Kermalis.SudokuSolver;

namespace Sudoku.HumanSolver;

public class HumanSolver : ISudokuSolver
{
    // Méthode principale pour résoudre un Sudoku.
    public SudokuGrid Solve(SudokuGrid s)
    {
        // Prétraitement pour remplir les cellules simples.
        Preprocess(s);

        // Convertit le SudokuGrid en Puzzle pour l'utiliser avec la bibliothèque de résolution.
        Puzzle puzzle = ConvertToPuzzle(s);

        // Utilise les techniques de résolution humaines pour essayer de résoudre le puzzle.
        Solver solver = new Solver(puzzle);
        bool solvedByHumanMethods = solver.TrySolve();

        // Si les techniques humaines ne suffisent pas, utilise le backtracking.
        if (!solvedByHumanMethods)
        {
            int[][] board = ConvertPuzzleToBoard(puzzle);
            if (SolveWithBacktracking(board))
            {
                return ConvertBoardToSudokuGrid(board);
            }
        }

        // Convertit le puzzle résolu en SudokuGrid pour le retour.
        return ConvertToSudokuGrid(puzzle, s);
    }

    // Méthodes privées pour la conversion et la logique de résolution.

    private static Puzzle ConvertToPuzzle(SudokuGrid s)
    {
        // Convertit un SudokuGrid en tableau 2D pour le puzzle.
        int[][] board = new int[9][];
        for (int i = 0; i < s.Cells.Length; i++)
        {
            board[i] = new int[9];
            for (int j = 0; j < s.Cells[i].Length; j++)
            {
                board[i][j] = s.Cells[i][j];
            }
        }
        return new Puzzle(board, false);
    }

    private static SudokuGrid ConvertToSudokuGrid(Puzzle p, SudokuGrid s)
    {
        // Convertit un puzzle résolu en SudokuGrid.
        for (int i = 0; i < s.Cells.Length; i++)
        {
            for (int j = 0; j < s.Cells[i].Length; j++)
            {
                s.Cells[i][j] = p[i, j].Value;
            }
        }
        return s;
    }

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
                        var candidates = GetCandidates(s, i, j);
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

    private static List<int> GetCandidates(SudokuGrid s, int row, int col)
    {
        // Obtient les candidats possibles pour une cellule donnée.
        var candidates = Enumerable.Range(1, 9).ToList();
        for (int i = 0; i < 9; i++)
        {
            candidates.Remove(s.Cells[row][i]);
            candidates.Remove(s.Cells[i][col]);

            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;
            for (int r = startRow; r < startRow + 3; r++)
            {
                for (int c = startCol; c < startCol + 3; c++)
                {
                    candidates.Remove(s.Cells[r][c]);
                }
            }
        }
        return candidates;
    }

    private static int[][] ConvertPuzzleToBoard(Puzzle puzzle)
    {
        // Convertit un Puzzle en tableau 2D pour le backtracking.
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

    private static SudokuGrid ConvertBoardToSudokuGrid(int[][] board)
    {
        // Convertit un tableau 2D en SudokuGrid.
        SudokuGrid s = new SudokuGrid();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s.Cells[i][j] = board[i][j];
            }
        }
        return s;
    }

    private static bool SolveWithBacktracking(int[][] board)
    {
        // Méthode de backtracking pour résoudre le Sudoku.
        var emptyCell = FindEmptyCell(board);
        if (emptyCell == null)
        {
            return true; // Le puzzle est résolu.
        }

        for (int num = 1; num <= 9; num++)
        {
            if (IsValid(board, emptyCell.Value, num))
            {
                board[emptyCell.Value.Item1][emptyCell.Value.Item2] = num;

                if (SolveWithBacktracking(board))
                {
                    return true; // Solution trouvée.
                }

                board[emptyCell.Value.Item1][emptyCell.Value.Item2] = 0; // Annuler l'affectation.
            }
        }

        return false; // Aucune solution trouvée, backtracking requis.
    }

    // Méthodes auxiliaires pour la logique de backtracking.

    private static (int, int)? FindEmptyCell(int[][] board)
    {
        // Trouve la première cellule vide dans le tableau.
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row][col] == 0)
                {
                    return (row, col);
                }
            }
        }
        return null;
    }

    private static bool IsValid(int[][] board, (int, int) cell, int num)
    {
        // Vérifie si un numéro est valide pour une cellule donnée.
        // Vérifie la ligne, la colonne et le bloc 3x3.
        for (int col = 0; col < 9; col++)
        {
            if (board[cell.Item1][col] == num)
            {
                return false;
            }
        }

        for (int row = 0; row < 9; row++)
        {
            if (board[row][cell.Item2] == num)
            {
                return false;
            }
        }

        int startRow = cell.Item1 / 3 * 3;
        int startCol = cell.Item2 / 3 * 3;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[startRow + i][startCol + j] == num)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
