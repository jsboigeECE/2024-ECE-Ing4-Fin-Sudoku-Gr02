using Kermalis.SudokuSolver;
using Sudoku.Shared;

namespace Sudoku.HumanSolver;

public class HumanSolver : ISudokuSolver 
{
    // Méthode principale pour résoudre un Sudoku.
    public SudokuGrid Solve(SudokuGrid s)
    {
        // Prétraiter la grille pour remplir les cellules simples.
        Preprocess(s);

        // Convertir le SudokuGrid en Puzzle.
        Puzzle puzzle = ConvertToPuzzle(s);

        // Utiliser la classe Solver pour résoudre le Puzzle.
        Solver solver = new Solver(puzzle);
        solver.TrySolve();

        // Convertir le puzzle résolu en SudokuGrid et le retourner.
        return ConvertToSudokuGrid(puzzle, s);
    }

    // Méthode pour convertir un SudokuGrid en Puzzle.
    public static Puzzle ConvertToPuzzle(SudokuGrid s)
    {
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

    // Méthode pour convertir un Puzzle en SudokuGrid.
    public static SudokuGrid ConvertToSudokuGrid(Puzzle p, SudokuGrid s)
    {
        for (int i = 0; i < s.Cells.Length; i++)
        {
            for (int j = 0; j < s.Cells[i].Length; j++)
            {
                s.Cells[i][j] = p[i, j].Value;
            }
        }
        return s;
    }

    // Méthode pour prétraiter le SudokuGrid. Remplir les cases faciles à compléter pour augmenter la vitesse d'exécution
    private static void Preprocess(SudokuGrid s)
    {
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

    // Méthode pour obtenir les candidats pour une cellule.
    private static List<int> GetCandidates(SudokuGrid s, int row, int col)
    {
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
}

